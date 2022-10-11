using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bridgesense.Models.BridgesenseData
{
  [Table("users", Schema = "public")]
  public partial class User
  {
    public DateTime? last_login
    {
      get;
      set;
    }
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
    public bool is_active
    {
      get;
      set;
    }
    public bool is_admin
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
    public string name
    {
      get;
      set;
    }
    public string email
    {
      get;
      set;
    }
    public string password
    {
      get;
      set;
    }
    public string phone_number
    {
      get;
      set;
    }
    public string organisation
    {
      get;
      set;
    }
    public string auth_token
    {
      get;
      set;
    }
  }
}
