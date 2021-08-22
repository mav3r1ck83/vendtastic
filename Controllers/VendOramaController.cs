using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendOrama.Interfaces;
using VendOrama.dto;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VendOrama.Controllers
{
    [Route("vendOrama/[controller]")]
    [ApiController]
    public class VendOramaController : ControllerBase
    {
        private readonly IVendOramaService _vendOramaService;
        public VendOramaController(IVendOramaService vendOramaService)
        {
            _vendOramaService = vendOramaService;
        }
        // Get current Stock. 
        [HttpGet]
        public List<ProductAmountDto> Get()
        {
            var results = _vendOramaService.getProducts();
            return results;
        }


        // Purchase and return current Stock. 
        [HttpPatch("purchase/{itemId}")]
        public StockAndChangeDto Purchase(int itemId, [FromBody] JObject payload)
        {
            var stockLeftOver = _vendOramaService.Purchase(itemId, payload);
            return stockLeftOver;
        }

    }
}
