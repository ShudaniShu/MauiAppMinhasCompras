using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;
using System.Xml.Linq;

namespace MauiAppMinhasCompras.Views
{
    public partial class NovoProduto : ContentPage
    {
        SQLiteDatabaseHelper _db;
        Produto produtoAtual;

        public NovoProduto(SQLiteDatabaseHelper db)
        {
            InitializeComponent();
            _db = db;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                produtoAtual = BindingContext as Produto;

                if (produtoAtual != null)
                {
                    txtDescricao.Text = produtoAtual.Descricao;
                    txtQuantidade.Text = produtoAtual.Quantidade.ToString();
                    txtPreco.Text = produtoAtual.Preco.ToString();

                    // 🔥 NOVO: carregar data ao editar
                    dtData.Date = produtoAtual.DataCadastro;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            try
            {
                Produto p = new Produto
                {
                    Descricao = txtDescricao.Text,
                    Quantidade = Convert.ToInt32(txtQuantidade.Text),
                    Preco = Convert.ToDouble(txtPreco.Text),

                    // 🔥 NOVO: salvar a data
                    DataCadastro = dtData.Date
                };

                if (produtoAtual != null)
                {
                    // EDITAR
                    p.Id = produtoAtual.Id;

                    // ⚠️ IMPORTANTE: manter data existente se não mudar
                    if (produtoAtual.DataCadastro != default)
                        p.DataCadastro = produtoAtual.DataCadastro;

                    await _db.Update(p);

                    await DisplayAlert("Sucesso", "Produto atualizado!", "OK");
                }
                else
                {
                    // NOVO
                    await _db.Insert(p);

                    await DisplayAlert("Sucesso", "Produto cadastrado!", "OK");
                }

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }
}