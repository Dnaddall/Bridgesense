using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bridgesense.Data;

namespace Bridgesense
{
    public partial class ExportBridgesenseDataController : ExportController
    {
        private readonly BridgesenseDataContext context;
        private readonly BridgesenseDataService service;
        public ExportBridgesenseDataController(BridgesenseDataContext context, BridgesenseDataService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/BridgesenseData/bridges/csv")]
        [HttpGet("/export/BridgesenseData/bridges/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportBridgesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBridges(), Request.Query), fileName);
        }

        [HttpGet("/export/BridgesenseData/bridges/excel")]
        [HttpGet("/export/BridgesenseData/bridges/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportBridgesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBridges(), Request.Query), fileName);
        }
        [HttpGet("/export/BridgesenseData/bridgelogs/csv")]
        [HttpGet("/export/BridgesenseData/bridgelogs/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportBridgelogsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBridgelogs(), Request.Query), fileName);
        }

        [HttpGet("/export/BridgesenseData/bridgelogs/excel")]
        [HttpGet("/export/BridgesenseData/bridgelogs/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportBridgelogsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBridgelogs(), Request.Query), fileName);
        }
        [HttpGet("/export/BridgesenseData/bridgestats/csv")]
        [HttpGet("/export/BridgesenseData/bridgestats/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportBridgestatsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBridgestats(), Request.Query), fileName);
        }

        [HttpGet("/export/BridgesenseData/bridgestats/excel")]
        [HttpGet("/export/BridgesenseData/bridgestats/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportBridgestatsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBridgestats(), Request.Query), fileName);
        }
        [HttpGet("/export/BridgesenseData/lastluxevents/csv")]
        [HttpGet("/export/BridgesenseData/lastluxevents/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportLastLuxEventsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLastLuxEvents(), Request.Query), fileName);
        }

        [HttpGet("/export/BridgesenseData/lastluxevents/excel")]
        [HttpGet("/export/BridgesenseData/lastluxevents/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportLastLuxEventsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLastLuxEvents(), Request.Query), fileName);
        }
        [HttpGet("/export/BridgesenseData/sensors/csv")]
        [HttpGet("/export/BridgesenseData/sensors/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSensorsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSensors(), Request.Query), fileName);
        }

        [HttpGet("/export/BridgesenseData/sensors/excel")]
        [HttpGet("/export/BridgesenseData/sensors/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSensorsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSensors(), Request.Query), fileName);
        }
        [HttpGet("/export/BridgesenseData/sensorevents/csv")]
        [HttpGet("/export/BridgesenseData/sensorevents/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSensorEventsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSensorEvents(), Request.Query), fileName);
        }

        [HttpGet("/export/BridgesenseData/sensorevents/excel")]
        [HttpGet("/export/BridgesenseData/sensorevents/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSensorEventsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSensorEvents(), Request.Query), fileName);
        }
        [HttpGet("/export/BridgesenseData/sensoreventcounts/csv")]
        [HttpGet("/export/BridgesenseData/sensoreventcounts/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSensorEventCountsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSensorEventCounts(), Request.Query), fileName);
        }

        [HttpGet("/export/BridgesenseData/sensoreventcounts/excel")]
        [HttpGet("/export/BridgesenseData/sensoreventcounts/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSensorEventCountsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSensorEventCounts(), Request.Query), fileName);
        }
        [HttpGet("/export/BridgesenseData/sensorstatuses/csv")]
        [HttpGet("/export/BridgesenseData/sensorstatuses/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSensorStatusesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSensorStatuses(), Request.Query), fileName);
        }

        [HttpGet("/export/BridgesenseData/sensorstatuses/excel")]
        [HttpGet("/export/BridgesenseData/sensorstatuses/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSensorStatusesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSensorStatuses(), Request.Query), fileName);
        }
        [HttpGet("/export/BridgesenseData/sensorstatslights/csv")]
        [HttpGet("/export/BridgesenseData/sensorstatslights/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSensorstatsLightsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSensorstatsLights(), Request.Query), fileName);
        }

        [HttpGet("/export/BridgesenseData/sensorstatslights/excel")]
        [HttpGet("/export/BridgesenseData/sensorstatslights/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSensorstatsLightsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSensorstatsLights(), Request.Query), fileName);
        }
        [HttpGet("/export/BridgesenseData/tests/csv")]
        [HttpGet("/export/BridgesenseData/tests/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportTestsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTests(), Request.Query), fileName);
        }

        [HttpGet("/export/BridgesenseData/tests/excel")]
        [HttpGet("/export/BridgesenseData/tests/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportTestsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTests(), Request.Query), fileName);
        }
        [HttpGet("/export/BridgesenseData/udpmessages/csv")]
        [HttpGet("/export/BridgesenseData/udpmessages/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportUdpMessagesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUdpMessages(), Request.Query), fileName);
        }

        [HttpGet("/export/BridgesenseData/udpmessages/excel")]
        [HttpGet("/export/BridgesenseData/udpmessages/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportUdpMessagesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUdpMessages(), Request.Query), fileName);
        }
        [HttpGet("/export/BridgesenseData/users/csv")]
        [HttpGet("/export/BridgesenseData/users/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUsers(), Request.Query), fileName);
        }

        [HttpGet("/export/BridgesenseData/users/excel")]
        [HttpGet("/export/BridgesenseData/users/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUsers(), Request.Query), fileName);
        }
    }
}
