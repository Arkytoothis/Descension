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
        [SerializeField] MapType mapType;
        [SerializeField] MapLocation mapLocation;
        [SerializeField] MapTheme mapTheme;
        [SerializeField] MapWeather mapWeather;

        public MapData()
        {
            this.timeOfDay = 0f;
            this.mapType = MapType.None;
            this.mapLocation = MapLocation.None;
            this.mapTheme = MapTheme.None;
            this.mapWeather = MapWeather.None;
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
            this.timeOfDay = mapData.timeOfDay;
            this.mapType = mapData.mapType;
            this.mapLocation = mapData.mapLocation;
            this.mapTheme = mapData.mapTheme;
            this.mapWeather = mapData.mapWeather;
        }

        public string GetString()
        {
            return mapType.ToString() + " " + mapLocation.ToString() + " " + mapTheme.ToString() + " " + mapWeather.ToString();
        }
    }
}