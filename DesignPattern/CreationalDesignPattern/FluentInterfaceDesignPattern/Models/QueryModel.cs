namespace FluentInterfaceDesignPattern.Models
{
    public class QueryModel
    {
        public string TableName { get; set; }
        public string[] Fields { get; set; }
        public string JoinTable { get; set; }
        public string OnCondition { get; set; }
        public string WhereCondition { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
