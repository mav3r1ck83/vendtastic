using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendOrama.Entities
{
    public abstract class Drink: BaseEntity
    {
        public int DrinkTypeId { get; set; }
    }
}
