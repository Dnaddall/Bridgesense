using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bridgesense.Models.BridgesenseData
{
  [Table("sensor_status", Schema = "public")]
  public partial class SensorStatus
  {
    public DateTime timestamp
    {
      get;
      set;
    }
    public int? sensor_id
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
    public string status
    {
      get;
      set;
    }
  }
}
