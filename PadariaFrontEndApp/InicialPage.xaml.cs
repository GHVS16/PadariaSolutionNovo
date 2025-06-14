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
        // Navegar para a p�gina de Login Cliente (que � sua MainPage atual)
        await Shell.Current.GoToAsync("ClienteLoginPage");
    }

    private async void OnFuncionarioClicked(object sender, EventArgs e)
    {
        // Navegar para a p�gina de Login Funcion�rio
        await Shell.Current.GoToAsync("FuncionarioLoginPage");
        
    }

    private async void OnRegistrarClicked(object sender, EventArgs e)
    {
        // Navegar para a p�gina de Cadastro
        await Shell.Current.GoToAsync("CadastroPage");
    }

   
}