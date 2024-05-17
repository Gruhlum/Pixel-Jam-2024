using HexTecGames.GridBaseSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames
{
    public enum Orientation { Vertical, Horizontal }

    [System.Serializable]
    public class Path
    {
        public List<Tile> pathTiles = new List<Tile>();
        public enum Direction { Top, Bottom, Left, Right }
        

        private Direction direction;
        private Orientation Orientation
        {
            get
            {
                if (direction == Direction.Top || direction == Direction.Bottom)
                {
                    return Orientation.Vertical;
                }
                return Orientation.Horizontal;
            }
        }

        private BaseGrid grid;

        private int startIndex;
        private int centerIndex;
        private int endIndex;

        public Path(BaseGrid grid, List<Tile> tiles, Coord mapCenter)
        {
            this.grid = grid;
            pathTiles = tiles;
            int lowestX = tiles.Min(t => t.X);
            int lowestY = tiles.Min(t => t.Y);
            int highestX = tiles.Max(t => t.X);
            int highestY = tiles.Max(t => t.Y);

            if (highestX - lowestX > highestY - lowestY)
            {
                if (mapCenter.x > highestX)
                {
                    direction = Direction.Left;
                }
                else direction = Direction.Right;
            }
            else
            {
                if (mapCenter.y > highestY)
                {
                    direction = Direction.Bottom;
                }
                else direction = Direction.Top;
            }

            switch (direction)
            {
                case Direction.Top:
                    startIndex = highestY;
                    centerIndex = (highestY + lowestY) / 2;
                    endIndex = lowestY;
                    break;
                case Direction.Bottom:
                    startIndex = lowestY;
                    centerIndex = (highestY + lowestY) / 2;
                    endIndex = highestY;
                    break;
                case Direction.Left:
                    startIndex = highestX;
                    centerIndex = (highestX + lowestX) / 2;
                    endIndex = lowestX;
                    break;
                case Direction.Right:
                    startIndex = lowestX;
                    centerIndex = (highestX + lowestX) / 2;
                    endIndex = highestX;
                    break;
                default:
                    break;
            }
        }

        public bool IsEndTile(Tile tile)
        {            
            if (Orientation == Orientation.Vertical && tile.Center.y == endIndex)
            {
                return true;
            }
            else if (Orientation == Orientation.Horizontal && tile.Center.x == endIndex)
            {
                return true;
            }
            return false;
        }
        private Coord GetStartCoord(int index)
        {
            if (Orientation == Orientation.Vertical)
            {
                return new Coord(index, startIndex);
            }
            else return new Coord(startIndex, index);
        }

        public Tile GetNextTile(Coord current)
        {
            Coord bestCoord = GetNextCoord(current);
            if (grid.IsTileEmpty(bestCoord))
            {
                return grid.GetTile(bestCoord);
            }

            Tile result = GetBestSidewayTile(current);
            if (result != null)
            {
                return result;
            }

            return null;
        }
        private Tile GetBestSidewayTile(Coord current)
        {
            Tile result = GetBestSidewayTile(current, false);

            if (result != null)
            {
                return result;
            }
            return GetBestSidewayTile(current, true);
        }
        private Tile GetBestSidewayTile(Coord current, bool opposite)
        {
            Coord result = GetSidewayCoord(current, opposite);
            if (grid.IsTileEmpty(result))
            {
                return grid.GetTile(result);
            }
            else return null;
        }
        private Coord GetSidewayCoord(Coord current, bool opposite)
        {
            int multiplier = opposite ? 1 : 0;

            switch (direction)
            {
                case Direction.Top:
                    return current - Coord.left * multiplier;
                case Direction.Bottom:
                    return current - Coord.right * multiplier;
                case Direction.Left:
                    return current - Coord.up * multiplier;
                case Direction.Right:
                    return current - Coord.down * multiplier;
                default:
                    return current;
            }
        }
        public Coord GetNextCoord(Coord current)
        {
            switch (direction)
            {
                case Direction.Top:
                    return current - Coord.up;
                case Direction.Bottom:
                    return current - Coord.down;
                case Direction.Left:
                    return current - Coord.left;
                case Direction.Right:
                    return current - Coord.right;
                default:
                    return current;
            }
        }
        public Tile GetPathStartTile()
        {
            List<Tile> results = new List<Tile>();
            for (int i = 0; i < 3; i++)
            {
                results.Add( grid.GetTile(GetStartCoord(-1)));
                results.Add( grid.GetTile(GetStartCoord(0)));
                results.Add( grid.GetTile(GetStartCoord(1)));
            }
            if (results.Count == 0)
            {
                return null;
            }
            return results.Random();
        }
        public bool ContainsCoord(Coord coord)
        {
            if (pathTiles.Any(x => x.Center == coord))
            {
                return true;
            }
            return false;
        }

    }
}