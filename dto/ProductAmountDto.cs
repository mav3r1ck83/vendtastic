using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendOrama.dto
{
    [Serializable]
    public class ProductAmountDto
    {
        public int ProductAmount { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }

    }
}
