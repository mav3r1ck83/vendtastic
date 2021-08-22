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
        List<ProductAmountDto> getProducts();
        StockAndChangeDto Purchase(int pennyUsed, int dimeUsed, int nickleUsed, int quarterUsed,  int pepsiPurchased, int cokePurchased, int sodaPurchased, int paidAmount, int amountDue);
    }
}
