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
        await Shell.Current.GoToAsync("//LoginPage");
    }

    private async void OnFuncionarioClicked(object sender, EventArgs e)
    {
        // Navegar para a p�gina de Login Funcion�rio (voc� precisar� cri�-la)
        await DisplayAlert("Acesso Funcion�rio", "Funcionalidade em desenvolvimento.", "OK");
        // await Shell.Current.GoToAsync("//FuncionarioLoginPage"); // Descomente quando tiver a p�gina
    }

    private async void OnRegistrarClicked(object sender, EventArgs e)
    {
        // Navegar para a p�gina de Cadastro
        await Shell.Current.GoToAsync("//CadastroPage");
    }

   
}