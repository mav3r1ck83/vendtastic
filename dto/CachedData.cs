using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendOrama.dto
{
    public class CachedData
    {
        public int PennyAmount{ get; set; }
        public int NickelAmount { get; set; }
        public int DimeAmount { get; set; }
        public int QuerterAmount { get; set; }

        public List<ProductAmountDto> CurrentStock { get; set; }
    }
}
