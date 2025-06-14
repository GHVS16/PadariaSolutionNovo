using Microsoft.Maui.Controls;
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
        await Shell.Current.GoToAsync("ClienteLoginPage");
    }

    private async void OnFuncionarioClicked(object sender, EventArgs e)
    {
        // Navegar para a página de Login Funcionário
        await Shell.Current.GoToAsync("FuncionarioLoginPage");
        
    }

    private async void OnRegistrarClicked(object sender, EventArgs e)
    {
        // Navegar para a página de Cadastro
        await Shell.Current.GoToAsync("CadastroPage");
    }

   
}