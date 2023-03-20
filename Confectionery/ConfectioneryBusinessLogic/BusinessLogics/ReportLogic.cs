using ConfectioneryBusinessLogic.OfficePackage.HelperModels;
using ConfectioneryBusinessLogic.OfficePackage;
using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IIngredientStorage _ingredientStorage;

        private readonly IPastryStorage _pastryStorage;

        private readonly IOrderStorage _orderStorage;

        private readonly AbstractSaveToExcel _saveToExcel;

        private readonly AbstractSaveToWord _saveToWord;

        private readonly AbstractSaveToPdf _saveToPdf;

        public ReportLogic(IPastryStorage pastryStorage, IIngredientStorage ingredientStorage, IOrderStorage orderStorage,
            AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf)
        {
            _pastryStorage = pastryStorage;
            _ingredientStorage = ingredientStorage;
            _orderStorage = orderStorage;

            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }

        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportPastryIngredientViewModel> GetPastryIngredient()
        {
            var ingredients = _ingredientStorage.GetFullList();

            var pastrys = _pastryStorage.GetFullList();

            var list = new List<ReportPastryIngredientViewModel>();

            foreach (var pastry in pastrys)
            {
                var record = new ReportPastryIngredientViewModel
                {
                    PastryName = pastry.PastryName,
                    Ingredients = new List<(string, int)>(),
                    TotalCount = 0
                };
                foreach (var ingredient in ingredients)
                {
                    if (pastry.PastryIngredients.ContainsKey(ingredient.Id))
                    {
                        record.Ingredients.Add(new(ingredient.IngredientName, pastry.PastryIngredients[ingredient.Id].Item2));
                        record.TotalCount += pastry.PastryIngredients[ingredient.Id].Item2;
                    }
                }

                list.Add(record);
            }

            return list;
        }

        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderSearchModel { DateFrom = model.DateFrom, DateTo = model.DateTo })
                    .Select(x => new ReportOrdersViewModel
                    {
                        Id = x.Id,
                        DateCreate = x.DateCreate,
                        PastryName = x.PastryName,
                        Sum = x.Sum,
                        Status = x.Status.ToString(),
                    })
                    .ToList();
        }

        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SavePastrysToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                Pastrys = _pastryStorage.GetFullList()
            });
        }

        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SavePastryIngredientToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                PastryIngredients = GetPastryIngredient()
            });
        }

        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom!.Value,
                DateTo = model.DateTo!.Value,
                Orders = GetOrders(model)
            });
        }
    }
}
