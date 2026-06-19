namespace School.Domain.Lessons;

public class Lesson
{
    public LessonId Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }

    public Lesson(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title), $"Поступил пустой {nameof(title)}");

        if (description is null)
            throw new ArgumentNullException(nameof(description), $"Поступил пустой {nameof(description)}");

        Title = title;
        Description = description;
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
}
