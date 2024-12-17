using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Xml;

namespace AdapterDesignPattern.Adapter
{
    public class ListToDataTableAdapter:IDataTableAdapter
    {
        public  DataTable ConvertToDataTable<T>(IEnumerable<T> data, params Expression<Func<T, object>>[] propertySelectors)
        {
            DataTable dataTable = new DataTable();

            if (data == null || !data.Any())
                return dataTable;

            // Get properties based on selectors or default to all
            var properties = propertySelectors != null && propertySelectors.Any()
                ? propertySelectors.Select(GetPropertyInfo).ToArray()
                : typeof(T).GetProperties();

            // Create DataTable columns
            foreach (var property in properties)
            {
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            // Populate rows
            foreach (var item in data)
            {
                var row = dataTable.NewRow();
                foreach (var property in properties)
                {
                    row[property.Name] = property.GetValue(item, null) ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
		public string DataTableToJson(DataTable dataTable)
		{
			var rows = new List<Dictionary<string, object>>();

			foreach (DataRow row in dataTable.Rows)
			{
				var rowDict = new Dictionary<string, object>();
				foreach (DataColumn column in dataTable.Columns)
				{
					rowDict[column.ColumnName] = row[column];
				}
				rows.Add(rowDict);
			}
			return JsonSerializer.Serialize(rows, new JsonSerializerOptions
			{
				WriteIndented = true // For pretty-printing
			});
			//OR you can direcyly use the Newtonsoft package to do it
			//return JsonConvert.SerializeObject(dataTable, Formatting.Indented); 
		}

		private PropertyInfo GetPropertyInfo<T>(Expression<Func<T, object>> selector)
        {
            if (selector.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression memberExpression)
            {
                return (PropertyInfo)memberExpression.Member;
            }

            if (selector.Body is MemberExpression member)
            {
                return (PropertyInfo)member.Member;
            }

            throw new ArgumentException("Selector must point to a property.", nameof(selector));
        }
    }
}
