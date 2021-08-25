//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourierManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class OrderList
    {
        public int TrackID { get; set; }
        [Required]
        public string Status { get; set; }
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
        
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string CurrentLocation { get; set; }
    }
}
