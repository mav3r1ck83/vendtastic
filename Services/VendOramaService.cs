using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendOrama.Interfaces;
using VendOrama.dto;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace VendOrama.Services
{
    public class VendOramaService : IVendOramaService

    {
        private IMemoryCache _cache;
        public VendOramaService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public List<ProductAmountDto> getProducts()
        {
            CachedData data = new CachedData();
            bool AlreadyExist = _cache.TryGetValue("CachedVendOrama", out data);
            if (!AlreadyExist)
            {
                data = CreateCache();
            }
            List<ProductAmountDto> result = data.CurrentStock;
            return result;
        }

        public StockAndChangeDto Purchase(int itemId, JObject payload)
        {
            StockAndChangeDto result = new StockAndChangeDto();
            return result;
        }

        public CachedData CreateCache()
        {
            CachedData data = CreateNewData();
            // cache set to 60 minutes. 
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));
            _cache.Set("CachedVendOrama", data, cacheEntryOptions );
            return data;

        }

        public CachedData CreateNewData()
        {
            List<ProductAmountDto> productAmount = new List<ProductAmountDto>();
            ProductAmountDto Cokedata = new ProductAmountDto
            {
                ProductName = "Coke",
                ProductPrice = 25,
                ProductAmount = 5,
            };

            productAmount.Add(Cokedata);

            ProductAmountDto PepsiData = new ProductAmountDto
            {
                ProductName = "Pepsi",
                ProductPrice = 36,
                ProductAmount = 15
            };

            productAmount.Add(PepsiData);

            ProductAmountDto SodaData = new ProductAmountDto
            {
                ProductName = "Soda",
                ProductPrice = 45,
                ProductAmount = 3,
            };


            productAmount.Add(SodaData);


            CachedData data = new CachedData
            {
                CurrentStock = productAmount,
                PennyAmount =100,
                NickelAmount = 10,
                DimeAmount = 5,
                QuerterAmount = 25
                
            };

            return data;
        }
    }
}
