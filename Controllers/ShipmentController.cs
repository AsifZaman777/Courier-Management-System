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
            return RedirectToAction("ShipmentList");

        }

        [HttpGet]
        public async Task<IActionResult> ShipmentList()
        {
            var shipments= await courierDbContext.Shipments.ToListAsync();
            return View(shipments);
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid Id)
        {
            var shipmentInfoAvailable = await courierDbContext.Shipments.FirstOrDefaultAsync(x => x.Id == Id);

            if(shipmentInfoAvailable != null)
            {
                var viewModel = new UpdateShipmentViewModel()
                {
                    Id = shipmentInfoAvailable.Id,
                    ShipperName = shipmentInfoAvailable.ShipperName,
                    ShipperAddress = shipmentInfoAvailable.ShipperAddress,
                    ShipperEmail = shipmentInfoAvailable.ShipperEmail,

                    ReceiverName = shipmentInfoAvailable.ReceiverName,
                    ReceiverAddress = shipmentInfoAvailable.ReceiverAddress,
                    ReceiverEmail = shipmentInfoAvailable.ReceiverEmail,

                    ShipmentStatus = shipmentInfoAvailable.ShipmentStatus,
                };
                return await Task.Run (()=> View("View", viewModel));
            }

            return RedirectToAction("ShipmentList");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateShipmentViewModel model)
        {
            var shipments = await courierDbContext.Shipments.FindAsync(model.Id);

            if(shipments != null)
            {
                shipments.ShipperName = model.ShipperName;
                shipments.ShipperAddress = model.ShipperAddress;
                shipments.ShipperEmail = model.ShipperEmail;
                shipments.ReceiverName = model.ReceiverName;
                shipments.ReceiverAddress = model.ReceiverAddress;
                shipments.ReceiverEmail = model.ReceiverEmail;
                shipments.ShipmentStatus = model.ShipmentStatus;

               await courierDbContext.SaveChangesAsync();

                return RedirectToAction("ShipmentList");
            }
            return RedirectToAction("ShipmentList");
        }

    }
}
