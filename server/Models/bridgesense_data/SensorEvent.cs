using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bridgesense.Models.BridgesenseData
{
  [Table("sensor_events", Schema = "public")]
  public partial class SensorEvent
  {
    public DateTime timestamp
    {
      get;
      set;
    }
    public int sensor_id
    {
      get;
      set;
    }
    public Sensor Sensor { get; set; }
    [Key]
    public int id
    {
      get;
      set;
    }
    public string sensor_udid
    {
      get;
      set;
    }

    [Column("sensorevent")]
    public string sensorevent1
    {
      get;
      set;
    }
  }
}
