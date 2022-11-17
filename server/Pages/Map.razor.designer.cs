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
using DocumentFormat.OpenXml.Bibliography;

namespace Bridgesense.Pages
{
    public partial class MapComponent : ComponentBase
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

        public EventCallback<RadzenGoogleMapMarker> MarkerClick { get; set; }





        IEnumerable<Bridgesense.Models.BridgesenseData.Bridge> _getBridgeResults;
        protected IEnumerable<Bridgesense.Models.BridgesenseData.Bridge> getBridgeResults
        {
            get
            {
                return _getBridgeResults;
            }
            set
            {
                if (!object.Equals(_getBridgeResults, value))
                {
                    var args = new PropertyChangedEventArgs() { Name = "getBridgeResults", NewValue = value, OldValue = _getBridgeResults };
                    _getBridgeResults = value;
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
            getBridgeResults = bridgesenseDataGetBridgesResult;

        }

        public void OnMarkerClick(RadzenGoogleMapMarker marker)
        {

            Console.WriteLine($"Map {marker.Title} marker clicked. Marker position -> Lat: {marker.Position.Lat}, Lng: {marker.Position.Lng}");

        }

    
    }
}



