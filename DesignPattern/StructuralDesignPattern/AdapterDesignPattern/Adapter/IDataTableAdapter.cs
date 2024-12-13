using System.Data;
using System.Linq.Expressions;

namespace AdapterDesignPattern.Adapter
{
    public interface IDataTableAdapter
    {
        DataTable ConvertToDataTable<T>(IEnumerable<T> data, params Expression<Func<T, object>>[] propertySelectors);
        string DataTableToJson(DataTable dataTable);
    }
}
