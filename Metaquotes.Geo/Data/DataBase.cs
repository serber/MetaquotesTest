namespace Metaquotes.Geo.Data
{
    public class DataBase
    {
        #region C-tor
        
        public DataBase(Header header, Range[] ranges, Location[] locations, uint[] indexes, long loadTime)
        {
            Header = header;
            Ranges = ranges;
            Locations = locations;
            Indexes = indexes;
            LoadTime = loadTime;
        }

        #endregion

        #region Public properties
        
        public Header Header { get; set; }

        public Range[] Ranges { get; set; }

        public Location[] Locations { get; set; }

        public uint[] Indexes { get; set; }

        public long LoadTime { get; set; }

        #endregion
    }
}