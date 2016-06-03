using System;

namespace Metaquotes.Geo.Data
{
    public struct Location
    {
        #region C-tor
        
        public Location(char[] country, char[] region, char[] postal, char[] city, char[] organization, float latitude, float longitude)
        {
            Country = country;
            Region = region;
            Postal = postal;
            City = city;
            Organization = organization;
            Latitude = latitude;
            Longitude = longitude;
        }
        
        #endregion

        #region Public properties

        public char[] Country;

        public char[] Region;

        public char[] Postal;

        public char[] City;

        public char[] Organization;

        public float Latitude;

        public float Longitude;

        #endregion

        #region Public methods
        
        public int CompareCity(string city)
        {
            string cityString = new string(City).TrimEnd('\0');

            return string.Compare(cityString, city, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return $"{new string(Country)}";
        }

        #endregion
    }
}