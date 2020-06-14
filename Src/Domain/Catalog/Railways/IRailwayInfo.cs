using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public interface IRailwayInfo
    {
        RailwayId Id { get; }

        Slug Slug { get; }

        string Name { get; }

        string ToLabel() => Name;
    }
}
