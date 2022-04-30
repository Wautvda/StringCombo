using System;
using System.Collections;
using System.Collections.Generic;
using StringCombo.Models;
using StringCombo.UnitTests.Factories;
using Xunit;

namespace StringCombo.UnitTests;

public class JoinableStringTests
{
    [Theory]
    [ClassData(typeof(JoinedStringTestData))]
    public void WhenHasValuesShouldHaveSpecificOutput(params string[] values)
    {
        var joinableString = new JoinableString(values);
        Assert.Equal(string.Join("", values), joinableString.ToString());
        Assert.Equal($"{string.Join("+", values)}={string.Join("", values)}", joinableString.GetOutput());
    }
}

public class JoinedStringTestData : IEnumerable<object[]>
{
    private static readonly Random Random = new();
    public IEnumerator<object[]> GetEnumerator()
    {
        for (int i = 0; i < 10; i++)
        {
            var res = new List<object>();
            // Should return between 2 and 4 random strings per test
            for (int j = 0; j < Random.Next(2, 4); j++)
            {
                res.Add(StringFactory.Create());
            }
            yield return res.ToArray();
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}