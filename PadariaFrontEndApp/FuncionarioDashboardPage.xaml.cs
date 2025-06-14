namespace PadariaFrontEndApp;

public partial class FuncionarioDashboardPage : ContentPage
{
    public FuncionarioDashboardPage()
    {
        InitializeComponent();
    }

    private async void OnCadastrarProdutoClicked(object sender, EventArgs e)
    {
        // Navega para a p�gina de cadastro de produto
        // Certifique-se de que "CadastroProdutoPage" esteja registrada em AppShell.xaml.cs
        await Shell.Current.GoToAsync(nameof(CadastroProdutoPage));
    }

    private async void OnVerProdutosClicked(object sender, EventArgs e)
    {
        // Navega para a p�gina de listagem de produtos
        // Certifique-se de que "ListaProdutosPage" esteja registrada em AppShell.xaml.cs
        await Shell.Current.GoToAsync(nameof(ListaProdutosPage));
    }

    private async void OnSairClicked(object sender, EventArgs e)
    {
        // L�gica para deslogar o funcion�rio, limpar sess�es, etc.
        // Por exemplo, limpar dados de usu�rio armazenados localmente.

        // Navegar de volta para a p�gina inicial (ou login do funcion�rio)
        // Usamos // para ir para a raiz do Shell e InicialPage
        await Shell.Current.GoToAsync("//InicialPage");
    }
}