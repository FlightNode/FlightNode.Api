using System;

namespace FlightNode.DataCollection.Domain.Entities
{
    public class WindDirection : EnumBase
    {

        private static WindDirection _north;

        public static WindDirection North
        {
            get
            {
                return _north ?? (_north = new WindDirection
                {
                    Id = 0,
                    Description = "North"
                });
            }
        }


        private static WindDirection _northEast;

        public static WindDirection NorthEast
        {
            get
            {
                return _northEast ?? (_northEast = new WindDirection
                {
                    Id = 1,
                    Description = "Northeast"
                });
            }
        }


        private static WindDirection _east;

        public static WindDirection East
        {
            get
            {
                return _east ?? (_east = new WindDirection
                {
                    Id = 2,
                    Description = "East"
                });
            }
        }


        private static WindDirection _southEast;

        public static WindDirection SouthEast
        {
            get
            {
                return _southEast ?? (_southEast = new WindDirection
                {
                    Id = 3,
                    Description = "Southeast"
                });
            }
        }


        private static WindDirection _south;

        public static WindDirection South
        {
            get
            {
                return _south ?? (_south = new WindDirection
                {
                    Id = 3,
                    Description = "South"
                });
            }
        }


        private static WindDirection _southWest;

        public static WindDirection SouthWest
        {
            get
            {
                return _southWest ?? (_southWest = new WindDirection
                {
                    Id = 3,
                    Description = "Southwest"
                });
            }
        }


        private static WindDirection _west;

        public static WindDirection West
        {
            get
            {
                return _west ?? (_west = new WindDirection
                {
                    Id = 3,
                    Description = "West"
                });
            }
        }


        private static WindDirection _northWest;

        public static WindDirection NorthWest
        {
            get
            {
                return _northWest ?? (_northWest = new WindDirection
                {
                    Id = 3,
                    Description = "Northwest"
                });
            }
        }
    }
}
