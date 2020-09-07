﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerceModels.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public string ProductSKU { get; set; }
        [ForeignKey("Standard")]
        public int CategoryId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
