namespace PadariaFrontEndApp;

public partial class FuncionarioDashboardPage : ContentPage
{
    public FuncionarioDashboardPage()
    {
        InitializeComponent();
    }

    private async void OnCadastrarProdutoClicked(object sender, EventArgs e)
    {
        // Navega para a página de cadastro de produto
        // Certifique-se de que "CadastroProdutoPage" esteja registrada em AppShell.xaml.cs
        await Shell.Current.GoToAsync(nameof(CadastroProdutoPage));
    }

    private async void OnVerProdutosClicked(object sender, EventArgs e)
    {
        // Navega para a página de listagem de produtos
        // Certifique-se de que "ListaProdutosPage" esteja registrada em AppShell.xaml.cs
        await Shell.Current.GoToAsync(nameof(ListaProdutosPage));
    }

    private async void OnSairClicked(object sender, EventArgs e)
    {
        // Lógica para deslogar o funcionário, limpar sessões, etc.
        // Por exemplo, limpar dados de usuário armazenados localmente.

        // Navegar de volta para a página inicial (ou login do funcionário)
        // Usamos // para ir para a raiz do Shell e InicialPage
        await Shell.Current.GoToAsync("//InicialPage");
    }
}