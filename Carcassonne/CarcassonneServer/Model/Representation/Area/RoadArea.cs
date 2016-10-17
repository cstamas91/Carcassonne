using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Area
{
    public class RoadArea : BaseArea
    {
        public override AreaType AreaType { get { return AreaType.Road; } }

        public override bool IsFinished { get { return EvaluateIsFinished(); } }

        /// <summary>
        /// Alapértelmezett konstruktor.
        /// </summary>
        public RoadArea() : base() { }

        public RoadArea(SubArea subArea)
            : base(subArea)
        {

        }
        /// <summary>
        /// Mező hozzáadása területhez.
        /// Ha a mező nem szomszédos az úttal, kivételt dobunk.
        /// * Ellenőrizzük, hogy a mezők valóban egymás mellé helyezhetők-e.
        /// * Megállapítjuk, hogy milyen cédulát kap az út.
        /// * Ellenőrizzük, hogy a céldulákat kezelni kell-e.
        /// </summary>
        /// <param name="subArea">Mező amit hozzá akarunk adni a területhez.</param>
        public override void AddSubArea(SubArea subArea)
        {
            base.AddSubArea(subArea);            
        }

        /// <summary>
        /// Beállítja a kapott mező megfelelő oldalak GUIDjainak a konstrukció guidját.
        /// </summary>
        /// <param name="element">A menedzselendő mező.</param>
        private void ManageGuids(Tile element, params ConnectingPoint[] sideDirections)
        {
            throw new NotImplementedException();
        }

        public override void AddMeeple(Meeple meeple, SubArea subArea)
        {
            if (meeples.Count > 0)
                throw new InvalidOperationException();

            meeples.Add(meeple);
        }

        protected override bool EvaluateIsFinished()
        {
            //TODO: !!!!!!!!!Akkor bezárt egy terület, ha minden hozzá tartozó alterületre egyenként igaz, hogy az alterület oldali ebbe a területbe tartozó alterülettel érintkeznek.
            throw new NotImplementedException();
        }

        protected override bool IsNeighbourTo(Position element)
        {
            throw new NotImplementedException();
        }

        protected override bool IsNeighbourTo(BaseArea area)
        {
            throw new NotImplementedException();
        }

        public override ConnectingPoint NeighborDirection(Position other)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Két út összeépítésére szolgáló eljárás.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Hamissal, ha a két út nem szomszédos, egyébként igazzal, ha az összeolvasztás sikeres.</returns>
        public override BaseArea Merge(BaseArea other)
        {
            throw new NotImplementedException();
        }
    }

    public static class Extensions
    {
        public static IEnumerable<T> GetMemberEnumeration<T>()
            where T : struct
        {
            return Enum.GetValues(typeof(T)) as IEnumerable<T>;
        }
    }
}
