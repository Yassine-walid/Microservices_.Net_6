﻿using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmout { get; set; }
    }
}
