using NUnit.Framework;

namespace BaseTest;

public static class Assert2
{
    public static void NotNull(object? obj)
    {
        Assert.That(obj, Is.Not.Null);
    }
}