using SistemaTC.Core.Extensions;
using SistemaTC.Data.Entities;

namespace SistemaTC.Tests;

public class ExtensionsTests
{
    [Fact]
    public void GetLastException()
    {
        var lastExceptionText = "LastExceptionText";
        Exception exception = new("FirstExceptionText", new Exception(lastExceptionText));

        Assert.Equal(exception.GetLastException(), lastExceptionText);
    }

    [Fact]
    public void AsJson()
    {
        var text = new { test = "testvalue" };
        var json = text.AsJson();

        Assert.Equal("{\r\n  \"test\": \"testvalue\"\r\n}", json);
    }

    [Fact]
    public void AsNullableObject()
    {
        Role text = new();
        var json = text.AsJson();
        var roleObject = json.AsNullableObject<Role>();

        Assert.True(roleObject != null);

        var nullableRoleObject = string.Empty.AsNullableObject<Role>();

        Assert.True(nullableRoleObject == null);
    }

    [Fact]
    public void HashTextAndCompare()
    {
        var password = "Test123!";
        var hashedPassword = password.Hash();

        Assert.True(password.ValidHash(hashedPassword));
    }
}