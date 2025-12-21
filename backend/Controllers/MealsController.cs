using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using System.Security.Claims;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/meals")]
[Authorize]
public class MealsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly INotificationService _notificationService;

    public MealsController(ApplicationDbContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    // ========== MENU ENDPOINTS ==========

    /// <summary>
    /// Get all menus with optional filtering
    /// </summary>
    [HttpGet("menus")]
    public async Task<ActionResult<IEnumerable<object>>> GetMenus(
        [FromQuery] DateTime? date,
        [FromQuery] MealType? mealType,
        [FromQuery] Guid? cafeteriaId)
    {
        var query = _context.MealMenus
            .Include(m => m.Cafeteria)
            // .Where(m => m.IsPublished)
            .AsQueryable();

        if (date.HasValue)
        {
            var start = date.Value.Date;
            var end = start.AddDays(1);
            query = query.Where(m => m.Date >= start && m.Date < end);
        }

        if (mealType.HasValue)
            query = query.Where(m => m.MealType == mealType.Value);

        if (cafeteriaId.HasValue)
            query = query.Where(m => m.CafeteriaId == cafeteriaId.Value);

        var menus = await query
            .OrderBy(m => m.Date)
            .ThenBy(m => m.MealType)
            .Select(m => new
            {
                m.Id,
                m.CafeteriaId,
                CafeteriaName = m.Cafeteria != null ? m.Cafeteria.Name : "Unknown Cafeteria",
                m.Date,
                m.MealType,
                m.ItemsJson,
                m.NutritionJson,
                m.Price,
                m.CalorieCount,
                m.IsPublished,
                m.CreatedAt
            })
            .ToListAsync();

        return Ok(menus);
    }

    /// <summary>
    /// Get menu by ID
    /// </summary>
    [HttpGet("menus/{id}")]
    public async Task<ActionResult<object>> GetMenu(Guid id)
    {
        var menu = await _context.MealMenus
            .Include(m => m.Cafeteria)
            .Where(m => m.Id == id)
            .Select(m => new
            {
                m.Id,
                m.CafeteriaId,
                CafeteriaName = m.Cafeteria != null ? m.Cafeteria.Name : "Unknown Cafeteria",
                CafeteriaLocation = m.Cafeteria != null ? m.Cafeteria.Location : "Unknown Location",
                m.Date,
                m.MealType,
                m.ItemsJson,
                m.NutritionJson,
                m.Price,
                m.CalorieCount,
                m.IsPublished,
                m.CreatedAt,
                m.UpdatedAt
            })
            .FirstOrDefaultAsync();

        if (menu == null)
            return NotFound(new { message = "Menü bulunamadı" });

        return Ok(menu);
    }

    /// <summary>
    /// Create a new menu (Admin/Staff only)
    /// </summary>
    [HttpPost("menus")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<MealMenu>> CreateMenu([FromBody] CreateMenuDto dto)
    {
        var cafeteria = await _context.Cafeterias.FindAsync(dto.CafeteriaId);
        if (cafeteria == null)
            return BadRequest(new { message = "Kafeterya bulunamadı" });

        // Check for duplicate
        var exists = await _context.MealMenus.AnyAsync(m =>
            m.CafeteriaId == dto.CafeteriaId &&
            m.Date.Date == dto.Date.Date &&
            m.MealType == dto.MealType);

        if (exists)
            return BadRequest(new { message = "Bu tarih ve öğün için menü zaten mevcut" });

        var menu = new MealMenu
        {
            CafeteriaId = dto.CafeteriaId,
            Date = dto.Date.Date,
            MealType = dto.MealType,
            ItemsJson = dto.ItemsJson ?? "[]",
            NutritionJson = dto.NutritionJson,
            Price = dto.Price,
            CalorieCount = dto.CalorieCount,
            IsPublished = dto.IsPublished
        };

        _context.MealMenus.Add(menu);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMenu), new { id = menu.Id }, menu);
    }

    /// <summary>
    /// Update a menu
    /// </summary>
    [HttpPut("menus/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateMenu(Guid id, [FromBody] UpdateMenuDto dto)
    {
        var menu = await _context.MealMenus.FindAsync(id);
        if (menu == null)
            return NotFound(new { message = "Menü bulunamadı" });

        if (dto.ItemsJson != null)
            menu.ItemsJson = dto.ItemsJson;

        if (dto.NutritionJson != null)
            menu.NutritionJson = dto.NutritionJson;

        if (dto.IsPublished.HasValue)
            menu.IsPublished = dto.IsPublished.Value;

        if (dto.Price.HasValue)
            menu.Price = dto.Price.Value;

        if (dto.CalorieCount.HasValue)
            menu.CalorieCount = dto.CalorieCount.Value;

        menu.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Menü güncellendi" });
    }

    /// <summary>
    /// Delete a menu
    /// </summary>
    [HttpDelete("menus/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMenu(Guid id)
    {
        var menu = await _context.MealMenus.FindAsync(id);
        if (menu == null)
            return NotFound(new { message = "Menü bulunamadı" });

        _context.MealMenus.Remove(menu);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Menü silindi" });
    }

    // ========== RESERVATION ENDPOINTS ==========

    /// <summary>
    /// Create a meal reservation
    /// </summary>
    [HttpPost("reservations")]
    public async Task<ActionResult<object>> CreateReservation([FromBody] CreateReservationDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var menu = await _context.MealMenus
            .Include(m => m.Cafeteria)
            .FirstOrDefaultAsync(m => m.Id == dto.MenuId);

        if (menu == null)
            return BadRequest(new { message = "Menü bulunamadı" });

        // if (!menu.IsPublished)
        //    return BadRequest(new { message = "Bu menü henüz yayınlanmamış" });

        if (menu.Date.Date < DateTime.UtcNow.Date)
            return BadRequest(new { message = "Geçmiş tarihli menü için rezervasyon yapılamaz" });

        // Check if user already has reservation for this meal
        var existingReservation = await _context.MealReservations
            .AnyAsync(r => r.UserId == userId &&
                          r.Date.Date == menu.Date.Date &&
                          r.MealType == menu.MealType &&
                          r.Status != MealReservationStatus.Cancelled);

        if (existingReservation)
            return BadRequest(new { message = "Bu öğün için zaten rezervasyonunuz var" });

        // Check scholarship status and daily quota
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.UserId == userId);

        decimal amount = 0;
        if (student != null && student.IsScholarship)
        {
            // Check daily quota for scholarship students (max 2 meals/day)
            var todayReservations = await _context.MealReservations
                .CountAsync(r => r.UserId == userId &&
                               r.Date.Date == menu.Date.Date &&
                               r.Status != MealReservationStatus.Cancelled);

            if (todayReservations >= 2)
                return BadRequest(new { message = "Burslu öğrenci günlük yemek kotası (2 öğün) doldu" });
        }
        else
        {
             // For paid users, calculate amount
             amount = menu.Price > 0 ? menu.Price : 50;
        }
        
        // Prepare reservation ID
        var reservationId = Guid.NewGuid();

        if (amount > 0)
        {
            // For paid users, check wallet balance and deduct immediately
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            
            if (wallet == null || wallet.Balance < amount)
                return BadRequest(new { message = "Yetersiz bakiye. Lütfen cüzdanınıza para yükleyin." });

            // Deduct
            wallet.Balance -= amount;
            wallet.UpdatedAt = DateTime.UtcNow;
            
            // Explicitly mark as modified to ensure persistence
            _context.Update(wallet);

            var transaction = new Transaction
            {
                WalletId = wallet.Id,
                Type = TransactionType.Debit,
                Amount = amount,
                BalanceAfter = wallet.Balance,
                ReferenceType = "MealReservation",
                ReferenceId = reservationId,
                Description = $"Yemek rezervasyonu - {menu.Date:dd.MM.yyyy} {(menu.MealType == MealType.Lunch ? "Öğle" : "Akşam")}"
            };
            _context.Transactions.Add(transaction);
        }

        // Generate unique QR code
        var qrCode = $"MEAL-{Guid.NewGuid():N}";

        var reservation = new MealReservation
        {
            Id = reservationId,
            UserId = userId,
            MenuId = menu.Id,
            CafeteriaId = menu.CafeteriaId,
            MealType = menu.MealType,
            Date = menu.Date,
            Amount = amount,
            QrCode = qrCode,
            Status = MealReservationStatus.Reserved
        };

        _context.MealReservations.Add(reservation);
        await _context.SaveChangesAsync();

        // Send notification
        await _notificationService.SendMealReservationConfirmationAsync(reservation.Id);

        return CreatedAtAction(nameof(GetMyReservations), new
        {
            reservation.Id,
            reservation.Date,
            reservation.MealType,
            reservation.QrCode,
            reservation.Status,
            CafeteriaName = menu.Cafeteria.Name,
            MenuItems = menu.ItemsJson,
            message = "Rezervasyon oluşturuldu"
        });
    }

    /// <summary>
    /// Cancel a reservation
    /// </summary>
    [HttpDelete("reservations/{id}")]
    public async Task<IActionResult> CancelReservation(Guid id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var reservation = await _context.MealReservations
            .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

        if (reservation == null)
            return NotFound(new { message = "Rezervasyon bulunamadı" });

        if (reservation.Status != MealReservationStatus.Reserved)
            return BadRequest(new { message = "Bu rezervasyon iptal edilemez" });

        // Check if at least 2 hours before meal time
        var mealTime = reservation.Date.Date.AddHours(reservation.MealType == MealType.Lunch ? 12 : 18);
        if (DateTime.UtcNow > mealTime.AddHours(-2))
            return BadRequest(new { message = "Yemek saatinden en az 2 saat önce iptal edilmelidir" });

        reservation.Status = MealReservationStatus.Cancelled;
        reservation.UpdatedAt = DateTime.UtcNow;

        // If paid, refund to wallet
        if (reservation.Amount > 0)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet != null)
            {
                wallet.Balance += reservation.Amount;
                wallet.UpdatedAt = DateTime.UtcNow;

                var transaction = new Transaction
                {
                    WalletId = wallet.Id,
                    Type = TransactionType.Credit,
                    Amount = reservation.Amount,
                    BalanceAfter = wallet.Balance,
                    ReferenceType = "MealRefund",
                    ReferenceId = reservation.Id,
                    Description = "Yemek rezervasyonu iptali iadesi"
                };
                _context.Transactions.Add(transaction);
            }
        }

        await _context.SaveChangesAsync();

        // Send cancellation notification
        await _notificationService.SendMealReservationCancellationAsync(id);

        return Ok(new { message = "Rezervasyon iptal edildi" });
    }

    /// <summary>
    /// Get current user's reservations
    /// </summary>
    [HttpGet("reservations/my-reservations")]
    public async Task<ActionResult<IEnumerable<object>>> GetMyReservations(
        [FromQuery] MealReservationStatus? status,
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var query = _context.MealReservations
            .Include(r => r.Menu)
            .Include(r => r.Cafeteria)
            .Where(r => r.UserId == userId)
            .AsQueryable();

        if (status.HasValue)
            query = query.Where(r => r.Status == status.Value);

        if (dateFrom.HasValue)
            query = query.Where(r => r.Date >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(r => r.Date <= dateTo.Value);

        var reservations = await query
            .OrderByDescending(r => r.Date)
            .Select(r => new
            {
                r.Id,
                r.Date,
                r.MealType,
                r.QrCode,
                r.Status,
                r.Amount,
                r.UsedAt,
                CafeteriaName = r.Cafeteria != null ? r.Cafeteria.Name : "Unknown Cafeteria",
                MenuItems = r.Menu != null ? r.Menu.ItemsJson : "[]",
                r.CreatedAt
            })
            .ToListAsync();

        return Ok(reservations);
    }

    /// <summary>
    /// Verify meal reservation QR code - Staff only
    /// </summary>
    [HttpPost("reservations/verify-qr")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> VerifyMealQR([FromBody] VerifyMealQRDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.QrCode))
            return BadRequest(new { message = "QR kod gerekli" });

        var reservation = await _context.MealReservations
            .Include(r => r.User)
            .Include(r => r.Menu)
                .ThenInclude(m => m.Cafeteria)
            .FirstOrDefaultAsync(r => r.QrCode == dto.QrCode);

        if (reservation == null)
            return NotFound(new { message = "Rezervasyon bulunamadı" });

        if (reservation.Status == MealReservationStatus.Used)
            return BadRequest(new { message = "Bu rezervasyon zaten kullanıldı" });

        if (reservation.Status == MealReservationStatus.Cancelled)
            return BadRequest(new { message = "Bu rezervasyon iptal edilmiş" });

        if (reservation.Date.Date != DateTime.UtcNow.Date)
            return BadRequest(new { message = "Rezervasyon bugün için geçerli değil" });

        return Ok(new
        {
            id = reservation.Id,
            studentName = $"{reservation.User.FirstName} {reservation.User.LastName}",
            mealType = reservation.MealType.ToString(),
            date = reservation.Date,
            cafeteriaName = reservation.Menu.Cafeteria.Name,
            status = reservation.Status.ToString()
        });
    }

    /// <summary>
    /// Use a meal reservation (scan QR code) - Staff only
    /// </summary>
    [HttpPost("reservations/{id}/use")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> UseReservation(Guid id, [FromBody] UseReservationDto dto)
    {
        var reservation = await _context.MealReservations
            .Include(r => r.User)
            .Include(r => r.Menu)
            .FirstOrDefaultAsync(r => r.Id == id || r.QrCode == dto.QrCode);

        if (reservation == null)
            return NotFound(new { message = "Rezervasyon bulunamadı" });

        if (reservation.Status == MealReservationStatus.Used)
            return BadRequest(new { message = "Bu rezervasyon zaten kullanıldı" });

        if (reservation.Status == MealReservationStatus.Cancelled)
            return BadRequest(new { message = "Bu rezervasyon iptal edilmiş" });

        if (reservation.Date.Date != DateTime.UtcNow.Date)
            return BadRequest(new { message = "Rezervasyon bugün için geçerli değil" });

        // Mark as used
        reservation.Status = MealReservationStatus.Used;
        reservation.UsedAt = DateTime.UtcNow;
        reservation.UpdatedAt = DateTime.UtcNow;

        // Payment is already handled at reservation time
        // Just mark as used

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Yemek kullanıldı",
            UserName = $"{reservation.User.FirstName} {reservation.User.LastName}",
            reservation.MealType,
            reservation.UsedAt
        });
    }

    // ========== CAFETERIA ENDPOINTS ==========

    /// <summary>
    /// Get all cafeterias
    /// </summary>
    [HttpGet("cafeterias")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<object>>> GetCafeterias()
    {
        var cafeterias = await _context.Cafeterias
            .Where(c => c.IsActive)
            .Select(c => new
            {
                c.Id,
                c.Name,
                c.Location,
                c.Capacity
            })
            .ToListAsync();

        return Ok(cafeterias);
    }
}

// DTOs
public record CreateMenuDto(
    Guid CafeteriaId,
    DateTime Date,
    MealType MealType,
    string? ItemsJson,
    string? NutritionJson,
    decimal Price,
    int CalorieCount,
    bool IsPublished = false
);

public record UpdateMenuDto(
    string? ItemsJson,
    string? NutritionJson,
    decimal? Price,
    int? CalorieCount,
    bool? IsPublished
);

public record CreateReservationDto(Guid MenuId);

public record VerifyMealQRDto(string QrCode);

public record UseReservationDto(string? QrCode);
