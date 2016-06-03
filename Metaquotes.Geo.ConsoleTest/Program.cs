using System;
using System.Collections.Generic;
using Metaquotes.Geo.Common;
using Metaquotes.Geo.Data;

namespace Metaquotes.Geo.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBase database = DataBaseManager.Read(@"geobase.dat");
            //---
            Console.WriteLine($"Database loaded in {database.LoadTime} ms.");
            //---
            ISearchService searchService = new SearchService(database);
            //---
            IReadOnlyCollection<Location> locations = searchService.GetLocationsByCity("cit_Opyfu");
            //---
            foreach (Location location in locations)
            {
                Console.WriteLine(location);
            }
            //---
            Console.WriteLine();
            //---
            Location? ipLocation = searchService.GetLocationByIp(16287938);
            if (ipLocation.HasValue)
            {
                Console.WriteLine(ipLocation);
            }
            //---
            Console.ReadKey();
        }
    }
}