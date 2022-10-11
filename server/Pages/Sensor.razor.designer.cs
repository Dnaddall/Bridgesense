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

namespace Bridgesense.Pages
{
    public partial class SensorComponent : ComponentBase
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
        protected RadzenDataGrid<Bridgesense.Models.BridgesenseData.Sensor> grid0;
        protected RadzenDataGrid<Bridgesense.Models.BridgesenseData.SensorEventCount> grid1;

        IEnumerable<Bridgesense.Models.BridgesenseData.Sensor> _getSensorsResult;
        protected IEnumerable<Bridgesense.Models.BridgesenseData.Sensor> getSensorsResult
        {
            get
            {
                return _getSensorsResult;
            }
            set
            {
                if (!object.Equals(_getSensorsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getSensorsResult", NewValue = value, OldValue = _getSensorsResult };
                    _getSensorsResult = value;
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
                    var args = new PropertyChangedEventArgs(){ Name = "getSensorEventCountsResult", NewValue = value, OldValue = _getSensorEventCountsResult };
                    _getSensorEventCountsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        Bridgesense.Models.BridgesenseData.Sensor _master;
        protected Bridgesense.Models.BridgesenseData.Sensor master
        {
            get
            {
                return _master;
            }
            set
            {
                if (!object.Equals(_master, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "master", NewValue = value, OldValue = _master };
                    _master = value;
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
                    var args = new PropertyChangedEventArgs(){ Name = "SensorEventCounts", NewValue = value, OldValue = _SensorEventCounts };
                    _SensorEventCounts = value;
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
            var bridgesenseDataGetSensorsResult = await BridgesenseData.GetSensors();
            getSensorsResult = bridgesenseDataGetSensorsResult;

            var bridgesenseDataGetSensorEventCountsResult = await BridgesenseData.GetSensorEventCounts(new Query() { Expand = "Bridge,Sensor" });
            getSensorEventCountsResult = bridgesenseDataGetSensorEventCountsResult;
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Bridgesense.Models.BridgesenseData.Sensor args)
        {
            master = args;

            if (args == null) {
                SensorEventCounts = null;
            }

            if (args != null)
            {
                var bridgesenseDataGetSensorEventCountsResult = await BridgesenseData.GetSensorEventCounts(new Query() { Filter = $@"i => i.sensor_id == {args.id}" });
                SensorEventCounts = bridgesenseDataGetSensorEventCountsResult;
            }
        }
    }
}
