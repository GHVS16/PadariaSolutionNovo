using Microsoft.Maui.Controls;
namespace PadariaFrontEndApp;

public partial class CadastroPage : ContentPage
{
    public CadastroPage()
    {
        InitializeComponent();
    }

    // Este método é para o Label "Já tem uma conta? Entrar"
    private async void OnLoginTapped(object sender, TappedEventArgs e)
    {
        // Alterado para navegar para a LoginPage (ou ClienteLoginPage, dependendo da sua intenção)
        // Se você tiver uma LoginPage para clientes, use "LoginPage" ou "ClienteLoginPage"
        // Se a LoginPage for a raiz principal, use "//LoginPage"
        await Shell.Current.GoToAsync("//LoginPage"); // Supondo que LoginPage é a rota raiz para o login principal
    }

    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Cadastro", "Funcionalidade de cadastro em desenvolvimento.", "OK");
        // Futuramente, você pegaria os valores dos Entry e faria a lógica de cadastro aqui.
        // string nome = NomeEntry.Text;
        // string email = EmailEntry.Text;
        // string celular = CelularEntry.Text; // Se você tiver um x:Name="CelularEntry"
        // string cpf = CpfEntry.Text; // Se você tiver um x:Name="CpfEntry"
        // string senha = SenhaEntry.Text;
        // string confirmarSenha = ConfirmarSenhaEntry.Text;

        // if (senha != confirmarSenha)
        // {
        //     await DisplayAlert("Erro de Senha", "As senhas não coincidem.", "OK");
        //     return;
        // }
        // // ... lógica para chamar a API de cadastro ...
    }

    // Este método é para o botão "Voltar" que está no conteúdo da página XAML.
    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        // Mudado para navegar diretamente para a InicialPage (a rota raiz)
        // Isso garante que sempre volte para a página inicial, independentemente da pilha.
        await Shell.Current.GoToAsync("//InicialPage");
    }

    
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        // Usará a mesma navegação do botão "Voltar" do conteúdo da página
        await Shell.Current.GoToAsync("//InicialPage");
    }
}