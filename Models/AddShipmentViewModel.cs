﻿namespace Courier_Management_System.Models
{
    public class AddShipmentViewModel
    {
        public string ShipperName { get; set; }
        public string ShipperAddress { get; set; }
        public string ShipperEmail { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverEmail { get; set; }

        public string ShipmentStatus { get; set; }
    }
}
