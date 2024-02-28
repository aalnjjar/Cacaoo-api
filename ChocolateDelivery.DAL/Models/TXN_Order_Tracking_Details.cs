﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateDelivery.DAL
{
    public class TXN_Order_Tracking_Details
    {
        [Key]
        public long Tracking_Id { get; set; }
        public long Order_Id { get; set; }
        public short? Status_Id { get; set; }
        public long Driver_Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime Track_Datetime { get; set; }

    }
}
