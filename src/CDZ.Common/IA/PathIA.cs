using CDZ.Common.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CDZ.Common.IA
{
    public class PathIA
    {
        private static Point _begin = new Point(37, 37);
        private static Point _end = new Point(4, 37);

        private int[,] _map;
        private Queue<Point> _queue;
        private List<Path> _paths;

        public PathIA(int[,] map)
        {
            _queue = new Queue<Point>();
            _queue.Enqueue(_begin);
            _paths = new List<Path>
            {
                new Path
                {
                    Key = _begin,
                    CalculatedTimes = new List<double>
                    {
                        0
                    },
                    Points = new List<Point>
                    {
                        _begin
                    }
                }
            };
            _map = map;
        }

        private List<Point> MapNeighbours(Point origin)
        {
            var neighbours = new List<Point>();
            var mapLenght = _map.GetLength(0);

            if (new Point(origin.X, origin.Y + 1) is Point north && north.Y < mapLenght && _map[north.X, north.Y] != 14)
            {
                neighbours.Add(north);
            }
            if (new Point(origin.X, origin.Y - 1) is Point south && south.Y > 0 && _map[south.X, south.Y] != 14)
            {
                neighbours.Add(south);
            }
            if (new Point(origin.X + 1, origin.Y) is Point east && east.X < mapLenght && _map[east.X, east.Y] != 14)
            {
                neighbours.Add(east);
            }
            if (new Point(origin.X - 1, origin.Y) is Point west && west.X > 0 && _map[west.X, west.Y] != 14)
            {
                neighbours.Add(west);
            }

            return neighbours;
        }

        public double CalculateTime(Point point)
        {
            var terrain = _map[point.X, point.Y];

            switch (terrain)
            {
                case 14:
                    return 200;
                case 15:
                    return 1;
                case 16:
                    return 5;
                default:
                    return 0;
            }
        }

        public Path Find()
        {
            while (_queue.Count > 0)
            {
                var currentPoint = _queue.Dequeue();
                var currentPath = _paths.FirstOrDefault(path => path.Key == currentPoint);

                foreach (var neighborPoint in MapNeighbours(currentPoint))
                {
                    var time = CalculateTime(neighborPoint);
                    if (_paths.FirstOrDefault(path => path.Key.X == neighborPoint.X && path.Key.Y == neighborPoint.Y) is Path neighborPath)
                    {
                        if (neighborPath.GetTotalTime() > currentPath.GetTotalTime() + time)
                        {
                            neighborPath.CalculatedTimes = new List<double>(currentPath.CalculatedTimes)
                            {
                                time
                            };
                            neighborPath.Points = new List<Point>(currentPath.Points)
                            {
                                neighborPoint
                            };
                            _queue.Enqueue(neighborPoint);
                        }
                    }
                    else
                    {
                        _paths.Add(new Path
                        {
                            Key = neighborPoint,
                            CalculatedTimes = new List<double>(currentPath.CalculatedTimes)
                            {
                                time
                            },
                            Points = new List<Point>(currentPath.Points)
                            {
                                neighborPoint
                            }
                        });
                        _queue.Enqueue(neighborPoint);
                    }
                }
            }

            return _paths.FirstOrDefault(path => path.Key == _end);
        }
    }
}
