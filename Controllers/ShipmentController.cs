using Courier_Management_System.Data;
using Courier_Management_System.Models;
using Courier_Management_System.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courier_Management_System.Controllers
{
    public class ShipmentController : Controller
    {
        private readonly CourierDbContext courierDbContext;

        public ShipmentController(CourierDbContext courierDbContext)
        {
            this.courierDbContext = courierDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddShipmentViewModel addShipmentRequest)
        {
            var shipment = new Shipment()
            {
                Id = Guid.NewGuid(),
                ShipperName = addShipmentRequest.ShipperName,
                ShipperAddress = addShipmentRequest.ShipperAddress,
                ShipperEmail = addShipmentRequest.ShipperEmail,

                ReceiverName = addShipmentRequest.ReceiverName,
                ReceiverAddress = addShipmentRequest.ReceiverAddress,
                ReceiverEmail = addShipmentRequest.ReceiverEmail,

                ShipmentStatus = addShipmentRequest.ShipmentStatus,

            };

            await courierDbContext.Shipments.AddAsync(shipment);
            await courierDbContext.SaveChangesAsync();
            return RedirectToAction("Add");

        }

        [HttpGet]
        public async Task<IActionResult> ShipmentList()
        {
            var shipments= await courierDbContext.Shipments.ToListAsync();
            return View(shipments);
        }


 

    }
}
