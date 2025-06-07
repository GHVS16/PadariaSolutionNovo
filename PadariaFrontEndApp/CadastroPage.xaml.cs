namespace PadariaFrontEndApp;

public partial class CadastroPage : ContentPage
{
    public CadastroPage()
    {
        InitializeComponent();
    }

    private async void OnLoginTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Cadastro", "Funcionalidade de cadastro em desenvolvimento.", "OK");
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        // Volta para a página anterior na pilha (InicialPage, neste caso)
        await Shell.Current.GoToAsync("..");
    }
}