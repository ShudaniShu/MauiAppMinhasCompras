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
            try
            {
                base.OnAppearing();

                var produtos = await _db.GetAll();
                listaProdutos = new ObservableCollection<Produto>(produtos);
                lvProdutos.ItemsSource = listaProdutos;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void OnNovoProdutoClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new NovoProduto(_db));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string texto = e.NewTextValue?.ToLower() ?? "";

                var produtosFiltrados = listaProdutos
                    .Where(p => p.Descricao != null && p.Descricao.ToLower().Contains(texto))
                    .ToList();

                lvProdutos.ItemsSource = produtosFiltrados;
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        // 🔥 NOVO: FILTRO POR DATA
        private void OnFiltrarClicked(object sender, EventArgs e)
        {
            try
            {
                var inicio = dtInicio.Date;
                var fim = dtFim.Date;

                var filtrados = listaProdutos
                    .Where(p => p.DataCadastro >= inicio && p.DataCadastro <= fim)
                    .ToList();

                lvProdutos.ItemsSource = filtrados;
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        // 🔹 SELECIONAR ITEM (EDITAR)
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Produto produtoSelecionado = e.SelectedItem as Produto;

                if (produtoSelecionado != null)
                {
                    await Navigation.PushAsync(new NovoProduto(_db)
                    {
                        BindingContext = produtoSelecionado
                    });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        // 🔹 EXCLUIR ITEM
        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem menuItem = sender as MenuItem;
                Produto produto = menuItem.BindingContext as Produto;

                bool confirmacao = await DisplayAlert(
                    "Confirmar",
                    "Deseja excluir o produto?",
                    "Sim",
                    "Não"
                );

                if (confirmacao)
                {
                    await _db.Delete(produto.Id);
                    await DisplayAlert("Sucesso", "Produto excluído.", "OK");

                    OnAppearing();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }
}