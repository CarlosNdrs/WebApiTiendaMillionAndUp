using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using WebApiTienda.DTOs;
using WebApiTienda.Services;
using WebApiTienda.Helpers;
using WebApiTienda.BO;

namespace WebApiTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        private ExternalDataService externalDataService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private ProductBO _productBO;

        public ProductosController(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            externalDataService = new ExternalDataService(_configuration);          
            _mapper = mapper;
            _productBO = new ProductBO(_configuration, _mapper);
        }        

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetProducts([FromBody] ProductRequest request)
        {
            
            // Llama a la capa de negocio para obtener los productos
            var productos = await _productBO.GetProducts(request);

            return new JsonResult(productos);

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetProduct(string store, int id)
        {

            // Llama a la capa de negocio para obtener el producto
            var producto = await _productBO.GetProduct(store, id);

            return new JsonResult(producto);

        }        
    }    
}
