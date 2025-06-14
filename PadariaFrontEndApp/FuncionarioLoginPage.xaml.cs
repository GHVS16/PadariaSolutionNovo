namespace PadariaFrontEndApp;

public partial class FuncionarioLoginPage : ContentPage
{
    public FuncionarioLoginPage()
    {
        InitializeComponent();
    }


         // M�todo que ser� chamado quando o bot�o "Entrar" for clicado
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
        await Shell.Current.GoToAsync("//InicialPage"); // Volta para a p�gina anterior na pilha de navega��o
    }
}