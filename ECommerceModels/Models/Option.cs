﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerceModels.Models
{
    public class Option
    {
        [Key]
        public int OptionId { get; set; }
        public string OptionName { get; set; }
        public IList<ProductOption> ProductOptions { get; set; }
        public IList<CartProduct> CartProducts { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
        [ForeignKey("Standard")]
        public int? OptionGroupId { get; set; }
        public OptionGroup OptionGroup { get; set; }
    }
}
