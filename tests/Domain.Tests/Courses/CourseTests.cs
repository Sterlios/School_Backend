using FluentAssertions;

namespace Domain.Tests.Courses;

public class CourseTests
{
    [Fact]
    public void Create_Should_CreateCourse_WhenValidParametersAreProvided()
    {
        var course = Create();
        course.Should().NotBeNull();
    }

    private object Create() => throw new NotImplementedException();
}
