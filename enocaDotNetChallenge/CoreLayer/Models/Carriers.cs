using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enocaDotNetChallenge.Core.Models
{
    public class Carriers : BaseEntity
    {
        public int CarrierConfigurationId { get; set; }
        public string CarrierName { get; set; }
        public bool CarrierIsActive { get; set; }
        public int CarrierPlusDesiCost { get; set; }
        public virtual ICollection<CarrierConfigurations> CarrierConfigurations { get; set; }
        public virtual ICollection<Orders> Orders { get; set; } 
    }
}
