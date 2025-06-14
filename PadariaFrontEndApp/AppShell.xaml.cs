namespace PadariaFrontEndApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("InicialPage", typeof(InicialPage));
        Routing.RegisterRoute("LoginPage", typeof(LoginPage));
        Routing.RegisterRoute("FuncionarioLoginPage", typeof(FuncionarioLoginPage)); 
        Routing.RegisterRoute("CadastroPage", typeof(CadastroPage));
        Routing.RegisterRoute("ClienteLoginPage", typeof(ClienteLoginPage));
        Routing.RegisterRoute(nameof(FuncionarioDashboardPage), typeof(FuncionarioDashboardPage));
        Routing.RegisterRoute(nameof(CadastroProdutoPage), typeof(CadastroProdutoPage)); 
        Routing.RegisterRoute(nameof(ListaProdutosPage), typeof(ListaProdutosPage));
        Routing.RegisterRoute(nameof(EditarProdutoPage), typeof(EditarProdutoPage));
    }
}