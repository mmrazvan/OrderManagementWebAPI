﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace OrderManagementWebAPI.DTOs
{
    public partial class OrderTrace
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string IdBoxNumber { get; set; }
        public DateTime? DateOut { get; set; }
        public string MachineId { get; set; }
    }
}