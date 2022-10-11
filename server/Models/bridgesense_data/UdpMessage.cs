using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bridgesense.Models.BridgesenseData
{
  [Table("udp_messages", Schema = "public")]
  public partial class UdpMessage
  {
    public DateTime? ts_receive
    {
      get;
      set;
    }
    public DateTime? ts_message
    {
      get;
      set;
    }
    public int? light_on_threshold
    {
      get;
      set;
    }
    public int? light_on_value
    {
      get;
      set;
    }
    public int? battery_voltage
    {
      get;
      set;
    }
    public int? persist
    {
      get;
      set;
    }
    public int? gain
    {
      get;
      set;
    }
    public int? timeout
    {
      get;
      set;
    }
    public int? rsrp
    {
      get;
      set;
    }
    public int logid
    {
      get;
      set;
    }
    public string imei
    {
      get;
      set;
    }
    public string trigger
    {
      get;
      set;
    }
    public string version
    {
      get;
      set;
    }
  }
}
