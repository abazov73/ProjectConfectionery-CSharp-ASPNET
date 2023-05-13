﻿using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataBaseImplement.Models
{
    [DataContract]
    public class Ingredient : IIngredientModel
    {
        [DataMember]
        public int Id { get; private set; }
        [DataMember]
        [Required]
        public string IngredientName { get; private set; } = string.Empty;
        [DataMember]
        [Required]
        public double Cost { get; set; }
        [ForeignKey("IngredientId")]
        public virtual List<PastryIngredient> PastryIgredients { get; set; } = new();
        public static Ingredient? Create(IngredientBindingModel? model)
        {
            if (model == null)
            {
                return null;
            }
            return new Ingredient()
            {
                Id = model.Id,
                IngredientName = model.IngredientName,
                Cost = model.Cost
            };
        }
        public static Ingredient? Create(IngredientViewModel? model)
        {
            return new Ingredient()
            {
                Id = model.Id,
                IngredientName = model.IngredientName,
                Cost = model.Cost
            };
        }
        public void Update(IngredientBindingModel? model)
        {
            if (model == null)
            {
                return;
            }
            IngredientName = model.IngredientName;
            Cost = model.Cost;
        }
        public IngredientViewModel GetViewModel => new()
        {
            Id = Id,
            IngredientName = IngredientName,
            Cost = Cost
        };
    }
}
