using System.Collections.Generic;
using Metaquotes.Geo.Common;
using Metaquotes.Geo.Data;

namespace Metaquotes.Geo
{
    public class SearchService : ISearchService
    {
        #region Private fields

        private readonly DataBase _dataBase;

        #endregion

        #region C-tor

        public SearchService(DataBase dataBase)
        {
            _dataBase = dataBase;
        }

        #endregion

        #region Public methods

        public Location? GetLocationByIp(ulong ip)
        {
            // Обычный бинарный поиск по отсортированному списку диапазонов IP адресов
            int low = 0;
            int high = _dataBase.Ranges.Length - 1;

            while (low <= high)
            {
                int midpoint = low + (high - low) / 2;
                Range range = _dataBase.Ranges[midpoint];

                if (ip >= range.IpFrom && ip <= range.IpTo)
                {
                    return _dataBase.Locations[range.Index];
                }

                if (ip < range.IpFrom)
                {
                    high = midpoint - 1;
                }
                else
                {
                    low = midpoint + 1;
                }
            }

            return null;
        }

        public IReadOnlyCollection<Location> GetLocationsByCity(string city)
        {
            // Немного модифицированный бинарный поиск.
            // Находим нужный элемент, далее идем влево и вправо и находим элементы, завершаем поиск
            List<Location> locations = new List<Location>();

            int low = 0;
            int high = _dataBase.Indexes.Length - 1;

            while (low <= high)
            {
                int midpoint = low + (high - low) / 2;
                Location location = _dataBase.Locations[_dataBase.Indexes[midpoint]];
                int compareResult = location.CompareCity(city);

                if (compareResult == 0)
                {
                    locations.Add(location);
                    int leftIndex = midpoint;
                    int rightIndex = midpoint;

                    while (leftIndex-- > low)
                    {
                        Location leftLocation = _dataBase.Locations[_dataBase.Indexes[leftIndex]];
                        if (leftLocation.CompareCity(city) != 0)
                        {
                            break;
                        }

                        locations.Add(leftLocation);
                    }

                    while (rightIndex++ < high)
                    {
                        Location rightLocation = _dataBase.Locations[_dataBase.Indexes[rightIndex]];
                        if (rightLocation.CompareCity(city) != 0)
                        {
                            break;
                        }

                        locations.Add(rightLocation);
                    }

                    break;
                }

                if (compareResult > 0)
                {
                    high = midpoint - 1;
                }
                else
                {
                    low = midpoint + 1;
                }
            }

            return locations;
        }

        #endregion
    }
}