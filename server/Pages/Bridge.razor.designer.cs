using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Bridgesense.Models.BridgesenseData;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace Bridgesense.Pages
{
    public partial class BridgeComponent : ComponentBase
    {


        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected BridgesenseDataService BridgesenseData { get; set; }
        protected RadzenDataGrid<Bridgesense.Models.BridgesenseData.Bridge> grid0;
        protected RadzenDataGrid<Bridgesense.Models.BridgesenseData.Sensor> grid1;
        protected RadzenDataGrid<Bridgesense.Models.BridgesenseData.SensorEventCount> grid2;
        protected RadzenDataGrid<Bridgesense.Models.BridgesenseData.Bridgestat> grid3;
        protected RadzenDataGrid<Bridgesense.Models.BridgesenseData.Bridgelog> grid4;

        IEnumerable<Models.BridgesenseData.Bridgelog> _getBridgelogResult;
        protected IEnumerable<Bridgesense.Models.BridgesenseData.Bridgelog> getBridgelogResult
        {
            get
            {
                return _getBridgelogResult;
            }
            set
            {
                if (!object.Equals(_getBridgelogResult, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = "getBridgelogResult", NewValue = value, OldValue = _getBridgelogResult };
                    _getBridgelogResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }





        IEnumerable<Bridgesense.Models.BridgesenseData.Bridge> _getBridgesResult;
        protected IEnumerable<Bridgesense.Models.BridgesenseData.Bridge> getBridgesResult
        {
            get
            {
                return _getBridgesResult;
            }
            set
            {
                if (!object.Equals(_getBridgesResult, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = "getBridgesResult", NewValue = value, OldValue = _getBridgesResult };
                    _getBridgesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Bridgesense.Models.BridgesenseData.Bridgestat> _getBridgestatsResult;
        protected IEnumerable<Bridgesense.Models.BridgesenseData.Bridgestat> getBridgestatsResult
        {
            get
            {
                return _getBridgestatsResult;
            }
            set
            {
                if (!object.Equals(_getBridgestatsResult, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = "getBridgestatsResult", NewValue = value, OldValue = _getBridgestatsResult };
                    _getBridgestatsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Bridgesense.Models.BridgesenseData.SensorEventCount> _getSensorEventCountsResult;
        protected IEnumerable<Bridgesense.Models.BridgesenseData.SensorEventCount> getSensorEventCountsResult
        {
            get
            {
                return _getSensorEventCountsResult;
            }
            set
            {
                if (!object.Equals(_getSensorEventCountsResult, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = "getSensorEventCountsResult", NewValue = value, OldValue = _getSensorEventCountsResult };
                    _getSensorEventCountsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        Bridgesense.Models.BridgesenseData.Bridge _master;
        protected Bridgesense.Models.BridgesenseData.Bridge master
        {
            get
            {
                return _master;
            }
            set
            {
                if (!object.Equals(_master, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = "master", NewValue = value, OldValue = _master };
                    _master = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }
        Bridgesense.Models.BridgesenseData.Sensor sensor_master;
        protected Bridgesense.Models.BridgesenseData.Sensor sensors_master
        {
            get
            {
                return sensor_master;
            }
            set
            {
                if (!object.Equals(sensor_master, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = "sensor_master", NewValue = value, OldValue = sensor_master };
                    sensor_master = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        string _lastFilter;
        protected string lastFilter
        {
            get
            {
                return _lastFilter;
            }
            set
            {
                if (!object.Equals(_lastFilter, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = "lastFilter", NewValue = value, OldValue = _lastFilter };
                    _lastFilter = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Bridgesense.Models.BridgesenseData.Sensor> _Sensors;
        protected IEnumerable<Bridgesense.Models.BridgesenseData.Sensor> Sensors
        {
            get
            {
                return _Sensors;
            }
            set
            {
                if (!object.Equals(_Sensors, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = "Sensors", NewValue = value, OldValue = _Sensors };
                    _Sensors = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Bridgesense.Models.BridgesenseData.SensorEventCount> _SensorEventCounts;
        protected IEnumerable<Bridgesense.Models.BridgesenseData.SensorEventCount> SensorEventCounts
        {
            get
            {
                return _SensorEventCounts;
            }
            set
            {
                if (!object.Equals(_SensorEventCounts, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = "SensorEventCounts", NewValue = value, OldValue = _SensorEventCounts };
                    _SensorEventCounts = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Bridgesense.Models.BridgesenseData.Bridgestat> _Bridgestats;
        protected IEnumerable<Bridgesense.Models.BridgesenseData.Bridgestat> Bridgestats
        {
            get
            {
                return _Bridgestats;
            }
            set
            {
                if (!object.Equals(_Bridgestats, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = "Bridgestats", NewValue = value, OldValue = _Bridgestats };
                    _Bridgestats = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Models.BridgesenseData.Bridgelog> _Bridgelogs;
        protected IEnumerable<Bridgesense.Models.BridgesenseData.Bridgelog> Bridgelogs
        {
            get
            {
                return _Bridgelogs;
            }
            set
            {
                if (!object.Equals(_Bridgelogs, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = "Bridgelogs", NewValue = value, OldValue = _Bridgelogs };
                    _Bridgelogs = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }


        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var bridgesenseDataGetBridgesResult = await BridgesenseData.GetBridges();
            getBridgesResult = bridgesenseDataGetBridgesResult;

            var bridgesenseDataGetBridgestats = await BridgesenseData.GetBridgestats(new Query() { Expand = "Sensor,Bridge" });
            getBridgestatsResult = bridgesenseDataGetBridgestats;

            var bridgesenseDataGetSensorEventCountsResult = await BridgesenseData.GetSensorEventCounts(new Query() { Expand = "Sensor,Bridge" });
            getSensorEventCountsResult = bridgesenseDataGetSensorEventCountsResult;

            var bridgesenseDataGetBridgelogResult = await BridgesenseData.GetBridgelogs(new Query() { Expand = "Bridge" });
            getBridgelogResult = bridgesenseDataGetBridgelogResult;


        }


        protected async void Grid0Render(DataGridRenderEventArgs<Bridgesense.Models.BridgesenseData.Bridge> args)
        {
            if (grid0.Query.Filter != lastFilter)
            {
                master = grid0.View.FirstOrDefault();
            }

            if (grid0.Query.Filter != lastFilter)
            {
                await grid0.SelectRow(master);
            }

            lastFilter = grid0.Query.Filter;
        }
        protected async void Grid1Render(DataGridRenderEventArgs<Bridgesense.Models.BridgesenseData.Sensor> args)
        {
            if (grid1.Query.Filter != lastFilter)
            {
                sensor_master = grid1.View.FirstOrDefault();
            }

            if (grid0.Query.Filter != lastFilter)
            {
                await grid1.SelectRow(sensor_master);
            }

            lastFilter = grid1.Query.Filter;
        }


        protected async System.Threading.Tasks.Task Grid0RowSelect(Bridgesense.Models.BridgesenseData.Bridge args)
        {
            
            master = args;

            if (args == null)
            {
                Sensors = null;
                Bridgelogs = null;
                Bridgestats = null; 
            }

            if (args != null)
            {
                var bridgesenseDataGetSensorsResult = await BridgesenseData.GetSensors(new Query() { Filter = $@"i => i.bridge_id == {args.id}" });
                Sensors = bridgesenseDataGetSensorsResult;
                var bridgesenseDataGetBridgestatsResult = await BridgesenseData.GetBridgestats(new Query() { Filter = $@"i => i.bridge_id == {args.id}" });
                Bridgestats = bridgesenseDataGetBridgestatsResult;

                var bridgesenseDataGetBridgelogResult = await BridgesenseData.GetBridgelogs(new Query() { Filter = $@"i => i.bridgeid == {args.id}" });
                Bridgelogs = bridgesenseDataGetBridgelogResult;

                foreach (var stats in Bridgestats)
                {

                    this.threshold = (stats.avg + (stats.std*2));
                    
                }
                

                foreach (var logs in Bridgelogs)
                {        

                    if (logs.duration != -1)
                    {
                        if (logs.duration >= threshold)
                        {
                            
                            this.dates.Add(logs.timestamp.Date);
                        }
                        
                    }
                    
                }
                

                dates.ForEach(date => Console.WriteLine(date));
                Console.WriteLine(this.threshold);
                
              
            }
        }
        protected async System.Threading.Tasks.Task Grid1RowSelect(Bridgesense.Models.BridgesenseData.Sensor args)
        {
            sensor_master = args;

            if (args == null)
            {
                Sensors = null;
            }

            if (args != null)
            {
                var bridgesenseDataGetSensorEventCountsResult = await BridgesenseData.GetSensorEventCounts(new Query() { Filter = $@"i => i.sensor_id == {args.id}" });
                SensorEventCounts = bridgesenseDataGetSensorEventCountsResult;
            }


        }

        public DateTime value = DateTime.Now;

        public List<DateTime?> dates = new List<DateTime?>();

        public int? threshold = 0;
       
        

        public void OnChange(DateTime? value, string name, string format)
        {
            Console.WriteLine($"{name} value changed to {value?.ToString(format)}");
         }

        public void DateRenderSpecial(DateRenderEventArgs args)
        {


            if (dates.Contains(args.Date))
            {
                args.Attributes.Add("style", "background-color: #ff6d41; border-color: white;");
            }
            
          
        }

     
        void OnTodayClick()
        {
            value = DateTime.Now;
        }
     
          
        
    }
   
}
