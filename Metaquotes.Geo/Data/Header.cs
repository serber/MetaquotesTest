namespace Metaquotes.Geo.Data
{
    public struct Header
    {
        public int Version;

        public char[] Name;

        public ulong Date;

        public int Records;

        public uint OffsetRanges;

        public uint OffsetCities;

        public uint OffsetLocations;
    }
}