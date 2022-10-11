using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bridgesense.Models.BridgesenseData
{
  [Table("sensorstats_light", Schema = "public")]
  public partial class SensorstatsLight
  {
    public int? off_count
    {
      get;
      set;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id
    {
      get;
      set;
    }
    public int? on_count
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
    public float? on_percent
    {
      get;
      set;
    }
    public int? total
    {
      get;
      set;
    }
  }
}
