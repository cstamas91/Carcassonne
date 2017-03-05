using CarcassonneServer.Model.Representation.SubAreas;
using System;
using System.Collections.Generic;

namespace CarcassonneServer.Model.Representation.GameItems
{
    public static class TileFactory
    {
        private static Dictionary<TileType, Func<Tile>> tiles;

        static TileFactory()
        {
            tiles = new Dictionary<TileType, Func<Tile>>()
            {
                { TileType.CityWithFields, GetCityWithFields },
                { TileType.CityWithSouthField, GetCityWithSouthField },
                { TileType.CityWithSouthGate, GetCityWithSouthGate },
                { TileType.CornerCity, GetCornerCity },
                { TileType.CornerCItyWihRightElbowRoad, GetCornerCItyWihRightElbowRoad },
                { TileType.CrossRoads, GetCrossRoads },
                { TileType.FieldWithCities, GetFieldWithCities },
                { TileType.FullCity, GetFullCity },
                { TileType.LeftElbowRoad, GetLeftElbowRoad },
                { TileType.LShapeCity, GetLShapeCity },
                { TileType.Monastery, GetMonastery },
                { TileType.MonastreyWithRoad, GetMonastreyWithRoad },
                { TileType.SingleRoad, GetSingleRoad },
                { TileType.TRoad, GetTRoad },
                { TileType.UpperCity, GetUpperCity },
                { TileType.UpperCityWithHorizontalRoad, GetUpperCityWithHorizontalRoad },
                { TileType.UpperCityWithLeftElbowRoad, GetUpperCityWithLeftElbowRoad },
                { TileType.UpperCityWithRightElbowRoad, GetUpperCityWithRightElbowRoad },
                { TileType.UpperCityWithTRoad, GetUpperCityWithTRoad }
            };
        }

        public static Tile Get(TileType tileType)
        {
            return tiles[tileType]();
        }

