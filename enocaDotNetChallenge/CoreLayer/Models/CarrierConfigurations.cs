using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enocaDotNetChallenge.Core.Models
{
    public class CarrierConfigurations :BaseEntity
    {
        public int CarrierId { get; set; }
        public int CarrierMaxDesi { get; set; }
        public int CarrierMinDesi { get; set; }
        public decimal CarrierCost { get; set; }
        public Carriers Carriers { get; set; }
    }
}
