using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Descricao { get; set; }

        public int Quantidade { get; set; }

        public double Preco { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now; // NOVO CAMPO
    }
}