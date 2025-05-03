using NUnit.Framework;

namespace BaseTest;

public static class Assert2
{
    public static void NotNull(object? obj) 
        => Assert.That(obj, Is.Not.Null);
    
    public static void True(bool obj) 
        => Assert.That(obj, Is.True);
}