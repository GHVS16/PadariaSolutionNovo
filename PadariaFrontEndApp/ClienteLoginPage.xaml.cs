namespace PadariaFrontEndApp;

public partial class ClienteLoginPage : ContentPage
{
	public ClienteLoginPage()
	{
		InitializeComponent();
	}
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text; // Certifique-se de que x:Name="EmailEntry" está no XAML
        string senha = SenhaEntry.Text; // Certifique-se de que x:Name="SenhaEntry" está no XAML

        // Por enquanto, apenas exibe os valores para teste
        await DisplayAlert("Login Tentado (Cliente)", $"Email: {email}\nSenha: {senha}", "OK");

        // *** Lógica de autenticação real para clientes aqui: ***
        // - Chamar um serviço de API para autenticar o cliente
        // - Se o login for bem-sucedido, navegar para a página principal do cliente (ex: DashboardClientePage)
        // - Se as credenciais estiverem incorretas, exibir uma mensagem de erro (ex: "Email ou senha inválidos.")
    }

    // ADICIONADO: Método para o botão de voltar na barra de título
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        // Navega para a página inicial (assumindo que ela é a raiz do Shell)
        await Shell.Current.GoToAsync("//InicialPage");
    }

    // ADICIONADO (se você incluiu o botão no XAML): Método para navegar para a página de Cadastro
    private async void OnNavigateToCadastroClicked(object sender, EventArgs e)
    {
        // Navega para a página de Cadastro. Use "//CadastroPage" se ela for uma rota de nível superior
        await Shell.Current.GoToAsync("CadastroPage");
    }
}