using FluentAssertions;
using School.Domain.Exceptions;
using School.Domain.Users;

namespace Domain.Tests.Users;

public class EmailTests
{
    [Fact]
    public void Create_Should_Create_Email_When_Value_Is_Valid()
    {
        var value = "anton@example.com";

        var email = Email.Create(value);

        email.Value.Should().Be("anton@example.com");
    }

    [Fact]
    public void Create_Should_Throw_When_Email_Is_Null()
    {
        string? value = null;

        Action act = () => Email.Create(value!);

        act.Should().Throw<InvalidEmailException>();
    }

    [Fact]
    public void Create_Should_Throw_When_Email_Is_Empty()
    {
        var value = "";

        Action act = () => Email.Create(value);

        act.Should().Throw<InvalidEmailException>();
    }

    [Theory]
    [InlineData("anton")]
    [InlineData("anton@")]
    [InlineData("@gmail.com")]
    [InlineData("anton.gmail.com")]
    [InlineData("anton@@gmail.com")]
    public void Create_Should_Throw_When_Email_Has_Invalid_Format(string value)
    {
        Action act = () => Email.Create(value);

        act.Should().Throw<InvalidEmailException>();
    }

    [Fact]
    public void Create_Should_Normalize_Email()
    {
        var value = "Anton@Example.Com";

        var email = Email.Create(value);

        email.Value.Should().Be("anton@example.com");
    }

    [Fact]
    public void Emails_With_Same_Value_Should_Be_Equal()
    {
        var first = Email.Create("Anton@Example.Com");
        var second = Email.Create("anton@example.com");

        first.Should().Be(second);
    }
}
