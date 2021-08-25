using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourierManagement.Models.ViewModel
{
    public class Order
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerPhoneNumber { get; set; }

        [Required]
        public string ReceiverName { get; set; }
        [Required]
        public string ReceiverPhoneNumber { get; set; }

        [Required]
        public string ReceiverAddress { get; set; }

    }
}