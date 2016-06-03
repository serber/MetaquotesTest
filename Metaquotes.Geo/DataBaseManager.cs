using System;
using System.Diagnostics;
using System.IO;
using Metaquotes.Geo.Data;

namespace Metaquotes.Geo
{
    public static class DataBaseManager
    {
        public static DataBase Read(string geoBasePath)
        {
            if (!File.Exists(geoBasePath))
            {
                throw new FileNotFoundException($"File {geoBasePath} not found");
            }
            //---
            DataBase dataBase;
            Stopwatch stopwatch = Stopwatch.StartNew(); 
            //---
            using (SimpleGeoBaseReader geoBaseReader = new SimpleGeoBaseReader(File.OpenRead(geoBasePath)))
            {
                Header header = geoBaseReader.ReadHeader();
                Range[] ranges = geoBaseReader.ReadIpRanges(header.Records);
                Location[] locations = geoBaseReader.ReadIpLocations(header.Records);
                uint[] indexes = geoBaseReader.ReadIndexes(header.Records);
                //---
                stopwatch.Stop();
                //---
                dataBase = new DataBase(header, ranges, locations, indexes, stopwatch.ElapsedMilliseconds);
            }
            //---
            Trace.WriteLine($"Database loaded in {stopwatch.ElapsedMilliseconds} ms.");
            //---
            return dataBase;
        }
    }
}