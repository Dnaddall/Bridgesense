using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bridgesense.Models.BridgesenseData
{
  [Table("last_lux_events", Schema = "public")]
  public partial class LastLuxEvent
  {
    public DateTime timestamp
    {
      get;
      set;
    }
    public int last_lux
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
    public string sensor_udid
    {
      get;
      set;
    }

    [Column("event")]
    public string event1
    {
      get;
      set;
    }
  }
}
