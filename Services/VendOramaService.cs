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

        public StockAndChangeDto Purchase(int pennyUsed, int dimeUsed, int nickleUsed, int quarterUsed, int pepsiPurchased, int cokePurchased, int sodaPurchased, int paidAmount, int amountDue)
        {
            StockAndChangeDto newTransaction = new StockAndChangeDto();



            int changeDue = paidAmount - amountDue;

            

            try
            {
                CachedData data = new CachedData();

                bool AlreadyExist = _cache.TryGetValue("CachedVendOrama", out data);
                if (!AlreadyExist)
                {
                    data = CreateCache();
                }
                data.PennyAmount += pennyUsed;
                data.NickelAmount += nickleUsed;
                data.DimeAmount += dimeUsed;
                data.QuerterAmount += quarterUsed;

                var PepsiCurrentStock = data.CurrentStock.FirstOrDefault(e => e.ProductName == "Pepsi");
                var CokeCurrentStock = data.CurrentStock.FirstOrDefault(e => e.ProductName == "Coke");
                var SodaCurrentStock = data.CurrentStock.FirstOrDefault(e => e.ProductName == "Soda");

                newTransaction = GetChange(changeDue, data);

                PepsiCurrentStock.ProductAmount -= pepsiPurchased;
                CokeCurrentStock.ProductAmount -= cokePurchased;
                SodaCurrentStock.ProductAmount -= sodaPurchased;


                //newTransaction = GetChange(changeDue, data);

                newTransaction.CurrentStock = data.CurrentStock;
                newTransaction.orderReturn = new List<string>();
                if (pepsiPurchased > 0) {
                    var pepsiString = ("Pepsi ordered: " + pepsiPurchased);
                    newTransaction.orderReturn.Add(pepsiString);
                }
                if (cokePurchased > 0)
                {
                    newTransaction.orderReturn.Add("Coke ordered: " + cokePurchased);
                }
                if (sodaPurchased > 0)
                {
                    newTransaction.orderReturn.Add("Soda ordered: " + sodaPurchased);
                }



            }
            catch(Exception exception)
            {
                return new StockAndChangeDto
                {
                    PurchaseSuccess = false,
                    ErrorMessage = exception.Message?? "Purchase Failed, please contact supplier"
                };
            }

            return newTransaction;
        }

        public StockAndChangeDto GetChange(int changeDue, CachedData data)
        {
            StockAndChangeDto change = new StockAndChangeDto();
            int possibleChange = 0;
            int checkChange = 0;
            var tempData = data;
            int newQuarter = data.QuerterAmount;
            int newDime = data.DimeAmount;
            int newNickel = data.NickelAmount;
            int newPenny = data.PennyAmount;
            // check change amount in quarters. 
            if (changeDue % 25 == 0 && changeDue > 25)
            {
                possibleChange = data.QuerterAmount - changeDue % 25;
                if (possibleChange >= 0)
                {
                    newQuarter -= possibleChange;
                    change.QuerterAmount = changeDue / 25;
                    return change;
                }

            }
            else if (changeDue > 25)
            {
                checkChange = changeDue - (changeDue % 25);
                possibleChange = data.QuerterAmount - checkChange / 25;
                if (possibleChange >= 0)
                {
                    
                    newQuarter -= checkChange / 25;
                    change.QuerterAmount = checkChange / 25;
                    changeDue = changeDue % 25;
                }
            }

            // check change amount in dimes
            if (changeDue % 10 == 0 && changeDue > 10)
            {
                possibleChange = data.DimeAmount - changeDue % 10;
                if (possibleChange >= 0)
                {
                    newDime -= possibleChange;
                    change.DimeAmount = changeDue / 10;
                    return change;
                }

            }
            else if (changeDue > 10)
            {
                checkChange = changeDue - (changeDue % 10);
                possibleChange = data.DimeAmount - checkChange % 10;
                if (possibleChange >= 0)
                {
                    
                    newDime -= checkChange / 10;
                    change.DimeAmount = checkChange / 10;
                    changeDue = changeDue %  10;
                }
            }
            // check for nickle change
            if (changeDue % 5 == 0 && changeDue > 5)
            {
                possibleChange = data.NickelAmount - changeDue % 5;
                if (possibleChange >= 0)
                {
                    newNickel = possibleChange;
                    change.NickelAmount = changeDue / 5;
                    return change;
                }

            }
            else if (changeDue > 5)
            {
                checkChange = changeDue - (changeDue % 5);
                possibleChange = data.NickelAmount - checkChange % 5;
                if (possibleChange >= 0)
                {

                    newNickel -= checkChange / 5;
                    change.NickelAmount = checkChange / 5;
                    changeDue = changeDue % 5;
                }
            }

            // check for Penny change
            if (changeDue <= data.PennyAmount)
            {
                newPenny -= changeDue;
                change.PennyAmount = changeDue;
            }
            else
            {
                throw new Exception("Not sufficient change in the inventory");
            }

            data.QuerterAmount = newQuarter;
            data.DimeAmount = newDime;
            data.NickelAmount = newNickel;
            data.PennyAmount = newPenny;
            change.PurchaseSuccess = true;

            return change;
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
