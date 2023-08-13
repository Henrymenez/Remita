using JsonDiffPatchDotNet;
using System.Text.Json;
using Xunit;

namespace Remita.Tests.Integration.Shared.Helpers;
public static class AssertionUtility
{
    public static void OrderedEqual<T>(IList<T> expected, IList<T> actual)
    {
        Assert.True(expected != null, "Expected was null");
        Assert.True(actual != null, "Actual was null");
        Assert.True(expected!.Count == actual!.Count, "Actual and Expected are different sizes.");
        for (int index = 0; index < expected!.Count; index++)
        {
            Equal(expected[index], actual[index]);
        }
    }

    private static void Equal<T>(T expected, T actual)
    {
        Assert.True(expected != null, "Expected was null");
        Assert.True(actual != null, "Actual was null");
        Assert.True(
            expected!.Equals(actual),
            new JsonDiffPatch()
                .Diff(
                    JsonSerializer.Serialize(expected),
                    JsonSerializer.Serialize(actual)
                    )
        );
    }
}