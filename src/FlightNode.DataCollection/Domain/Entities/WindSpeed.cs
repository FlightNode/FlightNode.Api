using System;

namespace FlightNode.DataCollection.Domain.Entities
{
    public class WindSpeed : EnumBase
    {

        private static WindSpeed _zeroToFive;

        public static WindSpeed ZeroToFive
        {
            get
            {
                return _zeroToFive ?? (_zeroToFive = new WindSpeed
                {
                    Id = 0,
                    Description = "0 - 5 MPH"
                });
            }
        }


        private static WindSpeed _fiveToTen;

        public static WindSpeed FiveToTen
        {
            get
            {
                return _fiveToTen ?? (_fiveToTen = new WindSpeed
                {
                    Id = 1,
                    Description = "5 - 10 MPH"
                });
            }
        }


        private static WindSpeed _tenToFifteen;

        public static WindSpeed TenToFifteen
        {
            get
            {
                return _tenToFifteen ?? (_tenToFifteen = new WindSpeed
                {
                    Id = 2,
                    Description = "10 - 15 MPH"
                });
            }
        }


        private static WindSpeed _moreThanFifteen;

        public static WindSpeed MoreThanFifteen
        {
            get
            {
                return _moreThanFifteen ?? (_moreThanFifteen = new WindSpeed
                {
                    Id = 3,
                    Description = "> 15 MPH"
                });
            }
        }
    }
}
