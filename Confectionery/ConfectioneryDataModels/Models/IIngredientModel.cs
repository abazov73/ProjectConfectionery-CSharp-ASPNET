using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataModels.Models
{
    public interface IIngredientModel : IId
    {
        string IngredientName { get; }
        double Cost { get; }
    }
}
