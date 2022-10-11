using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bridgesense.Models.BridgesenseData
{
  [Table("bridgelog", Schema = "public")]
  public partial class Bridgelog
  {
    public int bridgeid
    {
      get;
      set;
    }
    public Bridge Bridge { get; set; }
    public DateTime? timestamp
    {
      get;
      set;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Int64 id
    {
      get;
      set;
    }
    public int duration
    {
      get;
      set;
    }
    public string situationid
    {
      get;
      set;
    }
    public string action
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
