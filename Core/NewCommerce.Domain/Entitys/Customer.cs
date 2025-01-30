﻿using NewCommerce.Domain.Entitys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Domain.Entitys
{
    public class Customer:BaseEntity
    {
        public string? Name { get; set; }
        public string SurName { get; set; }
        public ICollection<Order> Orders { get; set; }  
    }
}
