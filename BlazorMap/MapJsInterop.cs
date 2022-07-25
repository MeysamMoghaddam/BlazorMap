using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMap
{
    public interface IMapJsInterop
    {
        Task Init();
        Task Load(string Identifier, string Lat, string Lng, DotNetObjectReference<Map>? dotNetHelper);
        Task GetLocation();
    }
    public class MapJsInterop : IAsyncDisposable, IMapJsInterop
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private readonly Lazy<Task<IJSObjectReference>> main;
        public MapJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/BlazorMap/leaflet.js").AsTask());

            main = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
              "import", "./_content/BlazorMap/main.js").AsTask());

            Init();
        }
        public async Task Init()
        {
            await moduleTask.Value;
            await main.Value;
        }
        public async Task GetLocation()
        {
            var module = await main.Value;
            await module.InvokeVoidAsync("getLocation");
        }
        public async Task Load(string Identifier, string Lat, string Lng, DotNetObjectReference<Map>? dotNetHelper)
        {
            var module = await main.Value;
            await module.InvokeVoidAsync("initialMap", Identifier, Lat, Lng, dotNetHelper);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
