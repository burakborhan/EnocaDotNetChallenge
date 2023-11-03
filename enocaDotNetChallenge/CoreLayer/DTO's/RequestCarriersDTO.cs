using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enocaDotNetChallenge.Core.DTO_s
{
    public class RequestCarriersDTO
    {
        public int CarrierConfigurationId { get; set; }
        public string CarrierName { get; set; }
        public bool CarrierIsActive { get; set; }
        public int CarrierPlusDesiCost { get; set; }
    }
}
