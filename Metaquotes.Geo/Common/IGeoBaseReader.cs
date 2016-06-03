using Metaquotes.Geo.Data;

namespace Metaquotes.Geo.Common
{
    public interface IGeoBaseReader
    {
        Header ReadHeader();

        Range[] ReadIpRanges(int records);

        Location[] ReadIpLocations(int records);

        uint[] ReadIndexes(int records);
    }
}