using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTienda.BO;
using WebApiTienda.Context;
using WebApiTienda.Helpers;
using WebApiTienda.Models;
using WebApiTienda.Services;

namespace WebApiTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {

        private PurchaseContext _context;
        private readonly IMapper _mapper;
        private PurchaseBO _purchaseBO;

        public PurchaseController(PurchaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _purchaseBO = new PurchaseBO(_context, _mapper);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetPurchase(int purchaseId)
        {
            // Llama a la capa de negocio para obtener los datos de la compra
            return new JsonResult(_purchaseBO.GetPurchase(purchaseId));
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult SavePurchase([FromBody] PurchaseRequest purchaseRequest)
        {

            // Inicializa respuesta
            ResponseRequest response = new ResponseRequest()
            {
                Success = false,
                OrderId = 0,
                MessageError = ""
            };
            
            // Crea registro de la compra
            int idNewPurchase = _purchaseBO.SavePurchase(purchaseRequest);

            // Si el id de la compra es mayor a cero es por que se registró exitosamente
            if (idNewPurchase > 0)
            {
                response.Success = true;
                response.OrderId = idNewPurchase;
                return new JsonResult(response);
            }
            else
            {
                response.MessageError = "Failed to save purchase";
                return new JsonResult(response);                
            }
        }
    }
}
