using Google.API.Search;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TestApi.Models;

namespace TestApi
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HashSet<SearchResultModel> modelSearch1 = new HashSet<SearchResultModel> { };
        private ICollectionView viewSearch1;

        private HashSet<SearchResultModel> modelSearch2 = new HashSet<SearchResultModel> { };
        private ICollectionView viewSearch2;

        private HashSet<SearchResultModel> modelSearch3 = new HashSet<SearchResultModel> { };
        private ICollectionView viewSearch3;

        private HashSet<SearchResultModel> modelSearch4 = new HashSet<SearchResultModel> { };
        private ICollectionView viewSearch4;
        public MainWindow()
        {
            InitializeComponent();

            this.btnSearch1.Click += BtnSearch1_Click;
            this.btnSearch2.Click += BtnSearch2_Click;
            this.btnSearch3.Click += BtnSearch3_Click;
            this.btnSearch4.Click += BtnSearch4_Click;

            viewSearch1 = CollectionViewSource.GetDefaultView(modelSearch1);
            this.listSearchResult1.ItemsSource = viewSearch1;

            viewSearch2 = CollectionViewSource.GetDefaultView(modelSearch2);
            this.listSearchResult2.ItemsSource = viewSearch2;

            viewSearch3 = CollectionViewSource.GetDefaultView(modelSearch3);
            this.listSearchResult3.ItemsSource = viewSearch3;

            viewSearch4 = CollectionViewSource.GetDefaultView(modelSearch4);
            this.listSearchResult4.ItemsSource = viewSearch4;
        }
        /// <summary>
        /// Поиск Web
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void BtnSearch4_Click(object sender, RoutedEventArgs e)
        {
            UpdateModel(await SearchMode(this.textSearch1.Text, 4), ref modelSearch4, ref viewSearch4, 4);
        }
        /// <summary>
        /// Поиск Блоги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void BtnSearch3_Click(object sender, RoutedEventArgs e)
        {
            UpdateModel(await SearchMode(this.textSearch1.Text, 3), ref modelSearch3, ref viewSearch3, 3);
        }
        /// <summary>
        /// Поиск Видео
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void BtnSearch2_Click(object sender, RoutedEventArgs e)
        {
            UpdateModel(await SearchMode(this.textSearch1.Text, 2), ref modelSearch2, ref viewSearch2, 2);
        }

        /// <summary>
        /// Поиск Новости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void BtnSearch1_Click(object sender, RoutedEventArgs e)
        {
            UpdateModel(await SearchMode(this.textSearch1.Text, 1), ref modelSearch1, ref viewSearch1, 1);
        }

        /// <summary>
        /// Очистить модель данных
        /// </summary>
        private void ModelClear(ref HashSet<SearchResultModel> model)
        {
            model.Clear();
        }
        /// <summary>
        /// Обновить представление
        /// </summary>
        private void ViewRefresh(ref ICollectionView view)
        {
            view.Refresh();
        }        
        /// <summary>
        /// Обновление Модели
        /// </summary>
        /// <param name="result"></param>
        private void UpdateModel(List<SearchResultModel> result, ref HashSet<SearchResultModel> model, ref ICollectionView view, int mode = 1)
        {
            ModelClear(ref model);

            foreach (var item in result)
            {
                model.Add(item);
            }

            ViewRefresh(ref view);
        }
        /// <summary>
        /// Общий режим поиска
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        async private Task<List<SearchResultModel>> SearchMode(string searchText, int mode = 1)
        {
            return await Task<List<SearchResultModel>>.Factory.StartNew(() => Search(searchText, mode));
        }
        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="searchText">Текст для поиска</param>
        /// <param name="mode">Тип поиска</param>
        /// <returns></returns>
        private List<SearchResultModel> Search(string searchText, int mode)
        {
            var resultOut = new List<SearchResultModel> { };
            switch (mode)
            {
                case 1:
                    GnewsSearchClient t = new GnewsSearchClient("");
                    var SearchResult = t.Search(searchText, 10);
                    foreach (var item in SearchResult)
                    {
                        resultOut.Add(new SearchResultModel { ClusterUrl = item.ClusterUrl, Content = item.Content, Num = 0 });
                    }
                    break;
                case 2:
                    GvideoSearchClient t2 = new GvideoSearchClient("");
                    var SearchResult2 = t2.Search(searchText, 10);
                    foreach (var item in SearchResult2)
                    {
                        resultOut.Add(new SearchResultModel { ClusterUrl = item.PlayUrl, Content = item.Content, Num = 0 });
                    }
                    break;
                case 3:
                    GblogSearchClient t3 = new GblogSearchClient("");
                    var SearchResult3 = t3.Search(searchText, 10);
                    foreach (var item in SearchResult3)
                    {
                        resultOut.Add(new SearchResultModel { ClusterUrl = item.BlogUrl, Content = item.Content, Num = 0 });
                    }
                    break;
                case 4:
                    GwebSearchClient t4 = new GwebSearchClient("");
                    var SearchResult4 = t4.Search(searchText, 10);
                    foreach (var item in SearchResult4)
                    {
                        resultOut.Add(new SearchResultModel { ClusterUrl = item.Url, Content = item.Content, Num = 0 });
                    }
                    break;

            }
            return resultOut;
        }
    }
}
