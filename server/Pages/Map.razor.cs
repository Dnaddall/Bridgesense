using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using System.Transactions;

namespace Bridgesense.Pages
{
    public partial class MapComponent
    {
       public double a = new Maplatitude();
       public  double b = new Maplongitude();

        public class Maplatitude
        {
            public double? latitude { get; set; }

            public static implicit operator double(Maplatitude v)
            {
                throw new NotImplementedException();
            }
        }
        public class Maplongitude
        {
            public double? longitude { get; set; }

            public static implicit operator double(Maplongitude v)
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<Maplatitude> GetMaplatitude()
        {
            var Coords = BridgesenseDataContext.Bridges
                         .Select(s => new Maplatitude()
                         {
                             latitude = s.latitude
                         });


            return Coords;
        }

        public IEnumerable<Maplongitude> GetMaplongitude()
        {
            var Coords = BridgesenseDataContext.Bridges
                         .Select(s => new Maplongitude()
                         {
                             longitude = s.longitude
                         });


            return Coords;
        }

    }

}
