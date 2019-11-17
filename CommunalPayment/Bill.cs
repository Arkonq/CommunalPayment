using System;
using System.Collections.Generic;
using System.Text;

namespace CommunalPayment
{
	public class Bill
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime CreationDate { get; set; } = DateTime.Now;
		public string IIN { get; set; }
		public string Street { get; set; }
		public string HouseNum { get; set; } // Номер дома может быть указан через дробь, (15/2)
		public int FlatNum { get; set; }
		public string PhoneNum { get; set; }
		public string Type { get; set; }
		public int Sum { get; set; }	// только точные цифры
	}
}
