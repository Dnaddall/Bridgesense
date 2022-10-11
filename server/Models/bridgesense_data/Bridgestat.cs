using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bridgesense.Models.BridgesenseData
{
  [Table("bridgestats", Schema = "public")]
  public partial class Bridgestat
  {
    public int? bridge_id
    {
      get;
      set;
    }
    public Bridge Bridge { get; set; }
    public int? max
    {
      get;
      set;
    }
    public int? min
    {
      get;
      set;
    }
    public int? avg
    {
      get;
      set;
    }
    public int? std
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
    public int period
    {
      get;
      set;
    }
  }
}
