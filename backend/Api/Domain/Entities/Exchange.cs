using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITemplate.Domain.Entities
{
	public class Exchange
	{
		public Guid ExchangeID { get; set; }
		public string AddressTo { get; set; }
		public string AddressFrom { get; set; }
		public int Value { get; set; }
        public Guid BlockID { get; set; }
    }
}
