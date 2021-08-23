using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
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
        [EnableCors("AllowAll")]
        [HttpGet]
        public List<ProductAmountDto> Get()
        {
            var results = _vendOramaService.getProducts();
            return results;
        }


        // Purchase and return current Stock. 
        [EnableCors("AllowAll")]
        [HttpPatch]
        public StockAndChangeDto Purchase(int pennyUsed = 0, int dimeUsed = 0, int nickeUsed = 0, int quarterUsed = 0, int pepsiPurchased = 0, int cokePurchased = 0, int sodaPurchased = 0, int paidAmount = 0, int amountDue = 0)
        {
            var stockLeftOver = _vendOramaService.Purchase(pennyUsed, dimeUsed, nickeUsed, quarterUsed, pepsiPurchased, cokePurchased, sodaPurchased, paidAmount, amountDue);
            return stockLeftOver;
        }

    }
}
