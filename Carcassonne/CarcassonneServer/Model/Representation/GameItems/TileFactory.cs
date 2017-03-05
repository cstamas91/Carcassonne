using CarcassonneServer.Model.Representation.SubAreas;
using System.Collections.Generic;

namespace CarcassonneServer.Model.Representation.GameItems
{
    public class MonastreyWithRoad : Tile
    {
        public MonastreyWithRoad() 
            : base(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Field, 
                    Direction.UpLeft, Direction.Up,
                    Direction.UpRight, Direction.RightUp, Direction.Right,
                    Direction.RightDown, Direction.DownRight, Direction.DownLeft,
                    Direction.LeftDown, Direction.Left, Direction.LeftUp),
                BaseSubArea.Get(AreaType.Road, 
                    Direction.Down)
            }) { isMonastery = true; }
    }

    public class Monastery : Tile
    {
        public Monastery() : base(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Field, Direction.UpLeft, Direction.Up,
                    Direction.UpRight, Direction.RightUp, Direction.Right,
                    Direction.RightDown, Direction.DownRight, Direction.Down,
                    Direction.DownLeft, Direction.LeftDown, Direction.Left,
                    Direction.LeftUp)
            }) { isMonastery = true; }
    }

    public class FullCity : Tile
    {
        public FullCity() :base (new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.UpLeft, Direction.Up,
                    Direction.UpRight, Direction.RightUp, Direction.Right,
                    Direction.RightDown, Direction.DownRight, Direction.Down,
                    Direction.DownLeft, Direction.LeftDown, Direction.Left,
                    Direction.LeftUp)
            }) { }
    }

    public class UpperCityWithHorizontalRoad : Tile
    {
        public UpperCityWithHorizontalRoad()
            : base(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.Up, Direction.UpRight, Direction.UpLeft),
                BaseSubArea.Get(AreaType.Road, Direction.Left, Direction.Right ),
                BaseSubArea.Get(AreaType.Field, Direction.LeftUp, Direction.RightUp),
                BaseSubArea.Get(AreaType.Field, Direction.LeftDown, Direction.RightDown,
                    Direction.Down, Direction.DownLeft, Direction.DownRight ) 
            }) { }
    }

    public class UpperCity : Tile
    {
        public UpperCity() 
            : base(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.Up, Direction.UpRight, Direction.UpLeft),
                BaseSubArea.Get(AreaType.Field, Direction.Left, Direction.Right, Direction.LeftUp,
                    Direction.RightUp, Direction.LeftDown, Direction.RightDown, Direction.Down,
                    Direction.DownLeft, Direction.DownRight)
            }) { }
    }

    public class CityWithFields : Tile
    {
        public CityWithFields() 
            :base(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Field, Direction.UpLeft, Direction.UpRight, Direction.Up),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft, Direction.DownRight, Direction.Down),
                BaseSubArea.Get(AreaType.Castle, Direction.LeftDown, Direction.LeftUp, Direction.Left,
                    Direction.RightDown, Direction.RightUp, Direction.Right)
            }) { }
    }

    public class FieldWithCities : Tile
    {
        public FieldWithCities()
            : base (new List<ISubArea>()
            {
                BaseSubArea.Get(AreaType.Field, Direction.UpLeft, Direction.UpRight, Direction.Up,
                    Direction.DownLeft, Direction.DownRight, Direction.Down),
                BaseSubArea.Get(AreaType.Castle, Direction.LeftDown, Direction.LeftUp, Direction.Left),
                BaseSubArea.Get(AreaType.Castle, Direction.RightDown, Direction.RightUp, Direction.Right)
            }) { }
    }

    public class LShapeCity : Tile
    {
        public LShapeCity()
            : base (new List<ISubArea>()
            {
                BaseSubArea.Get(AreaType.Field, Direction.LeftDown, Direction.LeftUp, Direction.Left,
                    Direction.DownLeft, Direction.DownRight, Direction.Down),
                BaseSubArea.Get(AreaType.Castle, Direction.RightDown, Direction.RightUp, Direction.Right),
                BaseSubArea.Get(AreaType.Castle, Direction.Up, Direction.UpLeft, Direction.UpRight)
            }) { }
    }

    public class UpperCityWithRightElbowRoad : Tile
    {
        public UpperCityWithRightElbowRoad()
            : base (new List<ISubArea>(){
                BaseSubArea.Get(AreaType.Castle, Direction.UpLeft, Direction.UpRight, Direction.Up),
                BaseSubArea.Get(AreaType.Field, Direction.Left, Direction.LeftUp, Direction.LeftDown,
                    Direction.DownLeft, Direction.RightUp),
                BaseSubArea.Get(AreaType.Road, Direction.Down, Direction.Right),
                BaseSubArea.Get(AreaType.Field, Direction.DownRight, Direction.RightDown)
            }) { }
    }

    public class UpperCityWithLeftElbowRoad : Tile
    {
        public UpperCityWithLeftElbowRoad() 
            : base (new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.UpLeft, Direction.UpRight, Direction.Up),
                BaseSubArea.Get(AreaType.Field, Direction.Right, Direction.RightUp, Direction.RightDown,
                    Direction.DownRight, Direction.RightUp),
                BaseSubArea.Get(AreaType.Road, Direction.Down, Direction.Left),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft, Direction.LeftDown)
            }) { }
    }

    public class UpperCityWithTRoad : Tile
    {
        public UpperCityWithTRoad()
            : base(new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.UpLeft,Direction.UpRight, Direction.Up),
                BaseSubArea.Get(AreaType.Road, Direction.Left),
                BaseSubArea.Get(AreaType.Road, Direction.Right),
                BaseSubArea.Get(AreaType.Road, Direction.Down ),
                BaseSubArea.Get(AreaType.Field, Direction.RightUp, Direction.LeftUp),
                BaseSubArea.Get(AreaType.Field, Direction.LeftDown, Direction.DownLeft ),
                BaseSubArea.Get(AreaType.Field, Direction.RightDown, Direction.DownLeft )
            }) { }
    }
    public class CornerCity : Tile
    {
        public CornerCity()
            : base (new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.UpLeft, Direction.UpRight, Direction.Up,
                    Direction.LeftDown, Direction.LeftUp, Direction.Left),
                BaseSubArea.Get(AreaType.Field, Direction.RightDown, Direction.RightUp, Direction.Right,
                    Direction.DownLeft, Direction.DownRight, Direction.Down)
            }) { }
    }

    public class CornerCItyWihRightElbowRoad : Tile
    {
        public CornerCItyWihRightElbowRoad()
            : base (new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.Up, Direction.UpLeft, Direction.UpRight,
                    Direction.LeftDown, Direction.Left, Direction.LeftUp),
                BaseSubArea.Get(AreaType.Road, Direction.Down, Direction.Right ),
                BaseSubArea.Get(AreaType.Field, Direction.RightDown, Direction.DownRight),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft, Direction.RightUp)
            }) { }
    }

    public class CityWithSouthField : Tile
    {
        public CityWithSouthField()
            : base (new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle, Direction.LeftDown, Direction.LeftUp, Direction.Left,
                    Direction.RightDown, Direction.RightUp, Direction.Right),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft, Direction.Down, Direction.DownRight)
            }) { }
    }

    public class CityWithSouthGate : Tile
    {
        public CityWithSouthGate()
            : base (new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Castle,
                    Direction.LeftDown, Direction.LeftUp, Direction.Left,
                    Direction.RightDown, Direction.Right, Direction.RightUp),
                BaseSubArea.Get(AreaType.Road, Direction.Left),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft),
                BaseSubArea.Get(AreaType.Field, Direction.DownRight)
            }) { }
    }

    public class SingleRoad : Tile
    {
        public SingleRoad() 
            : base (new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Road, Direction.Down, Direction.Up),
                BaseSubArea.Get(AreaType.Field, Direction.UpRight, Direction.DownRight,
                    Direction.RightDown, Direction.Right, Direction.RightUp),
                BaseSubArea.Get(AreaType.Field, Direction.UpLeft, Direction.DownLeft,
                    Direction.LeftUp, Direction.Left, Direction.LeftDown)
            }) { }
    }

    public class LeftElbowRoad : Tile
    {
        public LeftElbowRoad() 
            : base (new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Road, Direction.Down, Direction.Left),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft, Direction.LeftDown),
                BaseSubArea.Get(AreaType.Field, Direction.DownRight, Direction.LeftUp,
                    Direction.RightDown, Direction.Right, Direction.RightUp)
            }) { }
    }

    public class TRoad : Tile
    {
        public TRoad()
            : base (new List<ISubArea>() {
                BaseSubArea.Get( AreaType.Road, Direction.Left),
                BaseSubArea.Get(AreaType.Road,Direction.Right),
                BaseSubArea.Get( AreaType.Road, Direction.Down),
                BaseSubArea.Get(AreaType.Field, Direction.RightDown, Direction.DownRight),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft, Direction.LeftDown),
                BaseSubArea.Get(AreaType.Road, Direction.RightUp, Direction.LeftUp, Direction.UpLeft, Direction.Up, Direction.UpRight),
            }) { }
    }

    public class CrossRoads : Tile
    {
        public CrossRoads() : 
            base (new List<ISubArea>() {
                BaseSubArea.Get(AreaType.Road, Direction.Right),
                BaseSubArea.Get(AreaType.Road, Direction.Down),
                BaseSubArea.Get(AreaType.Road, Direction.Left),
                BaseSubArea.Get(AreaType.Road, Direction.Up),
                BaseSubArea.Get(AreaType.Field, Direction.LeftUp, Direction.UpLeft),
                BaseSubArea.Get(AreaType.Field, Direction.RightUp, Direction.UpRight),
                BaseSubArea.Get(AreaType.Field, Direction.RightDown, Direction.DownRight),
                BaseSubArea.Get(AreaType.Field, Direction.DownLeft, Direction.LeftDown),
            }) { }
    }
}
