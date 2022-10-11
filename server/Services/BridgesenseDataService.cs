using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using Bridgesense.Data;

namespace Bridgesense
{
    public partial class BridgesenseDataService
    {
        BridgesenseDataContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly BridgesenseDataContext context;
        private readonly NavigationManager navigationManager;

        public BridgesenseDataService(BridgesenseDataContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportBridgesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/bridges/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/bridges/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBridgesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/bridges/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/bridges/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBridgesRead(ref IQueryable<Models.BridgesenseData.Bridge> items);

        public async Task<IQueryable<Models.BridgesenseData.Bridge>> GetBridges(Query query = null)
        {
            var items = Context.Bridges.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnBridgesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBridgeCreated(Models.BridgesenseData.Bridge item);
        partial void OnAfterBridgeCreated(Models.BridgesenseData.Bridge item);

        public async Task<Models.BridgesenseData.Bridge> CreateBridge(Models.BridgesenseData.Bridge bridge)
        {
            OnBridgeCreated(bridge);

            var existingItem = Context.Bridges
                              .Where(i => i.id == bridge.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Bridges.Add(bridge);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(bridge).State = EntityState.Detached;
                throw;
            }

            OnAfterBridgeCreated(bridge);

            return bridge;
        }
        public async Task ExportBridgelogsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/bridgelogs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/bridgelogs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBridgelogsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/bridgelogs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/bridgelogs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBridgelogsRead(ref IQueryable<Models.BridgesenseData.Bridgelog> items);

        public async Task<IQueryable<Models.BridgesenseData.Bridgelog>> GetBridgelogs(Query query = null)
        {
            var items = Context.Bridgelogs.AsQueryable();

            items = items.Include(i => i.Bridge);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnBridgelogsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBridgelogCreated(Models.BridgesenseData.Bridgelog item);
        partial void OnAfterBridgelogCreated(Models.BridgesenseData.Bridgelog item);

        public async Task<Models.BridgesenseData.Bridgelog> CreateBridgelog(Models.BridgesenseData.Bridgelog bridgelog)
        {
            OnBridgelogCreated(bridgelog);

            var existingItem = Context.Bridgelogs
                              .Where(i => i.id == bridgelog.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Bridgelogs.Add(bridgelog);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(bridgelog).State = EntityState.Detached;
                bridgelog.Bridge = null;
                throw;
            }

            OnAfterBridgelogCreated(bridgelog);

            return bridgelog;
        }
        public async Task ExportBridgestatsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/bridgestats/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/bridgestats/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBridgestatsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/bridgestats/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/bridgestats/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBridgestatsRead(ref IQueryable<Models.BridgesenseData.Bridgestat> items);

        public async Task<IQueryable<Models.BridgesenseData.Bridgestat>> GetBridgestats(Query query = null)
        {
            var items = Context.Bridgestats.AsQueryable();

            items = items.Include(i => i.Bridge);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnBridgestatsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBridgestatCreated(Models.BridgesenseData.Bridgestat item);
        partial void OnAfterBridgestatCreated(Models.BridgesenseData.Bridgestat item);

        public async Task<Models.BridgesenseData.Bridgestat> CreateBridgestat(Models.BridgesenseData.Bridgestat bridgestat)
        {
            OnBridgestatCreated(bridgestat);

            var existingItem = Context.Bridgestats
                              .Where(i => i.id == bridgestat.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Bridgestats.Add(bridgestat);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(bridgestat).State = EntityState.Detached;
                bridgestat.Bridge = null;
                throw;
            }

            OnAfterBridgestatCreated(bridgestat);

            return bridgestat;
        }
        public async Task ExportLastLuxEventsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/lastluxevents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/lastluxevents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLastLuxEventsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/lastluxevents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/lastluxevents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLastLuxEventsRead(ref IQueryable<Models.BridgesenseData.LastLuxEvent> items);

        public async Task<IQueryable<Models.BridgesenseData.LastLuxEvent>> GetLastLuxEvents(Query query = null)
        {
            var items = Context.LastLuxEvents.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnLastLuxEventsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLastLuxEventCreated(Models.BridgesenseData.LastLuxEvent item);
        partial void OnAfterLastLuxEventCreated(Models.BridgesenseData.LastLuxEvent item);

        public async Task<Models.BridgesenseData.LastLuxEvent> CreateLastLuxEvent(Models.BridgesenseData.LastLuxEvent lastLuxEvent)
        {
            OnLastLuxEventCreated(lastLuxEvent);

            var existingItem = Context.LastLuxEvents
                              .Where(i => i.id == lastLuxEvent.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.LastLuxEvents.Add(lastLuxEvent);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(lastLuxEvent).State = EntityState.Detached;
                throw;
            }

            OnAfterLastLuxEventCreated(lastLuxEvent);

            return lastLuxEvent;
        }
        public async Task ExportSensorsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/sensors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/sensors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSensorsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/sensors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/sensors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSensorsRead(ref IQueryable<Models.BridgesenseData.Sensor> items);

        public async Task<IQueryable<Models.BridgesenseData.Sensor>> GetSensors(Query query = null)
        {
            var items = Context.Sensors.AsQueryable();

            items = items.Include(i => i.Bridge);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnSensorsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSensorCreated(Models.BridgesenseData.Sensor item);
        partial void OnAfterSensorCreated(Models.BridgesenseData.Sensor item);

        public async Task<Models.BridgesenseData.Sensor> CreateSensor(Models.BridgesenseData.Sensor sensor)
        {
            OnSensorCreated(sensor);

            var existingItem = Context.Sensors
                              .Where(i => i.id == sensor.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Sensors.Add(sensor);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(sensor).State = EntityState.Detached;
                sensor.Bridge = null;
                throw;
            }

            OnAfterSensorCreated(sensor);

            return sensor;
        }
        public async Task ExportSensorEventsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/sensorevents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/sensorevents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSensorEventsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/sensorevents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/sensorevents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSensorEventsRead(ref IQueryable<Models.BridgesenseData.SensorEvent> items);

        public async Task<IQueryable<Models.BridgesenseData.SensorEvent>> GetSensorEvents(Query query = null)
        {
            var items = Context.SensorEvents.AsQueryable();

            items = items.Include(i => i.Sensor);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnSensorEventsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSensorEventCreated(Models.BridgesenseData.SensorEvent item);
        partial void OnAfterSensorEventCreated(Models.BridgesenseData.SensorEvent item);

        public async Task<Models.BridgesenseData.SensorEvent> CreateSensorEvent(Models.BridgesenseData.SensorEvent sensorEvent)
        {
            OnSensorEventCreated(sensorEvent);

            var existingItem = Context.SensorEvents
                              .Where(i => i.id == sensorEvent.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SensorEvents.Add(sensorEvent);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(sensorEvent).State = EntityState.Detached;
                sensorEvent.Sensor = null;
                throw;
            }

            OnAfterSensorEventCreated(sensorEvent);

            return sensorEvent;
        }
        public async Task ExportSensorEventCountsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/sensoreventcounts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/sensoreventcounts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSensorEventCountsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/sensoreventcounts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/sensoreventcounts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSensorEventCountsRead(ref IQueryable<Models.BridgesenseData.SensorEventCount> items);

        public async Task<IQueryable<Models.BridgesenseData.SensorEventCount>> GetSensorEventCounts(Query query = null)
        {
            var items = Context.SensorEventCounts.AsQueryable();

            items = items.Include(i => i.Bridge);

            items = items.Include(i => i.Sensor);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnSensorEventCountsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSensorEventCountCreated(Models.BridgesenseData.SensorEventCount item);
        partial void OnAfterSensorEventCountCreated(Models.BridgesenseData.SensorEventCount item);

        public async Task<Models.BridgesenseData.SensorEventCount> CreateSensorEventCount(Models.BridgesenseData.SensorEventCount sensorEventCount)
        {
            OnSensorEventCountCreated(sensorEventCount);

            var existingItem = Context.SensorEventCounts
                              .Where(i => i.id == sensorEventCount.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SensorEventCounts.Add(sensorEventCount);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(sensorEventCount).State = EntityState.Detached;
                sensorEventCount.Bridge = null;
                sensorEventCount.Sensor = null;
                throw;
            }

            OnAfterSensorEventCountCreated(sensorEventCount);

            return sensorEventCount;
        }
        public async Task ExportSensorStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/sensorstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/sensorstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSensorStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/sensorstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/sensorstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSensorStatusesRead(ref IQueryable<Models.BridgesenseData.SensorStatus> items);

        public async Task<IQueryable<Models.BridgesenseData.SensorStatus>> GetSensorStatuses(Query query = null)
        {
            var items = Context.SensorStatuses.AsQueryable();

            items = items.Include(i => i.Sensor);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnSensorStatusesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSensorStatusCreated(Models.BridgesenseData.SensorStatus item);
        partial void OnAfterSensorStatusCreated(Models.BridgesenseData.SensorStatus item);

        public async Task<Models.BridgesenseData.SensorStatus> CreateSensorStatus(Models.BridgesenseData.SensorStatus sensorStatus)
        {
            OnSensorStatusCreated(sensorStatus);

            var existingItem = Context.SensorStatuses
                              .Where(i => i.id == sensorStatus.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SensorStatuses.Add(sensorStatus);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(sensorStatus).State = EntityState.Detached;
                sensorStatus.Sensor = null;
                throw;
            }

            OnAfterSensorStatusCreated(sensorStatus);

            return sensorStatus;
        }
        public async Task ExportSensorstatsLightsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/sensorstatslights/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/sensorstatslights/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSensorstatsLightsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/sensorstatslights/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/sensorstatslights/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSensorstatsLightsRead(ref IQueryable<Models.BridgesenseData.SensorstatsLight> items);

        public async Task<IQueryable<Models.BridgesenseData.SensorstatsLight>> GetSensorstatsLights(Query query = null)
        {
            var items = Context.SensorstatsLights.AsQueryable();

            items = items.Include(i => i.Sensor);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnSensorstatsLightsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSensorstatsLightCreated(Models.BridgesenseData.SensorstatsLight item);
        partial void OnAfterSensorstatsLightCreated(Models.BridgesenseData.SensorstatsLight item);

        public async Task<Models.BridgesenseData.SensorstatsLight> CreateSensorstatsLight(Models.BridgesenseData.SensorstatsLight sensorstatsLight)
        {
            OnSensorstatsLightCreated(sensorstatsLight);

            var existingItem = Context.SensorstatsLights
                              .Where(i => i.id == sensorstatsLight.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SensorstatsLights.Add(sensorstatsLight);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(sensorstatsLight).State = EntityState.Detached;
                sensorstatsLight.Sensor = null;
                throw;
            }

            OnAfterSensorstatsLightCreated(sensorstatsLight);

            return sensorstatsLight;
        }
        public async Task ExportTestsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/tests/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/tests/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTestsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/tests/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/tests/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTestsRead(ref IQueryable<Models.BridgesenseData.Test> items);

        public async Task<IQueryable<Models.BridgesenseData.Test>> GetTests(Query query = null)
        {
            var items = Context.Tests.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnTestsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTestCreated(Models.BridgesenseData.Test item);
        partial void OnAfterTestCreated(Models.BridgesenseData.Test item);

        public async Task<Models.BridgesenseData.Test> CreateTest(Models.BridgesenseData.Test test)
        {
            OnTestCreated(test);

            var existingItem = Context.Tests
                              .Where(i => i.id == test.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Tests.Add(test);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(test).State = EntityState.Detached;
                throw;
            }

            OnAfterTestCreated(test);

            return test;
        }
        public async Task ExportUdpMessagesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/udpmessages/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/udpmessages/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUdpMessagesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/udpmessages/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/udpmessages/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUdpMessagesRead(ref IQueryable<Models.BridgesenseData.UdpMessage> items);

        public async Task<IQueryable<Models.BridgesenseData.UdpMessage>> GetUdpMessages(Query query = null)
        {
            var items = Context.UdpMessages.AsQueryable();
            items = items.AsNoTracking();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnUdpMessagesRead(ref items);

            return await Task.FromResult(items);
        }
        public async Task ExportUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/bridgesensedata/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/bridgesensedata/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUsersRead(ref IQueryable<Models.BridgesenseData.User> items);

        public async Task<IQueryable<Models.BridgesenseData.User>> GetUsers(Query query = null)
        {
            var items = Context.Users.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnUsersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUserCreated(Models.BridgesenseData.User item);
        partial void OnAfterUserCreated(Models.BridgesenseData.User item);

        public async Task<Models.BridgesenseData.User> CreateUser(Models.BridgesenseData.User user)
        {
            OnUserCreated(user);

            var existingItem = Context.Users
                              .Where(i => i.id == user.id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Users.Add(user);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(user).State = EntityState.Detached;
                throw;
            }

            OnAfterUserCreated(user);

            return user;
        }

        partial void OnBridgeDeleted(Models.BridgesenseData.Bridge item);
        partial void OnAfterBridgeDeleted(Models.BridgesenseData.Bridge item);

        public async Task<Models.BridgesenseData.Bridge> DeleteBridge(int? id)
        {
            var itemToDelete = Context.Bridges
                              .Where(i => i.id == id)
                              .Include(i => i.Bridgelogs)
                              .Include(i => i.Sensors)
                              .Include(i => i.Bridgestats)
                              .Include(i => i.SensorEventCounts)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBridgeDeleted(itemToDelete);

            Context.Bridges.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBridgeDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnBridgeGet(Models.BridgesenseData.Bridge item);

        public async Task<Models.BridgesenseData.Bridge> GetBridgeByid(int? id)
        {
            var items = Context.Bridges
                              .AsNoTracking()
                              .Where(i => i.id == id);

            var itemToReturn = items.FirstOrDefault();

            OnBridgeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BridgesenseData.Bridge> CancelBridgeChanges(Models.BridgesenseData.Bridge item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBridgeUpdated(Models.BridgesenseData.Bridge item);
        partial void OnAfterBridgeUpdated(Models.BridgesenseData.Bridge item);

        public async Task<Models.BridgesenseData.Bridge> UpdateBridge(int? id, Models.BridgesenseData.Bridge bridge)
        {
            OnBridgeUpdated(bridge);

            var itemToUpdate = Context.Bridges
                              .Where(i => i.id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(bridge);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterBridgeUpdated(bridge);

            return bridge;
        }

        partial void OnBridgelogDeleted(Models.BridgesenseData.Bridgelog item);
        partial void OnAfterBridgelogDeleted(Models.BridgesenseData.Bridgelog item);

        public async Task<Models.BridgesenseData.Bridgelog> DeleteBridgelog(Int64? id)
        {
            var itemToDelete = Context.Bridgelogs
                              .Where(i => i.id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBridgelogDeleted(itemToDelete);

            Context.Bridgelogs.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBridgelogDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnBridgelogGet(Models.BridgesenseData.Bridgelog item);

        public async Task<Models.BridgesenseData.Bridgelog> GetBridgelogByid(Int64? id)
        {
            var items = Context.Bridgelogs
                              .AsNoTracking()
                              .Where(i => i.id == id);

            items = items.Include(i => i.Bridge);

            var itemToReturn = items.FirstOrDefault();

            OnBridgelogGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BridgesenseData.Bridgelog> CancelBridgelogChanges(Models.BridgesenseData.Bridgelog item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBridgelogUpdated(Models.BridgesenseData.Bridgelog item);
        partial void OnAfterBridgelogUpdated(Models.BridgesenseData.Bridgelog item);

        public async Task<Models.BridgesenseData.Bridgelog> UpdateBridgelog(Int64? id, Models.BridgesenseData.Bridgelog bridgelog)
        {
            OnBridgelogUpdated(bridgelog);

            var itemToUpdate = Context.Bridgelogs
                              .Where(i => i.id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(bridgelog);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterBridgelogUpdated(bridgelog);

            return bridgelog;
        }

        partial void OnBridgestatDeleted(Models.BridgesenseData.Bridgestat item);
        partial void OnAfterBridgestatDeleted(Models.BridgesenseData.Bridgestat item);

        public async Task<Models.BridgesenseData.Bridgestat> DeleteBridgestat(int? id)
        {
            var itemToDelete = Context.Bridgestats
                              .Where(i => i.id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBridgestatDeleted(itemToDelete);

            Context.Bridgestats.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBridgestatDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnBridgestatGet(Models.BridgesenseData.Bridgestat item);

        public async Task<Models.BridgesenseData.Bridgestat> GetBridgestatByid(int? id)
        {
            var items = Context.Bridgestats
                              .AsNoTracking()
                              .Where(i => i.id == id);

            items = items.Include(i => i.Bridge);

            var itemToReturn = items.FirstOrDefault();

            OnBridgestatGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BridgesenseData.Bridgestat> CancelBridgestatChanges(Models.BridgesenseData.Bridgestat item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBridgestatUpdated(Models.BridgesenseData.Bridgestat item);
        partial void OnAfterBridgestatUpdated(Models.BridgesenseData.Bridgestat item);

        public async Task<Models.BridgesenseData.Bridgestat> UpdateBridgestat(int? id, Models.BridgesenseData.Bridgestat bridgestat)
        {
            OnBridgestatUpdated(bridgestat);

            var itemToUpdate = Context.Bridgestats
                              .Where(i => i.id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(bridgestat);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterBridgestatUpdated(bridgestat);

            return bridgestat;
        }

        partial void OnLastLuxEventDeleted(Models.BridgesenseData.LastLuxEvent item);
        partial void OnAfterLastLuxEventDeleted(Models.BridgesenseData.LastLuxEvent item);

        public async Task<Models.BridgesenseData.LastLuxEvent> DeleteLastLuxEvent(int? id)
        {
            var itemToDelete = Context.LastLuxEvents
                              .Where(i => i.id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnLastLuxEventDeleted(itemToDelete);

            Context.LastLuxEvents.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLastLuxEventDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnLastLuxEventGet(Models.BridgesenseData.LastLuxEvent item);

        public async Task<Models.BridgesenseData.LastLuxEvent> GetLastLuxEventByid(int? id)
        {
            var items = Context.LastLuxEvents
                              .AsNoTracking()
                              .Where(i => i.id == id);

            var itemToReturn = items.FirstOrDefault();

            OnLastLuxEventGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BridgesenseData.LastLuxEvent> CancelLastLuxEventChanges(Models.BridgesenseData.LastLuxEvent item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLastLuxEventUpdated(Models.BridgesenseData.LastLuxEvent item);
        partial void OnAfterLastLuxEventUpdated(Models.BridgesenseData.LastLuxEvent item);

        public async Task<Models.BridgesenseData.LastLuxEvent> UpdateLastLuxEvent(int? id, Models.BridgesenseData.LastLuxEvent lastLuxEvent)
        {
            OnLastLuxEventUpdated(lastLuxEvent);

            var itemToUpdate = Context.LastLuxEvents
                              .Where(i => i.id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(lastLuxEvent);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterLastLuxEventUpdated(lastLuxEvent);

            return lastLuxEvent;
        }

        partial void OnSensorDeleted(Models.BridgesenseData.Sensor item);
        partial void OnAfterSensorDeleted(Models.BridgesenseData.Sensor item);

        public async Task<Models.BridgesenseData.Sensor> DeleteSensor(int? id)
        {
            var itemToDelete = Context.Sensors
                              .Where(i => i.id == id)
                              .Include(i => i.SensorEvents)
                              .Include(i => i.SensorStatuses)
                              .Include(i => i.SensorstatsLights)
                              .Include(i => i.SensorEventCounts)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSensorDeleted(itemToDelete);

            Context.Sensors.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSensorDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnSensorGet(Models.BridgesenseData.Sensor item);

        public async Task<Models.BridgesenseData.Sensor> GetSensorByid(int? id)
        {
            var items = Context.Sensors
                              .AsNoTracking()
                              .Where(i => i.id == id);

            items = items.Include(i => i.Bridge);

            var itemToReturn = items.FirstOrDefault();

            OnSensorGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BridgesenseData.Sensor> CancelSensorChanges(Models.BridgesenseData.Sensor item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSensorUpdated(Models.BridgesenseData.Sensor item);
        partial void OnAfterSensorUpdated(Models.BridgesenseData.Sensor item);

        public async Task<Models.BridgesenseData.Sensor> UpdateSensor(int? id, Models.BridgesenseData.Sensor sensor)
        {
            OnSensorUpdated(sensor);

            var itemToUpdate = Context.Sensors
                              .Where(i => i.id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(sensor);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterSensorUpdated(sensor);

            return sensor;
        }

        partial void OnSensorEventDeleted(Models.BridgesenseData.SensorEvent item);
        partial void OnAfterSensorEventDeleted(Models.BridgesenseData.SensorEvent item);

        public async Task<Models.BridgesenseData.SensorEvent> DeleteSensorEvent(int? id)
        {
            var itemToDelete = Context.SensorEvents
                              .Where(i => i.id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSensorEventDeleted(itemToDelete);

            Context.SensorEvents.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSensorEventDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnSensorEventGet(Models.BridgesenseData.SensorEvent item);

        public async Task<Models.BridgesenseData.SensorEvent> GetSensorEventByid(int? id)
        {
            var items = Context.SensorEvents
                              .AsNoTracking()
                              .Where(i => i.id == id);

            items = items.Include(i => i.Sensor);

            var itemToReturn = items.FirstOrDefault();

            OnSensorEventGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BridgesenseData.SensorEvent> CancelSensorEventChanges(Models.BridgesenseData.SensorEvent item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSensorEventUpdated(Models.BridgesenseData.SensorEvent item);
        partial void OnAfterSensorEventUpdated(Models.BridgesenseData.SensorEvent item);

        public async Task<Models.BridgesenseData.SensorEvent> UpdateSensorEvent(int? id, Models.BridgesenseData.SensorEvent sensorEvent)
        {
            OnSensorEventUpdated(sensorEvent);

            var itemToUpdate = Context.SensorEvents
                              .Where(i => i.id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(sensorEvent);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterSensorEventUpdated(sensorEvent);

            return sensorEvent;
        }

        partial void OnSensorEventCountDeleted(Models.BridgesenseData.SensorEventCount item);
        partial void OnAfterSensorEventCountDeleted(Models.BridgesenseData.SensorEventCount item);

        public async Task<Models.BridgesenseData.SensorEventCount> DeleteSensorEventCount(int? id)
        {
            var itemToDelete = Context.SensorEventCounts
                              .Where(i => i.id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSensorEventCountDeleted(itemToDelete);

            Context.SensorEventCounts.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSensorEventCountDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnSensorEventCountGet(Models.BridgesenseData.SensorEventCount item);

        public async Task<Models.BridgesenseData.SensorEventCount> GetSensorEventCountByid(int? id)
        {
            var items = Context.SensorEventCounts
                              .AsNoTracking()
                              .Where(i => i.id == id);

            items = items.Include(i => i.Bridge);

            items = items.Include(i => i.Sensor);

            var itemToReturn = items.FirstOrDefault();

            OnSensorEventCountGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BridgesenseData.SensorEventCount> CancelSensorEventCountChanges(Models.BridgesenseData.SensorEventCount item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSensorEventCountUpdated(Models.BridgesenseData.SensorEventCount item);
        partial void OnAfterSensorEventCountUpdated(Models.BridgesenseData.SensorEventCount item);

        public async Task<Models.BridgesenseData.SensorEventCount> UpdateSensorEventCount(int? id, Models.BridgesenseData.SensorEventCount sensorEventCount)
        {
            OnSensorEventCountUpdated(sensorEventCount);

            var itemToUpdate = Context.SensorEventCounts
                              .Where(i => i.id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(sensorEventCount);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterSensorEventCountUpdated(sensorEventCount);

            return sensorEventCount;
        }

        partial void OnSensorStatusDeleted(Models.BridgesenseData.SensorStatus item);
        partial void OnAfterSensorStatusDeleted(Models.BridgesenseData.SensorStatus item);

        public async Task<Models.BridgesenseData.SensorStatus> DeleteSensorStatus(int? id)
        {
            var itemToDelete = Context.SensorStatuses
                              .Where(i => i.id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSensorStatusDeleted(itemToDelete);

            Context.SensorStatuses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSensorStatusDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnSensorStatusGet(Models.BridgesenseData.SensorStatus item);

        public async Task<Models.BridgesenseData.SensorStatus> GetSensorStatusByid(int? id)
        {
            var items = Context.SensorStatuses
                              .AsNoTracking()
                              .Where(i => i.id == id);

            items = items.Include(i => i.Sensor);

            var itemToReturn = items.FirstOrDefault();

            OnSensorStatusGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BridgesenseData.SensorStatus> CancelSensorStatusChanges(Models.BridgesenseData.SensorStatus item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSensorStatusUpdated(Models.BridgesenseData.SensorStatus item);
        partial void OnAfterSensorStatusUpdated(Models.BridgesenseData.SensorStatus item);

        public async Task<Models.BridgesenseData.SensorStatus> UpdateSensorStatus(int? id, Models.BridgesenseData.SensorStatus sensorStatus)
        {
            OnSensorStatusUpdated(sensorStatus);

            var itemToUpdate = Context.SensorStatuses
                              .Where(i => i.id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(sensorStatus);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterSensorStatusUpdated(sensorStatus);

            return sensorStatus;
        }

        partial void OnSensorstatsLightDeleted(Models.BridgesenseData.SensorstatsLight item);
        partial void OnAfterSensorstatsLightDeleted(Models.BridgesenseData.SensorstatsLight item);

        public async Task<Models.BridgesenseData.SensorstatsLight> DeleteSensorstatsLight(int? id)
        {
            var itemToDelete = Context.SensorstatsLights
                              .Where(i => i.id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSensorstatsLightDeleted(itemToDelete);

            Context.SensorstatsLights.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSensorstatsLightDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnSensorstatsLightGet(Models.BridgesenseData.SensorstatsLight item);

        public async Task<Models.BridgesenseData.SensorstatsLight> GetSensorstatsLightByid(int? id)
        {
            var items = Context.SensorstatsLights
                              .AsNoTracking()
                              .Where(i => i.id == id);

            items = items.Include(i => i.Sensor);

            var itemToReturn = items.FirstOrDefault();

            OnSensorstatsLightGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BridgesenseData.SensorstatsLight> CancelSensorstatsLightChanges(Models.BridgesenseData.SensorstatsLight item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSensorstatsLightUpdated(Models.BridgesenseData.SensorstatsLight item);
        partial void OnAfterSensorstatsLightUpdated(Models.BridgesenseData.SensorstatsLight item);

        public async Task<Models.BridgesenseData.SensorstatsLight> UpdateSensorstatsLight(int? id, Models.BridgesenseData.SensorstatsLight sensorstatsLight)
        {
            OnSensorstatsLightUpdated(sensorstatsLight);

            var itemToUpdate = Context.SensorstatsLights
                              .Where(i => i.id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(sensorstatsLight);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterSensorstatsLightUpdated(sensorstatsLight);

            return sensorstatsLight;
        }

        partial void OnTestDeleted(Models.BridgesenseData.Test item);
        partial void OnAfterTestDeleted(Models.BridgesenseData.Test item);

        public async Task<Models.BridgesenseData.Test> DeleteTest(int? id)
        {
            var itemToDelete = Context.Tests
                              .Where(i => i.id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTestDeleted(itemToDelete);

            Context.Tests.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTestDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnTestGet(Models.BridgesenseData.Test item);

        public async Task<Models.BridgesenseData.Test> GetTestByid(int? id)
        {
            var items = Context.Tests
                              .AsNoTracking()
                              .Where(i => i.id == id);

            var itemToReturn = items.FirstOrDefault();

            OnTestGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BridgesenseData.Test> CancelTestChanges(Models.BridgesenseData.Test item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTestUpdated(Models.BridgesenseData.Test item);
        partial void OnAfterTestUpdated(Models.BridgesenseData.Test item);

        public async Task<Models.BridgesenseData.Test> UpdateTest(int? id, Models.BridgesenseData.Test test)
        {
            OnTestUpdated(test);

            var itemToUpdate = Context.Tests
                              .Where(i => i.id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(test);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterTestUpdated(test);

            return test;
        }

        partial void OnUserDeleted(Models.BridgesenseData.User item);
        partial void OnAfterUserDeleted(Models.BridgesenseData.User item);

        public async Task<Models.BridgesenseData.User> DeleteUser(int? id)
        {
            var itemToDelete = Context.Users
                              .Where(i => i.id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUserDeleted(itemToDelete);

            Context.Users.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUserDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnUserGet(Models.BridgesenseData.User item);

        public async Task<Models.BridgesenseData.User> GetUserByid(int? id)
        {
            var items = Context.Users
                              .AsNoTracking()
                              .Where(i => i.id == id);

            var itemToReturn = items.FirstOrDefault();

            OnUserGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.BridgesenseData.User> CancelUserChanges(Models.BridgesenseData.User item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUserUpdated(Models.BridgesenseData.User item);
        partial void OnAfterUserUpdated(Models.BridgesenseData.User item);

        public async Task<Models.BridgesenseData.User> UpdateUser(int? id, Models.BridgesenseData.User user)
        {
            OnUserUpdated(user);

            var itemToUpdate = Context.Users
                              .Where(i => i.id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(user);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterUserUpdated(user);

            return user;
        }
    }
}
