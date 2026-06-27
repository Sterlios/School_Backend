using School.Domain.Common;
using School.Domain.Modules;

namespace School.Domain.Courses;

public class Course
{
    private readonly List<Module> _modules = new();

    private Course(string title, string description)
    {
        Title = title;
        Description = description;
        Status = Status.Draft;
    }

    public CourseId Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Status Status { get; private set; }
    public bool IsActive => Status == Status.Published && _modules.Any(m => m.IsActive);

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

    public void AddModule(Module module)
    {
        if (_modules.Any(m => m.Id == module.Id))
            throw new InvalidOperationException($"Cannot add module {module.Id}. Course {Id} already has Module.");

        _modules.Add(module);
    }

    public void RemoveModule(Module module)
    {
        if (!_modules.Any(m => m.Id == module.Id))
            throw new InvalidOperationException($"Cannot remove module {module.Id}. Course {Id} does not have Module.");

        _modules.Remove(module);
    }
}