        private static Tile GetMonastreyWithRoad()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Field,
                    Direction.UpLeft, Direction.Up,
                    Direction.UpRight, Direction.RightUp, Direction.Right,
                    Direction.RightDown, Direction.DownRight, Direction.DownLeft,
                    Direction.LeftDown, Direction.Left, Direction.LeftUp),
                BaseSubArea.Get(AreaType.Road,
                    Direction.Down)
            }, true);
        }

        private static Tile GetMonastery()
        {
            return new Tile(
                new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Field, Direction.UpLeft, Direction.Up,
                    Direction.UpRight, Direction.RightUp, Direction.Right,
                    Direction.RightDown, Direction.DownRight, Direction.Down,
                    Direction.DownLeft, Direction.LeftDown, Direction.Left,
                    Direction.LeftUp)
            }, true);
        }

        private static Tile GetFullCity()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.UpLeft, Direction.Up,
                    Direction.UpRight, Direction.RightUp, Direction.Right,
                    Direction.RightDown, Direction.DownRight, Direction.Down,
                    Direction.DownLeft, Direction.LeftDown, Direction.Left,
                    Direction.LeftUp)
            });
        }

        private static Tile GetUpperCityWithHorizontalRoad()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.Up, Direction.UpRight, Direction.UpLeft),
                BaseSubArea.Get(AreaType.Road, Direction.Left, Direction.Right ),
                BaseSubArea.Get(AreaType.Field, Direction.LeftUp, Direction.RightUp),
                BaseSubArea.Get(AreaType.Field, Direction.LeftDown, Direction.RightDown,
                    Direction.Down, Direction.DownLeft, Direction.DownRight )
            });
        }

        private static Tile GetUpperCity()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.Up, Direction.UpRight, Direction.UpLeft),
                BaseSubArea.Get(AreaType.Field, Direction.Left, Direction.Right, Direction.LeftUp,
                    Direction.RightUp, Direction.LeftDown, Direction.RightDown, Direction.Down,
                    Direction.DownLeft, Direction.DownRight)
            });
        }
        private static Tile GetCityWithFields()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Field, Direction.UpLeft, Direction.UpRight, Direction.Up),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft, Direction.DownRight, Direction.Down),
                BaseSubArea.Get(AreaType.Castle, Direction.LeftDown, Direction.LeftUp, Direction.Left,
                    Direction.RightDown, Direction.RightUp, Direction.Right)
            });
        }

        private static Tile GetFieldWithCities()
        {
            return new Tile(new List<ISubArea>()
            {
                BaseSubArea.Get(AreaType.Field, Direction.UpLeft, Direction.UpRight, Direction.Up,
                    Direction.DownLeft, Direction.DownRight, Direction.Down),
                BaseSubArea.Get(AreaType.Castle, Direction.LeftDown, Direction.LeftUp, Direction.Left),
                BaseSubArea.Get(AreaType.Castle, Direction.RightDown, Direction.RightUp, Direction.Right)
            });
        }

        private static Tile GetLShapeCity()
        {
            return new Tile(new List<ISubArea>()
            {
                BaseSubArea.Get(AreaType.Field, Direction.LeftDown, Direction.LeftUp, Direction.Left,
                    Direction.DownLeft, Direction.DownRight, Direction.Down),
                BaseSubArea.Get(AreaType.Castle, Direction.RightDown, Direction.RightUp, Direction.Right),
                BaseSubArea.Get(AreaType.Castle, Direction.Up, Direction.UpLeft, Direction.UpRight)
            });
        }

        private static Tile GetUpperCityWithRightElbowRoad()
        {
            return new Tile(new List<ISubArea>(){
                BaseSubArea.Get(AreaType.Castle, Direction.UpLeft, Direction.UpRight, Direction.Up),
                BaseSubArea.Get(AreaType.Field, Direction.Left, Direction.LeftUp, Direction.LeftDown,
                    Direction.DownLeft, Direction.RightUp),
                BaseSubArea.Get(AreaType.Road, Direction.Down, Direction.Right),
                BaseSubArea.Get(AreaType.Field, Direction.DownRight, Direction.RightDown)
            });
        }

        private static Tile GetUpperCityWithLeftElbowRoad()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.UpLeft, Direction.UpRight, Direction.Up),
                BaseSubArea.Get(AreaType.Field, Direction.Right, Direction.RightUp, Direction.RightDown,
                    Direction.DownRight, Direction.RightUp),
                BaseSubArea.Get(AreaType.Road, Direction.Down, Direction.Left),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft, Direction.LeftDown)
            });
        }

        private static Tile GetUpperCityWithTRoad()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.UpLeft,Direction.UpRight, Direction.Up),
                BaseSubArea.Get(AreaType.Road, Direction.Left),
                BaseSubArea.Get(AreaType.Road, Direction.Right),
                BaseSubArea.Get(AreaType.Road, Direction.Down ),
                BaseSubArea.Get(AreaType.Field, Direction.RightUp, Direction.LeftUp),
                BaseSubArea.Get(AreaType.Field, Direction.LeftDown, Direction.DownLeft ),
                BaseSubArea.Get(AreaType.Field, Direction.RightDown, Direction.DownLeft )
            });
        }

        private static Tile GetCornerCity()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.UpLeft, Direction.UpRight, Direction.Up,
                    Direction.LeftDown, Direction.LeftUp, Direction.Left),
                BaseSubArea.Get(AreaType.Field, Direction.RightDown, Direction.RightUp, Direction.Right,
                    Direction.DownLeft, Direction.DownRight, Direction.Down)
            });
        }

        private static Tile GetCornerCItyWihRightElbowRoad()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.Up, Direction.UpLeft, Direction.UpRight,
                    Direction.LeftDown, Direction.Left, Direction.LeftUp),
                BaseSubArea.Get(AreaType.Road, Direction.Down, Direction.Right ),
                BaseSubArea.Get(AreaType.Field, Direction.RightDown, Direction.DownRight),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft, Direction.RightUp)
            });
        }

        private static Tile GetCityWithSouthField()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.LeftDown, Direction.LeftUp, Direction.Left,
                    Direction.RightDown, Direction.RightUp, Direction.Right),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft, Direction.Down, Direction.DownRight)
            });
        }

        private static Tile GetCityWithSouthGate()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle,
                    Direction.LeftDown, Direction.LeftUp, Direction.Left,
                    Direction.RightDown, Direction.Right, Direction.RightUp),
                BaseSubArea.Get(AreaType.Road, Direction.Left),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft),
                BaseSubArea.Get(AreaType.Field, Direction.DownRight)
            });
        }

        private static Tile GetSingleRoad()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Road, Direction.Down, Direction.Up),
                BaseSubArea.Get(AreaType.Field, Direction.UpRight, Direction.DownRight,
                    Direction.RightDown, Direction.Right, Direction.RightUp),
                BaseSubArea.Get(AreaType.Field, Direction.UpLeft, Direction.DownLeft,
                    Direction.LeftUp, Direction.Left, Direction.LeftDown)
            });
        }

        private static Tile GetLeftElbowRoad()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Road, Direction.Down, Direction.Left),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft, Direction.LeftDown),
                BaseSubArea.Get(AreaType.Field, Direction.DownRight, Direction.LeftUp,
                    Direction.RightDown, Direction.Right, Direction.RightUp)
            });
        }

        private static Tile GetTRoad()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get( AreaType.Road, Direction.Left),
                BaseSubArea.Get(AreaType.Road,Direction.Right),
                BaseSubArea.Get( AreaType.Road, Direction.Down),
                BaseSubArea.Get(AreaType.Field, Direction.RightDown, Direction.DownRight),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft, Direction.LeftDown),
                BaseSubArea.Get(AreaType.Road, Direction.RightUp, Direction.LeftUp, Direction.UpLeft, Direction.Up, Direction.UpRight),
            });
        }

        private static Tile GetCrossRoads()
        {
            return new Tile(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Road, Direction.Right),
                BaseSubArea.Get(AreaType.Road, Direction.Down),
                BaseSubArea.Get(AreaType.Road, Direction.Left),
                BaseSubArea.Get(AreaType.Road, Direction.Up),
                BaseSubArea.Get(AreaType.Field, Direction.LeftUp, Direction.UpLeft),
                BaseSubArea.Get(AreaType.Field, Direction.RightUp, Direction.UpRight),
                BaseSubArea.Get(AreaType.Field, Direction.RightDown, Direction.DownRight),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft, Direction.LeftDown),
            });
        }
    }

    public enum TileType
    {
        MonastreyWithRoad,
        Monastery,
        FullCity,
        UpperCityWithHorizontalRoad,
        UpperCity,
        CityWithFields,
        FieldWithCities,
        LShapeCity,
        UpperCityWithRightElbowRoad,
        UpperCityWithLeftElbowRoad,
        UpperCityWithTRoad,
        CornerCity,
        CornerCItyWihRightElbowRoad,
        CityWithSouthField,
        CityWithSouthGate,
        SingleRoad,
        LeftElbowRoad,
        TRoad,
        CrossRoads
    }
}
