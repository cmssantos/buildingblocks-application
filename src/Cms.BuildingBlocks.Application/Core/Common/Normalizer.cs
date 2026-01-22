namespace Cms.BuildingBlocks.Application.Core.Common;

public static class Normalizer
{
    public static string TrimAndCollapseSpaces(string value)
        => string.Join(
            ' ',
            value.Split([' '],
            StringSplitOptions.RemoveEmptyEntries)).Trim();
}
