using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzariaAtv.Models.ViewModels.Request
{
    public class PostTamanhoDTO
    {
        
        public string Nome { get; set; }
   
        public string Descricao { get; set; }
        public List<int> Id { get; set; }
    }
}