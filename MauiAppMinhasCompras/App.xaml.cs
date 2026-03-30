using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Views;
using System.Globalization;

namespace MauiAppMinhasCompras;

public partial class App : Application
{
    static SQLiteDatabaseHelper _db;

    public App()
    {
        InitializeComponent();

        // 🔥 DEFINIR CULTURA BRASILEIRA
        var cultura = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentCulture = cultura;
        CultureInfo.DefaultThreadCurrentUICulture = cultura;

        string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "produtos.db3");

        _db = new SQLiteDatabaseHelper(dbPath);

        MainPage = new NavigationPage(new ListaProdutos(_db));
    }
}