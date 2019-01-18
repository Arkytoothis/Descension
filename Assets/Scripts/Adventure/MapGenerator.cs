using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public static class MapGenerator
    {
        static bool initialized = false;

        public static void Initialize()
        {
            if (initialized == false)
            {
                initialized = true;

            }
        }

        public static MapData Generate()
        {
            float timeOfDay = Random.Range(0f, 1f);
            MapType mapType = (MapType)Random.Range(0, (int)MapType.Number);
            MapLocation mapLocation = (MapLocation)Random.Range(0, (int)MapLocation.Number);
            MapTheme mapTheme = (MapTheme)Random.Range(0, (int)MapTheme.Number);
            MapWeather mapWeather = (MapWeather)Random.Range(0, (int)MapWeather.Number);

            MapData mapData = new MapData(timeOfDay, mapType, mapLocation, mapTheme, mapWeather);
            return mapData;
        }
    }
}