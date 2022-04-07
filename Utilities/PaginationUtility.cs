namespace MinimalTwitterApi.Utilities
{
    public static class PaginationUtility
    {
        public static PaginationCalculation CalculatePagination(int page, int limit)
        {
            var queryPage = page == 0 ? 1 : page;
            var queryLimit = limit == 0 ? 100 : limit;

            return new PaginationCalculation
            {
                QueryLimit = queryLimit,
                QuerySkip = (queryPage - 1) * queryLimit
            };
        }
    }

    public class PaginationCalculation
    {
        public int QuerySkip { get; set; }
        
        public int QueryLimit { get; set; }
    }
}