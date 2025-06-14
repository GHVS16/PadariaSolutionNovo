namespace PadariaFrontEndApp;

public partial class ClienteLoginPage : ContentPage
{
	public ClienteLoginPage()
	{
		InitializeComponent();
	}
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text; // Certifique-se de que x:Name="EmailEntry" est� no XAML
        string senha = SenhaEntry.Text; // Certifique-se de que x:Name="SenhaEntry" est� no XAML

        // Por enquanto, apenas exibe os valores para teste
        await DisplayAlert("Login Tentado (Cliente)", $"Email: {email}\nSenha: {senha}", "OK");

        // *** L�gica de autentica��o real para clientes aqui: ***
        // - Chamar um servi�o de API para autenticar o cliente
        // - Se o login for bem-sucedido, navegar para a p�gina principal do cliente (ex: DashboardClientePage)
        // - Se as credenciais estiverem incorretas, exibir uma mensagem de erro (ex: "Email ou senha inv�lidos.")
    }

    // ADICIONADO: M�todo para o bot�o de voltar na barra de t�tulo
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        // Navega para a p�gina inicial (assumindo que ela � a raiz do Shell)
        await Shell.Current.GoToAsync("//InicialPage");
    }

    // ADICIONADO (se voc� incluiu o bot�o no XAML): M�todo para navegar para a p�gina de Cadastro
    private async void OnNavigateToCadastroClicked(object sender, EventArgs e)
    {
        // Navega para a p�gina de Cadastro. Use "//CadastroPage" se ela for uma rota de n�vel superior
        await Shell.Current.GoToAsync("CadastroPage");
    }
}