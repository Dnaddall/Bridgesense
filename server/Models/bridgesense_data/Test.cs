using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bridgesense.Models.BridgesenseData
{
  [Table("test", Schema = "public")]
  public partial class Test
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id
    {
      get;
      set;
    }
    public int? value
    {
      get;
      set;
    }
    public string category
    {
      get;
      set;
    }
  }
}
