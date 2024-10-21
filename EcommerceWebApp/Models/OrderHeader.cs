using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWebApp.Models
{
	public class OrderHeader
	{
		public int Id { get; set; }
		public string AppUserId { get; set; }
		[ForeignKey("AppUserId")]
		[ValidateNever]
		public AppUser AppUser { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime ShippingDate { get; set; }
		public double OrderTotal { get; set; }
		public string? OrderStatus { get; set; }
		public string? TrackingNumber { get; set; }
		public string? Carrier { get; set; }

		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		public string HouseName { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string PostalCode { get; set; }
		[Required]
		public string State { get; set; }

	}
}
