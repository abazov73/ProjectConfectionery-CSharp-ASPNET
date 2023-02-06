using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataModels.Models
{
    public interface IPastryModel : IId
    {
        string PastryName { get; }
        double Price { get; }
        Dictionary<int, (IIngredientModel, int)> PastryIngredients { get; }
    }
}
