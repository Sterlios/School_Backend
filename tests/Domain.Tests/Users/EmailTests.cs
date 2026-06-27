using FluentAssertions;
using School.Domain.Exceptions;
using School.Domain.Users;

namespace School.Domain.Tests.Users;

public class EmailTests
{
    [Fact]
    public void Create_Should_CreateEmail_When_ValueIsValid()
    {
        var value = "anton@example.com";

        var email = Email.Create(value);

        email.Value.Should().Be("anton@example.com");
    }

    [Fact]
    public void Create_Should_Throw_When_EmailIsNull()
    {
        string? value = null;

        Action act = () => Email.Create(value!);

        act.Should().Throw<InvalidEmailException>();
    }

    [Fact]
    public void Create_Should_Throw_When_EmailIsEmpty()
    {
        var value = "";

        Action act = () => Email.Create(value);

        act.Should().Throw<InvalidEmailException>();
    }

    [Theory]
    [InlineData("anton")]
    [InlineData("anton@")]
    [InlineData("anton@Kuzmin")]
    [InlineData("@gmail.com")]
    [InlineData("anton.gmail.com")]
    [InlineData("ant@on@gmail.com")]
    [InlineData("anton@@gmail.com")]
    public void Create_Should_Throw_When_EmailHasInvalidFormat(string value)
    {
        Action act = () => Email.Create(value);

        act.Should().Throw<InvalidEmailException>();
    }

    [Fact]
    public void Create_Should_NormalizeEmail()
    {
        var value = "Anton@Example.Com";

        var email = Email.Create(value);

        email.Value.Should().Be("anton@example.com");
    }

    [Fact]
    public void EmailsWithSameValue_Should_BeEqual()
    {
        var first = Email.Create("Anton@Example.Com");
        var second = Email.Create("anton@example.com");

        first.Should().Be(second);
    }
}
