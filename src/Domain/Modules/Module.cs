using School.Domain.Lessons;

namespace School.Domain.Modules;

public class Module
{
    private readonly List<Lesson> _lessons = new();

    public ModuleId Id { get; init; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsPublished { get; private set; }

    public void AddLesson(Lesson lesson)
    {
        if (_lessons.Contains(lesson))
            throw new InvalidOperationException($"Cannot add lesson {lesson.Id}. Module {Id} already has Lesson.");


    }
}