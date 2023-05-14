using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataBaseImplement.Models
{
    public class PastryIngredient
    {
        public int Id { get; set; }
        [Required]
        public int PastryId { get; set; }
        [Required]
        public int IngredientId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Ingredient Ingredient { get; set; } = new();
        public virtual Pastry Pastry { get; set; } = new();
    }
}
