using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendOrama.dto;

namespace VendOrama.Interfaces
{
    public interface IVendOramaService
    {
        List<ProductAmountDto> getProducts(string cacheId);
        StockAndChangeDto Purchase(int itemId, JObject payload);
    }
}
