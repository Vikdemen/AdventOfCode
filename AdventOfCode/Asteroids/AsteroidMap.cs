using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Numerics;

namespace AdventOfCode.Asteroids
{
    public class AsteroidMap
    {
        //a set of every asteroid on map
        public HashSet<Vector2> AsteroidLocations { get; }

        //location of location with highest number of visible asteroids
        public (int x, int y) BestLocation => BestStationLocation();
        
        //populates the set
        public AsteroidMap(string[] mapData)
        {
            AsteroidLocations = MapParser.Parse(mapData);
        }
        
        //checks the number of asteroid for each locations and returns best
        private (int x, int y) BestStationLocation()
        {
            Vector2 bestLoc = AsteroidLocations.OrderByDescending(CountAsteroidsInLOS).First();
            return ((int)bestLoc.X, (int)bestLoc.Y);
        }

        //counts the asteroids visible from station
        private int CountAsteroidsInLOS(Vector2 station) => 
            AsteroidLocations.Where(location => location != station)
                .Count(location => InLOS(station, location));

        //creates a line between start and finish and checks if there are asteroids
        private bool InLOS(Vector2 station, Vector2 location)
        {
            HashSet<Vector2> line = Bresenham(station, location);
            line.Remove(station);
            line.Remove(location);
            return !line.Overlaps(AsteroidLocations);
        }

        //Bresenham's line algorithm is a line drawing algorithm that determines the points of an n-dimensional raster
        //that should be selected in order to form a close approximation to a straight line between two points.
        //the top-left is (0,0) such that pixel coordinates increase in the right and down directions
        //(e.g. that the pixel at (7,4) is directly above the pixel at (7,5))
        //and the pixel centers have integer coordinates.
        
        //It seems that Bresenham doesn't accurately represent the rules for "seeing" asteroids
        private HashSet<Vector2> Bresenham(Vector2 start, Vector2 end)
        {
            HashSet<Vector2> line = new HashSet<Vector2>();
            
            if (Math.Abs(end.Y - start.Y) < Math.Abs(end.X - start.X))
            {
                if (start.X > start.Y)
                    PlotLineLow(end, start);
                else
                    PlotLineLow(start, end);
            }
            else
            {
                if(start.Y > end.Y)
                    PlotLineHigh(end, start);
                else
                    PlotLineHigh(start, end);
            }

            void PlotLineLow(Vector2 v0, Vector2 v1)
            {
                var dx = v1.X - v0.X;
                var dy = v1.Y - v0.Y;
                var yi = 1;
                if (dy < 0)
                {
                    yi = -1;
                    dy = -dy;
                }

                var d = 2 * dx - dy;
                var y = v0.Y;
                for (var x = v0.X; x <= v1.X; x++)
                {
                    Plot(x, y);
                    if (d > 0)
                    {
                        y += yi;
                        d -= 2 * dx;
                    }
                    d += 2 * dy;
                }
            }

            void PlotLineHigh(Vector2 v0, Vector2 v1)
            {
                var dx = v1.X - v0.X;
                var dy = v1.Y - v0.Y;
                var xi = 1;
                if (dx < 0)
                {
                    xi = -1;
                    dx = -dx;
                }
                var d = 2 * dx - dy;
                var x = v0.X;
                for (var y = v0.Y; y <= v1.Y; y++)
                {
                    Plot(x, y);
                    if (d > 0)
                    {
                        x += xi;
                        d -= 2 * dy;
                    }
                    d += 2 * dx;
                }
            }

            void Plot(float x, float y) => line.Add(new Vector2(x,y));

            return line;
        }
    }
    
    public static class MapParser
    {
        public static HashSet<Vector2> Parse(string[] data)
        {
            int width = data[0].Length;
            int height = data.Length;
            var map = new HashSet<Vector2>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char location = data[y][x];
                    if (location == '#')
                        map.Add(new Vector2(x, y));
                }
            }
            return map;
        }
    }
}