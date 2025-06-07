namespace PadariaFrontEndApp;

public partial class InicialPage : ContentPage
{
    public InicialPage()
    {
        InitializeComponent();
    }

    private async void OnClienteClicked(object sender, EventArgs e)
    {
        // Navegar para a página de Login Cliente (que é sua MainPage atual)
        await Shell.Current.GoToAsync("//LoginPage");
    }

    private async void OnFuncionarioClicked(object sender, EventArgs e)
    {
        // Navegar para a página de Login Funcionário (você precisará criá-la)
        await DisplayAlert("Acesso Funcionário", "Funcionalidade em desenvolvimento.", "OK");
        // await Shell.Current.GoToAsync("//FuncionarioLoginPage"); // Descomente quando tiver a página
    }

    private async void OnRegistrarClicked(object sender, EventArgs e)
    {
        // Navegar para a página de Cadastro
        await Shell.Current.GoToAsync("//CadastroPage");
    }

   
}