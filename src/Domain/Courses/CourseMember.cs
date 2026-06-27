using School.Domain.Users;

namespace School.Domain.Courses;

public class CourseMember
{
    public CourseId CourseId { get; init; }
    public UserId UserId { get; init; }
    public CourseRole Role { get; init; }
}