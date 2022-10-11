using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bridgesense.Models.BridgesenseData
{
  [Table("sensors", Schema = "public")]
  public partial class Sensor
  {
    public int bridge_id
    {
      get;
      set;
    }
    public Bridge Bridge { get; set; }
    public DateTime? created_at
    {
      get;
      set;
    }
    public DateTime? updated_at
    {
      get;
      set;
    }
    [Key]
    public int id
    {
      get;
      set;
    }

    public ICollection<SensorEvent> SensorEvents { get; set; }
    public ICollection<SensorStatus> SensorStatuses { get; set; }
    public ICollection<SensorstatsLight> SensorstatsLights { get; set; }
    public ICollection<SensorEventCount> SensorEventCounts { get; set; }
    public string udid
    {
      get;
      set;
    }
    public string sensortype
    {
      get;
      set;
    }
  }
}
