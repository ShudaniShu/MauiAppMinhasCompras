using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Views;

namespace MauiAppMinhasCompras;

public partial class App : Application
{
    static SQLiteDatabaseHelper _db;

    public App()
    {
        InitializeComponent();

        string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "produtos.db3");

        _db = new SQLiteDatabaseHelper(dbPath);

        MainPage = new NavigationPage(new ListaProdutos(_db));
    }
}