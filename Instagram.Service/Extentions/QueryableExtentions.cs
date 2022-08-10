namespace Instagram.Service.Extentions
{
    public static class QueryableExtentions
    {
        public static IQueryable<T> GetWithPagenation<T>(this IQueryable<T> values, Tuple<int, int> pagenation = null)
            where T : class
        {
            if (pagenation == null)
                return values.AsQueryable();

            return values.Skip((pagenation.Item1 - 1) * pagenation.Item2).Take(pagenation.Item2);
        }
    }
}
