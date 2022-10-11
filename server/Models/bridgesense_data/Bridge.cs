using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bridgesense.Models.BridgesenseData
{
    [Table("bridges", Schema = "public")]
    public partial class Bridge
    {
        public double? latitude
        {
            get;
            set;
        }
        public double? longitude
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
        [Key]
        public int id
        {
            get;
            set;
        }

        public ICollection<Bridgelog> Bridgelogs { get; set; }
        public ICollection<Sensor> Sensors { get; set; }
        public ICollection<Bridgestat> Bridgestats { get; set; }
        public ICollection<SensorEventCount> SensorEventCounts { get; set; }
        public string name
        {
            get;
            set;
        }
        public string vild_code
        {
            get;
            set;
        }
        public string owner
        {
            get;
            set;
        }
        public string manager
        {
            get;
            set;
        }
        public string isrs
        {
            get;
            set;
        }
    }
}
