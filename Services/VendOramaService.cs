using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendOrama.Interfaces;
using VendOrama.dto;
using Newtonsoft.Json.Linq;

namespace VendOrama.Services
{
    public class VendOramaService : IVendOramaService

    {
        public List<ProductAmountDto> getProducts(string cacheId)
        {
            List<ProductAmountDto> result = new List<ProductAmountDto>();
            return result;
        }

        public StockAndChangeDto Purchase(int itemId, JObject payload)
        {
            StockAndChangeDto result = new StockAndChangeDto();
            return result;
        }
    }
}
