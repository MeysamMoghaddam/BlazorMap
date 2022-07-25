using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorMap
{
    public partial class Map
    {
        [Parameter]
        public string Identifier { get; set; } = "leaflet";
        private DotNetObjectReference<Map>? dotNetHelper;
        /// <summary>
        /// GeoLocation View Height. defult 50vh
        /// </summary>
        [Parameter] public float ViewHeight { get; set; } = 50;
        [Parameter] public string Address { get; set; }
        [Parameter] public EventCallback<string> AddressChanged { get; set; }

        [Parameter] public string Lat { get; set; }
        [Parameter] public EventCallback<string> LatChanged { get; set; }

        [Parameter] public string Lng { get; set; }
        [Parameter] public EventCallback<string> LngChanged { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await map.GetLocation();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                dotNetHelper = DotNetObjectReference.Create(this);
                if (!string.IsNullOrEmpty(Identifier) && !string.IsNullOrWhiteSpace(Identifier))
                    await map.Load(Identifier, Lat, Lng, dotNetHelper);
            }
        }

        [JSInvokable]
        public async void OnDragEnd(string address, double lat, double lng)
        {
            await AddressChanged.InvokeAsync(address);
            await LatChanged.InvokeAsync(lat.ToString());
            await LngChanged.InvokeAsync(lng.ToString());
        }
    }
}