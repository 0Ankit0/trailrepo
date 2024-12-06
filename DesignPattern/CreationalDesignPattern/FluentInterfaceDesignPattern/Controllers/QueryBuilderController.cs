using Microsoft.AspNetCore.Mvc;

namespace FluentInterfaceDesignPattern.Controllers
{
    using FluentInterfaceDesignPattern.FIDesignPattern;
    using FluentInterfaceDesignPattern.Models;
    using Microsoft.AspNetCore.Mvc;

    public class QueryBuilderController : Controller
    {
        private readonly IQueryBuilder _queryBuilder;

        public QueryBuilderController(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateQuery([FromForm] QueryModel queryModel)
        {
            // Build the query dynamically based on user input
            string query = _queryBuilder
                   .Select(queryModel.Fields)
                   .From(queryModel.TableName)
                   .InnerJoin(queryModel.JoinTable)
                   .OnJoin(queryModel.OnCondition)
                   .Where(queryModel.WhereCondition)
                   .Limit(queryModel.Limit)
                   .Offset(queryModel.Offset)
                   .Build();

            // Pass the query to the view
            ViewBag.Query = query;
            return View("Index");
        }
    }

}
