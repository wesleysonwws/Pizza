using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria.Models
{
   
        public class PizzasSabores
        {
            public PizzasSabores(int pizzaId, int saborId)
            {
                PizzaId = pizzaId;
                SaborId = saborId;
            }

            public Pizza Pizza { get; set; }
            public int PizzaId { get; set; }
            public Tamanho Tamanho{ get; set; }
            public int SaborId { get; set; }

        }
    }


