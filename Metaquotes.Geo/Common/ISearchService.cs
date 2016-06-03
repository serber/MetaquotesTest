using System.Collections.Generic;
using Metaquotes.Geo.Data;

namespace Metaquotes.Geo.Common
{
    public interface ISearchService
    {
        /// <summary>
        /// Поиск местоположения по IP адресу
        /// </summary>
        /// <param name="ip">IP адрес</param>
        Location? GetLocationByIp(ulong ip);

        /// <summary>
        /// Поиск местоположений по названию города
        /// </summary>
        /// <param name="city">Название города</param>
        IReadOnlyCollection<Location> GetLocationsByCity(string city);
    }
}