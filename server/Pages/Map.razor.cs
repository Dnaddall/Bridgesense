using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using System.Transactions;
using Bridgesense.Data;
using System.Reflection.Metadata.Ecma335;

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

        public List<double> GetLatitude(Models.BridgesenseData.Bridge bridge)
        {

            List<double> latitudes = Context.Bridges
                               .Where(i => i.id == bridge.id)
                               .Select(i => i.latitude)
                               .ToList();
            return latitudes;
        }

        public List<double> GetLongitude(Models.BridgesenseData.Bridge bridge)
        {

            List<double> longitudes = Context.Bridges
                                .Where(i => i.id == bridge.id)
                               .Select(i => i.longitude)
                               .ToList();
            return longitudes;
        }
        public List<GoogleMapPosition> GetPosition()
        {
            List<GoogleMapPosition> positions = new List<GoogleMapPosition>();
            GoogleMapPosition[] posArray = new GoogleMapPosition[] { };

            for (int i = 0; i < posArray.Length; i++)
            {
                GoogleMapPosition item = posArray[i];
                foreach (var b in getBridgeResults)
                {
                    foreach (var (latitude, longitude) in GetLatitude(b).SelectMany(latitude => GetLongitude(b).Select(longitude => (latitude, longitude))))
                    {
                        positions.Add(new GoogleMapPosition() { Lat = latitude, Lng = longitude });
                    }
                }                
            }
            return positions;
        }
        
       

        
    }

}
