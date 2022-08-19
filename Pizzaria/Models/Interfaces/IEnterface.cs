using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria.Models.Interfaces
{
    public interface IEnterface
    {
        int Id { get; }
        DateTime DataCadastro { get; }
        DateTime DataAlteracao { get; }
    }
}