using Microsoft.Maui.Controls;
namespace PadariaFrontEndApp;

public partial class CadastroPage : ContentPage
{
    public CadastroPage()
    {
        InitializeComponent();
    }

    // Este m�todo � para o Label "J� tem uma conta? Entrar"
    private async void OnLoginTapped(object sender, TappedEventArgs e)
    {
        // Alterado para navegar para a LoginPage (ou ClienteLoginPage, dependendo da sua inten��o)
        // Se voc� tiver uma LoginPage para clientes, use "LoginPage" ou "ClienteLoginPage"
        // Se a LoginPage for a raiz principal, use "//LoginPage"
        await Shell.Current.GoToAsync("//LoginPage"); // Supondo que LoginPage � a rota raiz para o login principal
    }

    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Cadastro", "Funcionalidade de cadastro em desenvolvimento.", "OK");
        // Futuramente, voc� pegaria os valores dos Entry e faria a l�gica de cadastro aqui.
        // string nome = NomeEntry.Text;
        // string email = EmailEntry.Text;
        // string celular = CelularEntry.Text; // Se voc� tiver um x:Name="CelularEntry"
        // string cpf = CpfEntry.Text; // Se voc� tiver um x:Name="CpfEntry"
        // string senha = SenhaEntry.Text;
        // string confirmarSenha = ConfirmarSenhaEntry.Text;

        // if (senha != confirmarSenha)
        // {
        //     await DisplayAlert("Erro de Senha", "As senhas n�o coincidem.", "OK");
        //     return;
        // }
        // // ... l�gica para chamar a API de cadastro ...
    }

    // Este m�todo � para o bot�o "Voltar" que est� no conte�do da p�gina XAML.
    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        // Mudado para navegar diretamente para a InicialPage (a rota raiz)
        // Isso garante que sempre volte para a p�gina inicial, independentemente da pilha.
        await Shell.Current.GoToAsync("//InicialPage");
    }

    
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        // Usar� a mesma navega��o do bot�o "Voltar" do conte�do da p�gina
        await Shell.Current.GoToAsync("//InicialPage");
    }
}