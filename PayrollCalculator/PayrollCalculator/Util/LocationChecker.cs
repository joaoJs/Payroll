using System;
using System.Collections.Generic;

namespace PayrollCalculator.Util
{
    public class LocationChecker
    {
        private HashSet<string> Locations;

        public LocationChecker()
        {
            Locations = new HashSet<string> { "IRELAND", "ITALY", "GERMANY" };
        }

        public bool CheckLocation(String location) => Locations.Contains(location.ToUpper());
    }
}
