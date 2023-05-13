using ConfectioneryContracts.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confectionery
{
    internal static class DataGridViewExtension
    {
        public static void FillAndConfigGrid<T>(this DataGridView grid, List<T>? data)
        {
            if (data == null)
            {
                return;
            }
            grid.DataSource = data;

            var type = typeof(T);
            var properties = type.GetProperties();
            foreach (DataGridViewColumn column in grid.Columns)
            {
                var property = properties.FirstOrDefault(x => x.Name == column.Name);
                if (property == null)
                {
                    throw new InvalidOperationException($"В типе {type.Name} не найдено свойство с именем {column.Name}");
                }
                var attribute = property.GetCustomAttributes(typeof(ColumnAttribute), true)?.SingleOrDefault();
                if (attribute == null)
                {
                    throw new InvalidOperationException($"Не найден атрибут типа ColumnAttribute для свойства {property.Name}");
                }
                // ищем нужный нам атрибут
                if (attribute is ColumnAttribute columnAttr)
                {
                    column.HeaderText = columnAttr.Title;
                    column.Visible = columnAttr.Visible;
                    if (columnAttr.IsUseAutoSize)
                    {
                        column.AutoSizeMode = (DataGridViewAutoSizeColumnMode)Enum.Parse(typeof(DataGridViewAutoSizeColumnMode), columnAttr.GridViewAutoSize.ToString());
                    }
                    else
                    {
                        column.Width = columnAttr.Width;
                    }
                }
            }
        }
    }
}
