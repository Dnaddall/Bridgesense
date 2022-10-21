using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using System.Transactions;
using Bridgesense.Data;

namespace Bridgesense.Pages
{
    public partial class MapComponent
    {
        BridgesenseDataContext Context
        {
            get
            {
                return this.context;
            }
        }
        private readonly BridgesenseDataContext context;

        public System.Collections.Generic.List<double?> GetLatitude(Models.BridgesenseData.Bridge bridge)
        {

            System.Collections.Generic.List<double?> latitudes = Context.Bridges
                               .Where(i => i.id == bridge.id)
                               .Select(i => i.latitude)
                               .ToList();
            foreach (double? latitude in latitudes)
            {

            }
            return latitudes;
        }

        public System.Collections.Generic.List<double?> GetLongitude(Models.BridgesenseData.Bridge bridge)
        {

            System.Collections.Generic.List<double?> longitudes = Context.Bridges
                               .Where(i => i.id == bridge.id)
                               .Select(i => i.longitude)
                               .ToList();
            return longitudes;
        }

    }

}
