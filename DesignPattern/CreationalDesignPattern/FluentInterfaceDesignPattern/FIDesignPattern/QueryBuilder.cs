using System.Reflection;
using System.Text;

namespace FluentInterfaceDesignPattern.FIDesignPattern
{
    public class QueryBuilder : IQueryBuilder
    {
        private string tableName;
        private List<string> selectedFields = new List<string>();
        private List<string> conditions = new List<string>();
        private List<Join> joins = new List<Join>();
        private string orderByField = null;
        private string orderDirection = null;
        private int? limit = null;
        private int? offset = null;

        public IQueryBuilder Select<T>()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                      .Select(p => p.Name);
            selectedFields.AddRange(properties);
            return this;
        }

        public IQueryBuilder Select(params string[] fields)
        {
            selectedFields.AddRange(fields);
            return this;
        }

        public IQueryBuilder From(string table)
        {
            tableName = table;
            return this;
        }

        public IQueryBuilder InnerJoin(string table)
        {
            if (!string.IsNullOrEmpty(table))
            {
                joins.Add(new Join { Type = "INNER JOIN", Table = table });
            }
            return this;
        }

        public IQueryBuilder LeftJoin(string table)
        {
            if (!string.IsNullOrEmpty(table))
            {
                joins.Add(new Join { Type = "LEFT JOIN", Table = table });
            }
            return this;
        }

        public IQueryBuilder RightJoin(string table)
        {
            if (!string.IsNullOrEmpty(table))
            {
                joins.Add(new Join { Type = "RIGHT JOIN", Table = table });
            }
            return this;
        }

        public IQueryBuilder FullJoin(string table)
        {
            if (!string.IsNullOrEmpty(table))
            {
                joins.Add(new Join { Type = "FULL JOIN", Table = table });
            }
            return this;
        }

        public IQueryBuilder OnJoin(string condition)
        {
            //if (joins.Count == 0)
            //{
            //    throw new InvalidOperationException("No join exists to apply ON condition.");
            //}
            if (!string.IsNullOrEmpty(condition))
            {
                joins[joins.Count - 1].Conditions.Add(condition);
            }
            return this;
        }

        public IQueryBuilder Where(string condition)
        {
            if (!string.IsNullOrEmpty(condition))
            {
                conditions.Add(condition);
            }
            return this;
        }

        public IQueryBuilder OrderBy(string field, string direction = "ASC")
        {
            if (!string.IsNullOrEmpty(field))
            {
                orderByField = field;
                orderDirection = direction.ToUpper();
            }
            return this;
        }

        public IQueryBuilder Limit(int count)
        {
            if (count > 0)
            {
                limit = count;
            }
            return this;
        }

        public IQueryBuilder Offset(int count)
        {
            if (count >= 0)
            {
                offset = count;
            }
            return this;
        }

        public string Build()
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new InvalidOperationException("Table name is required.");
            }

            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(selectedFields.Any() ? string.Join(", ", selectedFields) : "*");
            query.Append($" FROM {tableName}");

            foreach (var join in joins)
            {
                if (!string.IsNullOrEmpty(join.Table) && join.Conditions.Any())
                {
                    query.Append($" {join.Type} {join.Table}");
                    query.Append($" ON {string.Join(" AND ", join.Conditions)}");
                }
            }

            if (conditions.Any())
            {
                query.Append(" WHERE ");
                query.Append(string.Join(" AND ", conditions));
            }

            if (!string.IsNullOrEmpty(orderByField))
            {
                query.Append($" ORDER BY {orderByField} {orderDirection}");
            }

            if (limit.HasValue)
            {
                query.Append($" LIMIT {limit.Value}");
            }

            if (offset.HasValue)
            {
                query.Append($" OFFSET {offset.Value}");
            }

            return query.ToString();
        }

        private class Join
        {
            public string Type { get; set; }
            public string Table { get; set; }
            public List<string> Conditions { get; set; } = new List<string>();
        }
    }
}
