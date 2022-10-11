using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bridgesense.Models.BridgesenseData
{
  [Table("sensor_event_count", Schema = "public")]
  public partial class SensorEventCount
  {
    public int? bridge_id
    {
      get;
      set;
    }
    public Bridge Bridge { get; set; }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id
    {
      get;
      set;
    }
    public int? count
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

    [Column("event")]
    public string event1
    {
      get;
      set;
    }
    public string type
    {
      get;
      set;
    }
  }
}
