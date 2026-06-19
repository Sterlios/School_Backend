using School.Domain.Common;

namespace School.Domain.Lessons;

public class Lesson
{
    private Lesson(string title, string description)
    {
        Title = title;
        Description = description;
        Status = Status.Draft;
    }

    public LessonId Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Status Status { get; private set; }

    public static Lesson Create(string title, string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        return new Lesson(title, description);
    }

    public void Rename(string newTitle)
    {
        if (string.IsNullOrWhiteSpace(newTitle))
            throw new ArgumentNullException(nameof(newTitle), $"Поступил пустой {nameof(newTitle)}");

        Title = newTitle;
    }

    public void ChangeDescription(string newDescription)
    {
        if (newDescription is null)
            throw new ArgumentNullException(nameof(newDescription), $"Поступил пустой {nameof(newDescription)}");

        Description = newDescription;
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
