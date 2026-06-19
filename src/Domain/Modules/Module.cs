using School.Domain.Common;
using School.Domain.Lessons;

namespace School.Domain.Modules;

public class Module
{
    private readonly List<Lesson> _lessons = new();

    public ModuleId Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Status Status { get; private set; }

    private Module(string title, string description)
    {
        Title = title;
        Description = description;
        Status = Status.Draft;
    }

    public static Module Create(string title, string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        return new Module(title, description);
    }

    public void AddLesson(Lesson lesson)
    {
        if (_lessons.Any(l => l.Id == lesson.Id))
            throw new InvalidOperationException($"Cannot add lesson {lesson.Id}. Module {Id} already has Lesson.");

        _lessons.Add(lesson);
    }

    public void RemoveLesson(Lesson lesson)
    {
        if (!_lessons.Any(l => l.Id == lesson.Id))
            throw new InvalidOperationException($"Cannot remove lesson {lesson.Id}. Module {Id} does not have Lesson.");

        _lessons.Remove(lesson);
    }

    public void Publish()
    {
        if (Status == Status.Published)
            throw new InvalidOperationException($"Lesson {Id} is already published");

        Status = Status.Published;
    }

    public void Archive()
    {
        if (Status == Status.Archived)
            throw new InvalidOperationException($"Lesson {Id} is already archived");

        Status = Status.Archived;
    }
}