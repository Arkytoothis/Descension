using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    [System.Serializable]
    public class MapData
    {
        [SerializeField] float timeOfDay;
        public float TimeOfDay { get { return timeOfDay; } }

        [SerializeField] MapType mapType;
        public MapType MapType { get { return mapType; } }

        [SerializeField] MapLocation mapLocation;
        public MapLocation MapLocation { get { return mapLocation; } }

        [SerializeField] MapTheme mapTheme;
        public MapTheme MapTheme { get { return mapTheme; } }

        [SerializeField] MapWeather mapWeather;
        public MapWeather MapWeather { get { return mapWeather; } }

        public MapData()
        {
            timeOfDay = 0f;
            mapType = MapType.None;
            mapLocation = MapLocation.None;
            mapTheme = MapTheme.None;
            mapWeather = MapWeather.None;
        }

        public MapData(float timeOfDay, MapType mapType, MapLocation mapLocation, MapTheme mapTheme, MapWeather mapWeather)
        {
            this.timeOfDay = timeOfDay;
            this.mapType = mapType;
            this.mapLocation = mapLocation;
            this.mapTheme = mapTheme;
            this.mapWeather = mapWeather;
        }

        public MapData(MapData mapData)
        {
            timeOfDay = mapData.timeOfDay;
            mapType = mapData.mapType;
            mapLocation = mapData.mapLocation;
            mapTheme = mapData.mapTheme;
            mapWeather = mapData.mapWeather;
        }

        public string GetString()
        {
            return mapType.ToString() + " " + mapLocation.ToString() + " " + mapTheme.ToString() + " " + mapWeather.ToString();
        }
    }
}