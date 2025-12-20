using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using System.Security.Claims;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/equipment")]
[Authorize]
public class EquipmentController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<EquipmentController> _logger;
    private readonly INotificationService _notificationService;

    // Maximum number of active borrowings per user
    private const int MaxActiveBorrowingsPerUser = 3;

    public EquipmentController(
        ApplicationDbContext context, 
        ILogger<EquipmentController> logger,
        INotificationService notificationService)
    {
        _context = context;
        _logger = logger;
        _notificationService = notificationService;
    }

    // ========== EQUIPMENT CRUD ENDPOINTS ==========

    /// <summary>
    /// Get all equipment with optional filters
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<object>> GetEquipment(
        [FromQuery] EquipmentType? type = null,
        [FromQuery] EquipmentStatus? status = null,
        [FromQuery] string? location = null,
        [FromQuery] string? search = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = _context.Equipments.Where(e => e.IsActive).AsQueryable();

        if (type.HasValue)
            query = query.Where(e => e.Type == type.Value);

        if (status.HasValue)
            query = query.Where(e => e.Status == status.Value);

        if (!string.IsNullOrEmpty(location))
            query = query.Where(e => e.Location != null && e.Location.Contains(location));

        if (!string.IsNullOrEmpty(search))
            query = query.Where(e => e.Name.Contains(search) || 
                                    e.SerialNumber.Contains(search) ||
                                    (e.Brand != null && e.Brand.Contains(search)));

        var totalCount = await query.CountAsync();

        var equipment = await query
            .OrderBy(e => e.Type)
            .ThenBy(e => e.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new
            {
                e.Id,
                e.Name,
                e.Type,
                e.SerialNumber,
                e.Status,
                e.Location,
                e.Description,
                e.Brand,
                e.Model
            })
            .ToListAsync();

        return Ok(new
        {
            equipment,
            totalCount,
            page,
            pageSize,
            totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        });
    }

    /// <summary>
    /// Get equipment by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetEquipmentById(Guid id)
    {
        var equipment = await _context.Equipments
            .Include(e => e.Borrowings.Where(b => b.Status == BorrowingStatus.Borrowed || b.Status == BorrowingStatus.Overdue))
                .ThenInclude(b => b.User)
            .Where(e => e.Id == id)
            .Select(e => new
            {
                e.Id,
                e.Name,
                e.Type,
                e.SerialNumber,
                e.Status,
                e.Location,
                e.Description,
                e.Brand,
                e.Model,
                e.CreatedAt,
                e.UpdatedAt,
                CurrentBorrowing = e.Borrowings
                    .Where(b => b.Status == BorrowingStatus.Borrowed || b.Status == BorrowingStatus.Overdue)
                    .Select(b => new
                    {
                        b.Id,
                        BorrowerName = $"{b.User.FirstName} {b.User.LastName}",
                        b.BorrowDate,
                        b.ExpectedReturnDate,
                        b.Status
                    })
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();

        if (equipment == null)
            return NotFound(new { message = "Ekipman bulunamadı" });

        return Ok(equipment);
    }

    /// <summary>
    /// Create new equipment (Admin only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> CreateEquipment([FromBody] CreateEquipmentDto dto)
    {
        // Check for duplicate serial number
        var existingSerial = await _context.Equipments
            .AnyAsync(e => e.SerialNumber == dto.SerialNumber);

        if (existingSerial)
            return BadRequest(new { message = "Bu seri numarası zaten kayıtlı" });

        var equipment = new Equipment
        {
            Name = dto.Name,
            Type = dto.Type,
            SerialNumber = dto.SerialNumber,
            Status = EquipmentStatus.Available,
            Location = dto.Location,
            Description = dto.Description,
            Brand = dto.Brand,
            Model = dto.Model
        };

        _context.Equipments.Add(equipment);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Equipment created: {Id} - {Name}", equipment.Id, equipment.Name);

        return CreatedAtAction(nameof(GetEquipmentById), new { id = equipment.Id }, new
        {
            equipment.Id,
            equipment.Name,
            equipment.Type,
            equipment.SerialNumber,
            message = "Ekipman oluşturuldu"
        });
    }

    /// <summary>
    /// Update equipment (Admin only)
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> UpdateEquipment(Guid id, [FromBody] UpdateEquipmentDto dto)
    {
        var equipment = await _context.Equipments.FindAsync(id);
        if (equipment == null)
            return NotFound(new { message = "Ekipman bulunamadı" });

        if (!string.IsNullOrEmpty(dto.Name))
            equipment.Name = dto.Name;

        if (dto.Type.HasValue)
            equipment.Type = dto.Type.Value;

        if (!string.IsNullOrEmpty(dto.SerialNumber) && dto.SerialNumber != equipment.SerialNumber)
        {
            var existingSerial = await _context.Equipments
                .AnyAsync(e => e.SerialNumber == dto.SerialNumber && e.Id != id);
            if (existingSerial)
                return BadRequest(new { message = "Bu seri numarası zaten kayıtlı" });
            equipment.SerialNumber = dto.SerialNumber;
        }

        if (dto.Status.HasValue)
            equipment.Status = dto.Status.Value;

        if (dto.Location != null)
            equipment.Location = dto.Location;

        if (dto.Description != null)
            equipment.Description = dto.Description;

        if (dto.Brand != null)
            equipment.Brand = dto.Brand;

        if (dto.Model != null)
            equipment.Model = dto.Model;

        equipment.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Ekipman güncellendi", equipment.Id });
    }

    /// <summary>
    /// Delete equipment (Admin only)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteEquipment(Guid id)
    {
        var equipment = await _context.Equipments.FindAsync(id);
        if (equipment == null)
            return NotFound(new { message = "Ekipman bulunamadı" });

        // Check for active borrowings
        var hasActiveBorrowings = await _context.EquipmentBorrowings
            .AnyAsync(b => b.EquipmentId == id && 
                          (b.Status == BorrowingStatus.Borrowed || 
                           b.Status == BorrowingStatus.Overdue ||
                           b.Status == BorrowingStatus.Approved));

        if (hasActiveBorrowings)
            return BadRequest(new { message = "Aktif ödünç kaydı olan ekipman silinemez" });

        // Soft delete
        equipment.IsActive = false;
        equipment.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Ekipman silindi" });
    }

    // ========== BORROWING ENDPOINTS ==========

    /// <summary>
    /// Borrow equipment
    /// </summary>
    [HttpPost("{id}/borrow")]
    public async Task<ActionResult<object>> BorrowEquipment(Guid id, [FromBody] BorrowEquipmentDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var equipment = await _context.Equipments.FindAsync(id);
        if (equipment == null)
            return NotFound(new { message = "Ekipman bulunamadı" });

        if (equipment.Status != EquipmentStatus.Available)
            return BadRequest(new { message = "Bu ekipman şu an ödünç alınamaz" });

        // Check user's active borrowing count
        var activeBorrowings = await _context.EquipmentBorrowings
            .CountAsync(b => b.UserId == userId && 
                            (b.Status == BorrowingStatus.Borrowed || 
                             b.Status == BorrowingStatus.Approved ||
                             b.Status == BorrowingStatus.Pending));

        if (activeBorrowings >= MaxActiveBorrowingsPerUser)
            return BadRequest(new { message = $"Maksimum {MaxActiveBorrowingsPerUser} ekipman ödünç alabilirsiniz" });

        // Validate expected return date
        if (dto.ExpectedReturnDate <= DateTime.UtcNow)
            return BadRequest(new { message = "İade tarihi gelecekte olmalıdır" });

        if (dto.ExpectedReturnDate > DateTime.UtcNow.AddDays(30))
            return BadRequest(new { message = "Maksimum ödünç süresi 30 gündür" });

        var borrowing = new EquipmentBorrowing
        {
            EquipmentId = id,
            UserId = userId,
            BorrowDate = DateTime.UtcNow,
            ExpectedReturnDate = dto.ExpectedReturnDate,
            Purpose = dto.Purpose,
            Notes = dto.Notes,
            Status = BorrowingStatus.Pending
        };

        _context.EquipmentBorrowings.Add(borrowing);
        await _context.SaveChangesAsync();

        // Send notification
        await _notificationService.SendEquipmentBorrowingRequestAsync(borrowing.Id);

        _logger.LogInformation("Equipment borrowing requested: {BorrowingId} by user {UserId}", borrowing.Id, userId);

        return Ok(new
        {
            borrowing.Id,
            borrowing.Status,
            message = "Ödünç alma talebiniz alındı. Admin onayı bekleniyor."
        });
    }

    /// <summary>
    /// Return borrowed equipment
    /// </summary>
    [HttpPost("borrowings/{borrowingId}/return")]
    public async Task<IActionResult> ReturnEquipment(Guid borrowingId)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var borrowing = await _context.EquipmentBorrowings
            .Include(b => b.Equipment)
            .FirstOrDefaultAsync(b => b.Id == borrowingId && b.UserId == userId);

        if (borrowing == null)
            return NotFound(new { message = "Ödünç kaydı bulunamadı" });

        if (borrowing.Status != BorrowingStatus.Borrowed && borrowing.Status != BorrowingStatus.Overdue)
            return BadRequest(new { message = "Bu ekipman zaten iade edilmiş veya henüz teslim alınmamış" });

        borrowing.ActualReturnDate = DateTime.UtcNow;
        borrowing.Status = BorrowingStatus.Returned;
        borrowing.UpdatedAt = DateTime.UtcNow;
        
        borrowing.Equipment.Status = EquipmentStatus.Available;
        borrowing.Equipment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Equipment returned: {BorrowingId}", borrowingId);

        return Ok(new { message = "Ekipman iade edildi" });
    }

    /// <summary>
    /// Get current user's borrowings
    /// </summary>
    [HttpGet("my-borrowings")]
    public async Task<ActionResult<object>> GetMyBorrowings(
        [FromQuery] BorrowingStatus? status = null)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var query = _context.EquipmentBorrowings
            .Include(b => b.Equipment)
            .Where(b => b.UserId == userId)
            .AsQueryable();

        if (status.HasValue)
            query = query.Where(b => b.Status == status.Value);

        var borrowings = await query
            .OrderByDescending(b => b.CreatedAt)
            .Select(b => new
            {
                b.Id,
                b.BorrowDate,
                b.ExpectedReturnDate,
                b.ActualReturnDate,
                b.Status,
                b.Purpose,
                Equipment = new
                {
                    b.Equipment.Id,
                    b.Equipment.Name,
                    b.Equipment.Type,
                    b.Equipment.SerialNumber,
                    b.Equipment.Location
                }
            })
            .ToListAsync();

        return Ok(borrowings);
    }

    /// <summary>
    /// Approve borrowing request (Admin only)
    /// </summary>
    [HttpPut("borrowings/{borrowingId}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApproveBorrowing(Guid borrowingId)
    {
        var adminUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var borrowing = await _context.EquipmentBorrowings
            .Include(b => b.Equipment)
            .FirstOrDefaultAsync(b => b.Id == borrowingId);

        if (borrowing == null)
            return NotFound(new { message = "Ödünç kaydı bulunamadı" });

        if (borrowing.Status != BorrowingStatus.Pending)
            return BadRequest(new { message = "Bu talep zaten işlenmiş" });

        // Check if equipment is still available
        if (borrowing.Equipment.Status != EquipmentStatus.Available)
            return BadRequest(new { message = "Ekipman artık uygun değil" });

        borrowing.Status = BorrowingStatus.Borrowed;
        borrowing.ApprovedBy = adminUserId;
        borrowing.ApprovedAt = DateTime.UtcNow;
        borrowing.UpdatedAt = DateTime.UtcNow;

        borrowing.Equipment.Status = EquipmentStatus.Borrowed;
        borrowing.Equipment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        // Send notification
        await _notificationService.SendEquipmentBorrowingApprovalAsync(borrowingId);

        _logger.LogInformation("Borrowing approved: {BorrowingId} by admin {AdminId}", borrowingId, adminUserId);

        return Ok(new { message = "Ödünç alma talebi onaylandı" });
    }

    /// <summary>
    /// Reject borrowing request (Admin only)
    /// </summary>
    [HttpPut("borrowings/{borrowingId}/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RejectBorrowing(Guid borrowingId, [FromBody] RejectBorrowingDto? dto)
    {
        var adminUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var borrowing = await _context.EquipmentBorrowings
            .FirstOrDefaultAsync(b => b.Id == borrowingId);

        if (borrowing == null)
            return NotFound(new { message = "Ödünç kaydı bulunamadı" });

        if (borrowing.Status != BorrowingStatus.Pending)
            return BadRequest(new { message = "Bu talep zaten işlenmiş" });

        borrowing.Status = BorrowingStatus.Rejected;
        borrowing.ApprovedBy = adminUserId;
        borrowing.ApprovedAt = DateTime.UtcNow;
        borrowing.Notes = dto?.Reason ?? borrowing.Notes;
        borrowing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        // Send notification
        await _notificationService.SendEquipmentBorrowingRejectionAsync(borrowingId, dto?.Reason);

        _logger.LogInformation("Borrowing rejected: {BorrowingId} by admin {AdminId}", borrowingId, adminUserId);

        return Ok(new { message = "Ödünç alma talebi reddedildi" });
    }

    /// <summary>
    /// Get all borrowings (Admin only)
    /// </summary>
    [HttpGet("borrowings")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> GetAllBorrowings(
        [FromQuery] BorrowingStatus? status = null,
        [FromQuery] Guid? equipmentId = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = _context.EquipmentBorrowings
            .Include(b => b.Equipment)
            .Include(b => b.User)
            .AsQueryable();

        if (status.HasValue)
            query = query.Where(b => b.Status == status.Value);

        if (equipmentId.HasValue)
            query = query.Where(b => b.EquipmentId == equipmentId.Value);

        var totalCount = await query.CountAsync();

        var borrowings = await query
            .OrderByDescending(b => b.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(b => new
            {
                b.Id,
                b.BorrowDate,
                b.ExpectedReturnDate,
                b.ActualReturnDate,
                b.Status,
                b.Purpose,
                b.Notes,
                User = new
                {
                    b.User.Id,
                    Name = $"{b.User.FirstName} {b.User.LastName}",
                    b.User.Email
                },
                Equipment = new
                {
                    b.Equipment.Id,
                    b.Equipment.Name,
                    b.Equipment.Type,
                    b.Equipment.SerialNumber
                }
            })
            .ToListAsync();

        return Ok(new
        {
            borrowings,
            totalCount,
            page,
            pageSize,
            totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        });
    }

    /// <summary>
    /// Get pending borrowing requests (Admin only)
    /// </summary>
    [HttpGet("borrowings/pending")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> GetPendingBorrowings()
    {
        var pendingBorrowings = await _context.EquipmentBorrowings
            .Include(b => b.Equipment)
            .Include(b => b.User)
            .Where(b => b.Status == BorrowingStatus.Pending)
            .OrderBy(b => b.CreatedAt)
            .Select(b => new
            {
                b.Id,
                b.BorrowDate,
                b.ExpectedReturnDate,
                b.Purpose,
                b.CreatedAt,
                User = new
                {
                    b.User.Id,
                    Name = $"{b.User.FirstName} {b.User.LastName}",
                    b.User.Email
                },
                Equipment = new
                {
                    b.Equipment.Id,
                    b.Equipment.Name,
                    b.Equipment.Type,
                    b.Equipment.SerialNumber,
                    b.Equipment.Location
                }
            })
            .ToListAsync();

        return Ok(pendingBorrowings);
    }
}

// DTOs
public record CreateEquipmentDto(
    string Name,
    EquipmentType Type,
    string SerialNumber,
    string? Location,
    string? Description,
    string? Brand,
    string? Model
);

public record UpdateEquipmentDto(
    string? Name,
    EquipmentType? Type,
    string? SerialNumber,
    EquipmentStatus? Status,
    string? Location,
    string? Description,
    string? Brand,
    string? Model
);

public record BorrowEquipmentDto(
    DateTime ExpectedReturnDate,
    string Purpose,
    string? Notes
);

public record RejectBorrowingDto(string? Reason);
