namespace Metaquotes.Geo.Data
{
    public struct Range
    {
        public ulong IpFrom;

        public ulong IpTo;

        public uint Index;
        
        public override string ToString()
        {
            return $"[{Index}] {IpFrom} - {IpTo}";
        }
    }
}