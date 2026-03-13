using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views
{
    public partial class ListaProdutos : ContentPage
    {
        SQLiteDatabaseHelper _db;
        ObservableCollection<Produto> listaProdutos = new ObservableCollection<Produto>();

        public ListaProdutos(SQLiteDatabaseHelper db)
        {
            InitializeComponent();
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var produtos = await _db.GetAll();
            listaProdutos = new ObservableCollection<Produto>(produtos);
            cvProdutos.ItemsSource = listaProdutos;
        }

        private async void OnNovoProdutoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NovoProduto(_db));
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string texto = e.NewTextValue?.ToLower() ?? "";

            var produtosFiltrados = listaProdutos
                .Where(p => p.Descricao.ToLower().Contains(texto))
                .ToList();

            cvProdutos.ItemsSource = produtosFiltrados;
        }
    }
}