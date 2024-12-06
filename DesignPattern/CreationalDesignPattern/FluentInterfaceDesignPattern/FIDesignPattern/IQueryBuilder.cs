namespace FluentInterfaceDesignPattern.FIDesignPattern
{
    public interface IQueryBuilder
    {
        IQueryBuilder Select<T>();
        IQueryBuilder Select(params string[] fields);
        IQueryBuilder From(string table);
        IQueryBuilder InnerJoin(string table);
        IQueryBuilder LeftJoin(string table);
        IQueryBuilder RightJoin(string table);
        IQueryBuilder FullJoin(string table);
        IQueryBuilder OnJoin(string condition);
        IQueryBuilder Where(string condition);
        IQueryBuilder OrderBy(string field, string direction = "ASC");
        IQueryBuilder Limit(int count);
        IQueryBuilder Offset(int count);
        string Build();
    }
}
