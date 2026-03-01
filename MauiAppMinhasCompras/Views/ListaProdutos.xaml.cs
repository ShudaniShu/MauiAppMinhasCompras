using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras.Views
{
    public partial class ListaProdutos : ContentPage
    {
        SQLiteDatabaseHelper _db;

        public ListaProdutos(SQLiteDatabaseHelper db)
        {
            InitializeComponent();
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            cvProdutos.ItemsSource = await _db.GetAll();
        }

        private async void OnNovoProdutoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NovoProduto(_db));
        }
    }
}