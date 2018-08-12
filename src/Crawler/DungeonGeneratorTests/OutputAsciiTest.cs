using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DungeonGenerators;
using DungeonGenerators.Pathfinding;
using Xunit;
using Xunit.Abstractions;

namespace DungeonGeneratorTests
{
    public class OutputAsciiTest
    {
        private readonly ITestOutputHelper output;

        public OutputAsciiTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void PrintAscii()
        {
            DungeonGenerator dungeonGenerator = new DungeonGenerator(60, 30, 2, 4);

            for (var i = 0; i < 1000; i++) {
                dungeonGenerator.Generate();
                PrintAsciiMap(dungeonGenerator.Map);
            }

        }

        private void PrintAsciiMap(Tile[][] map)
        {
            output.WriteLine(DrawAsciiMap(map));
        }

        private string DrawAsciiMap(Tile[][] map)
        {
            char[][] charMap = new char[map.Length][];

            for(int x = 0; x < map.Count(); x++)
            {
                charMap[x] = new char[map[x].Length];

                for(int y = 0; y < map[x].Count(); y++)
                {
                    if (map[x][y] == Tile.Floor)
                    {
                        charMap[x][y] = '.';
                    }
                    else if (map[x][y] == Tile.Wall)
                    {
                        charMap[x][y] = '#';
                    }
                    else if (map[x][y] == Tile.Mob)
                    {
                        charMap[x][y] = 'M';
                    }
                    else
                    {
                        charMap[x][y] = '/';
                    }
                }
            }

            string result = string.Empty;

            for (int y = 0; y < charMap[0].Length; y++)
            {
                string line = String.Empty;

                for (int x = 0; x < charMap.Length; x++)
                {
                    line += charMap[x][y];
                }

                result += line + Environment.NewLine;
            }

            return result;
        }
    }
}
