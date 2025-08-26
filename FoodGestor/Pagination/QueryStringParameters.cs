namespace FoodGestor.Pagination
{
    public abstract class QueryStringParameters
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
