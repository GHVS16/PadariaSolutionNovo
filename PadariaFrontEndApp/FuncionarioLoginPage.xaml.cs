namespace PadariaFrontEndApp;

public partial class FuncionarioLoginPage : ContentPage
{
    public FuncionarioLoginPage()
    {
        InitializeComponent();
    }


         // Método que será chamado quando o botão "Entrar" for clicado
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text; // Acessa o texto digitado no campo de e-mail
        string senha = SenhaEntry.Text; // Acessa o texto digitado no campo de senha

        // Por enquanto, apenas exibe os valores para teste
        await DisplayAlert("Login Tentado", $"Email: {email}\nSenha: {senha}", "OK");

        await Shell.Current.GoToAsync(nameof(FuncionarioDashboardPage));

    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//InicialPage"); // Volta para a página anterior na pilha de navegação
    }
}