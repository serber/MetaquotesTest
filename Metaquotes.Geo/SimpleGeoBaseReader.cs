using System;
using System.IO;
using Metaquotes.Geo.Common;
using Metaquotes.Geo.Data;

namespace Metaquotes.Geo
{
    public class SimpleGeoBaseReader : IGeoBaseReader, IDisposable
    {
        private readonly BinaryReader _binaryReader;

        public SimpleGeoBaseReader(Stream stream)
        {
            _binaryReader = new BinaryReader(stream);
        }

        public Header ReadHeader()
        {
            ThrowIfCantReadStream();
            //---
            _binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
            //---
            Header header;
            //---
            header.Version = _binaryReader.ReadInt32();
            header.Name = _binaryReader.ReadChars(32);
            header.Date = _binaryReader.ReadUInt64();
            header.Records = _binaryReader.ReadInt32();
            header.OffsetRanges = _binaryReader.ReadUInt32();
            header.OffsetCities = _binaryReader.ReadUInt32();
            header.OffsetLocations = _binaryReader.ReadUInt32();
            //---
            return header;
        }

        public Range[] ReadIpRanges(int records)
        {
            ThrowIfCantReadStream();
            //---
            if (_binaryReader.BaseStream.Position != Constants.HeaderSize)
            {
                _binaryReader.BaseStream.Seek(Constants.HeaderSize, SeekOrigin.Begin);
            }
            //---
            Range[] ranges = new Range[records];
            //---
            for (int i = 0; i < records; i++)
            {
                ranges[i].IpFrom = _binaryReader.ReadUInt64();
                ranges[i].IpTo = _binaryReader.ReadUInt64();
                ranges[i].Index = _binaryReader.ReadUInt32();
            }
            //---
            return ranges;
        }

        public Location[] ReadIpLocations(int records)
        {
            ThrowIfCantReadStream();
            //---
            int offset = Constants.RangesSize * records + Constants.HeaderSize;
            if (_binaryReader.BaseStream.Position != offset)
            {
                _binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
            }
            Location[] locations = new Location[records];
            //---
            for (int i = 0; i < records; i++)
            {
                locations[i].Country = _binaryReader.ReadChars(8);
                locations[i].Region = _binaryReader.ReadChars(12);
                locations[i].Postal = _binaryReader.ReadChars(12);
                locations[i].City = _binaryReader.ReadChars(24);
                locations[i].Organization = _binaryReader.ReadChars(32);
                locations[i].Latitude = _binaryReader.ReadSingle();
                locations[i].Longitude = _binaryReader.ReadSingle();
            }
            //---
            return locations;
        }

        public uint[] ReadIndexes(int records)
        {
            ThrowIfCantReadStream();
            //---
            int offset = Constants.LocationsSize * records + Constants.RangesSize * records + Constants.HeaderSize;
            if (_binaryReader.BaseStream.Position != offset)
            {
                _binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
            }
            //---
            uint[] indexes = new uint[records];
            //---
            for (int i = 0; i < records; i++)
            {
                indexes[i] = _binaryReader.ReadUInt32();
            }
            //---
            return indexes;
        }

        public void Dispose()
        {
            _binaryReader.Dispose();
        }

        private void ThrowIfCantReadStream()
        {
            if (!_binaryReader.BaseStream.CanRead)
            {
                throw new Exception("Can't read database");
            }
        }
    }
}