using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    public partial class NovoProduto : ContentPage
    {
        SQLiteDatabaseHelper _db;

        public NovoProduto(SQLiteDatabaseHelper db)
        {
            InitializeComponent();
            _db = db;
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            try
            {
                var produto = new Produto
                {
                    Descricao = txtDescricao.Text,
                    Quantidade = int.Parse(txtQuantidade.Text),
                    Preco = double.Parse(txtPreco.Text)
                };

                await _db.Insert(produto);

                await DisplayAlert("Sucesso", "Produto salvo!", "OK");

                txtDescricao.Text = "";
                txtQuantidade.Text = "";
                txtPreco.Text = "";
            }
            catch
            {
                await DisplayAlert("Atenção", "Preencha os campos corretamente.", "OK");
            }
        }
    }
}