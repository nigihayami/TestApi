namespace TestApi.Models
{
    /// <summary>
    /// Модель результатов поиска
    /// </summary>
    public class SearchResultModel
    {
        public int Num { get; set; }
        public string ClusterUrl { get; set; }
        public string Content { get; set; }
    }
}
