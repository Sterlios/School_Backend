using School.Domain.Modules;

namespace School.Domain.Courses;

public class Course
{
    private readonly List<Module> _modules = new();
    private readonly List<CourseMember> _members = new();

    public CourseId Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsPublished { get; private set; }

    public void Publish()
    {
        if (IsPublished)
            throw new InvalidOperationException($"Course {Id} is already published");

        IsPublished = true;
    }

    public void Hide()
    {
        if (IsPublished == false)
            throw new InvalidOperationException($"Course {Id} is already hided");

        IsPublished = false;
    }
}
