using System.ComponentModel.DataAnnotations;

namespace SmartCampus.API.DTOs;

// ========== Announcement DTOs ==========

public class AnnouncementDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public Guid? CourseId { get; set; }
    public string? CourseName { get; set; }
    public string? CourseCode { get; set; }
    public bool IsImportant { get; set; }
    public bool IsGeneral => CourseId == null;
}

public class CreateAnnouncementDto
{
    [Required(ErrorMessage = "Başlık gereklidir")]
    [StringLength(200, ErrorMessage = "Başlık en fazla 200 karakter olabilir")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "İçerik gereklidir")]
    [StringLength(5000, ErrorMessage = "İçerik en fazla 5000 karakter olabilir")]
    public string Content { get; set; } = string.Empty;

    public Guid? CourseId { get; set; } // Nullable - if null, it's a general announcement

    public bool IsImportant { get; set; } = false;
}
