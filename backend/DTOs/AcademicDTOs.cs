namespace SmartCampus.API.DTOs;

// ========== Course DTOs ==========

public class CourseDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Credits { get; set; }
    public int ECTS { get; set; }
    public string? SyllabusUrl { get; set; }
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public List<CoursePrerequisiteDto> Prerequisites { get; set; } = new();
    public List<CourseSectionSummaryDto> AvailableSections { get; set; } = new();
}

public class CourseListDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Credits { get; set; }
    public int ECTS { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public int PrerequisiteCount { get; set; }
    public int AvailableSectionCount { get; set; }
}

public class CoursePrerequisiteDto
{
    public Guid CourseId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
}

public class CreateCourseRequest
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Credits { get; set; }
    public int ECTS { get; set; }
    public string? SyllabusUrl { get; set; }
    public Guid DepartmentId { get; set; }
    public List<Guid> PrerequisiteCourseIds { get; set; } = new();
}

public class UpdateCourseRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Credits { get; set; }
    public int? ECTS { get; set; }
    public string? SyllabusUrl { get; set; }
    public List<Guid>? PrerequisiteCourseIds { get; set; }
}

// ========== Course Section DTOs ==========

public class CourseSectionDto
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public int SectionNumber { get; set; }
    public string Semester { get; set; } = string.Empty;
    public int Year { get; set; }
    public Guid InstructorId { get; set; }
    public string InstructorName { get; set; } = string.Empty;
    public Guid? ClassroomId { get; set; }
    public string? ClassroomName { get; set; }
    public int Capacity { get; set; }
    public int EnrolledCount { get; set; }
    public int AvailableSeats => Capacity - EnrolledCount;
    public List<ScheduleSlotDto> Schedule { get; set; } = new();
    public bool IsActive { get; set; }
}

public class CourseSectionSummaryDto
{
    public Guid Id { get; set; }
    public int SectionNumber { get; set; }
    public string InstructorName { get; set; } = string.Empty;
    public string? ClassroomName { get; set; }
    public int Capacity { get; set; }
    public int EnrolledCount { get; set; }
    public int AvailableSeats => Capacity - EnrolledCount;
    public List<ScheduleSlotDto> Schedule { get; set; } = new();
}

public class ScheduleSlotDto
{
    public string Day { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
}

public class CreateSectionRequest
{
    public Guid CourseId { get; set; }
    public int SectionNumber { get; set; }
    public string Semester { get; set; } = "Fall";
    public int Year { get; set; }
    public Guid InstructorId { get; set; }
    public Guid? ClassroomId { get; set; }
    public int Capacity { get; set; }
    public List<ScheduleSlotDto> Schedule { get; set; } = new();
}

public class UpdateSectionRequest
{
    public Guid? InstructorId { get; set; }
    public Guid? ClassroomId { get; set; }
    public int? Capacity { get; set; }
    public List<ScheduleSlotDto>? Schedule { get; set; }
}

// ========== Enrollment DTOs ==========

public class EnrollmentDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentNumber { get; set; } = string.Empty;
    public Guid SectionId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public int SectionNumber { get; set; }
    public string InstructorName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime EnrollmentDate { get; set; }
    public decimal? MidtermGrade { get; set; }
    public decimal? FinalGrade { get; set; }
    public decimal? HomeworkGrade { get; set; }
    public string? LetterGrade { get; set; }
    public decimal? GradePoint { get; set; }
    public List<ScheduleSlotDto> Schedule { get; set; } = new();
    public decimal AttendancePercentage { get; set; }
}

public class EnrollRequest
{
    public Guid SectionId { get; set; }
}

public class EnrollmentCheckResult
{
    public bool CanEnroll { get; set; }
    public List<string> Errors { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
}

// ========== Grade DTOs ==========

public class GradeDto
{
    public Guid EnrollmentId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public int Credits { get; set; }
    public string Semester { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal? MidtermGrade { get; set; }
    public decimal? FinalGrade { get; set; }
    public decimal? HomeworkGrade { get; set; }
    public string? LetterGrade { get; set; }
    public decimal? GradePoint { get; set; }
}

public class TranscriptDto
{
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentNumber { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public int EnrollmentYear { get; set; }
    public decimal CGPA { get; set; }
    public int TotalCredits { get; set; }
    public int TotalECTS { get; set; }
    public List<SemesterGradesDto> Semesters { get; set; } = new();
}

public class SemesterGradesDto
{
    public string Semester { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal GPA { get; set; }
    public int Credits { get; set; }
    public List<GradeDto> Courses { get; set; } = new();
}

public class GradeInputRequest
{
    public Guid EnrollmentId { get; set; }
    public decimal? MidtermGrade { get; set; }
    public decimal? FinalGrade { get; set; }
    public decimal? HomeworkGrade { get; set; }
}

public class BulkGradeInputRequest
{
    public List<GradeInputRequest> Grades { get; set; } = new();
}

// ========== Classroom DTOs ==========

public class ClassroomDto
{
    public Guid Id { get; set; }
    public string Building { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;
    public string FullName => $"{Building} - {RoomNumber}";
    public int Capacity { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public List<string> Features { get; set; } = new();
    public bool IsActive { get; set; }
}
