using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendOrama.dto
{
    public class StockAndChangeDto
    {
        
        public int PennyChange { get; set; }
        public int NickelChange { get; set; }
        public int DimeChange { get; set; }
        public int QuerterChange { get; set; }

        public List<ProductAmountDto> CurrentStock { get; set; }
        public bool PurchaseSuccess { get; set; }
    }
}
