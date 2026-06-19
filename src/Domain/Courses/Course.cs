using School.Domain.Common;
using School.Domain.Modules;

namespace School.Domain.Courses;

public class Course
{
    private readonly List<Module> _modules = new();
    private readonly List<CourseMember> _members = new();

    public CourseId Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Status Status { get; private set; }

    private Course(string title, string description)
    {
        Title = title;
        Description = description;
        Status = Status.Draft;
    }

    public static Course Create(string title, string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        return new Course(title, description);
    }

    public void Publish()
    {
        if (Status == Status.Published)
            throw new InvalidOperationException($"Course {Id} is already published");

        Status = Status.Published;
    }

    public void Archive()
    {
        if (Status == Status.Archived)
            throw new InvalidOperationException($"Course {Id} is already archived");

        Status = Status.Archived;
    }
    {
        if (IsPublished == false)
            throw new InvalidOperationException($"Course {Id} is already hided");

        IsPublished = false;
    }
}
