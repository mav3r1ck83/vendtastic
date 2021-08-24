using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendOrama.dto
{
    public class StockAndChangeDto : CachedData
    {
        

        public bool PurchaseSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> orderReturn { get; set; }
    }
}
