using System;
using System.Linq;

namespace StringCombo.UnitTests.Factories;

public class StringFactory
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private static readonly Random Random = new();

    public static string Create()
    {
        return RandomString(10);
    }

    public static string RandomString(int length) =>
        new(Enumerable.Repeat(Chars, length)
            .Select(s => s[Random.Next(s.Length)])
            .ToArray()
        );
}