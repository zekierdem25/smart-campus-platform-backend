using System.ComponentModel.DataAnnotations;

namespace SmartCampus.API.DTOs;

// ========== Academic Calendar DTOs ==========

public class AcademicEventDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Type { get; set; } = string.Empty; // Exam, Holiday, General
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateAcademicEventDto
{
    [Required(ErrorMessage = "Başlık gereklidir")]
    [StringLength(200, ErrorMessage = "Başlık en fazla 200 karakter olabilir")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Başlangıç tarihi gereklidir")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "Bitiş tarihi gereklidir")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "Etkinlik tipi gereklidir")]
    public string Type { get; set; } = "General"; // Exam, Holiday, General

    [StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir")]
    public string? Description { get; set; }
}

public class UpdateAcademicEventDto
{
    [StringLength(200, ErrorMessage = "Başlık en fazla 200 karakter olabilir")]
    public string? Title { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Type { get; set; } // Exam, Holiday, General

    [StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir")]
    public string? Description { get; set; }
}
