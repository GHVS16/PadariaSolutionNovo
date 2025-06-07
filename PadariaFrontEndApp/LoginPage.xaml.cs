namespace PadariaFrontEndApp;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    // Método para o Label "Esqueceu sua senha?"
    private void OnForgotPasswordTapped(object sender, TappedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("O link 'Esqueceu sua senha?' foi clicado.");
        
    }

    
    private async void OnCriarContaClicked(object sender, EventArgs e)
    {
        // Navega para a página de cadastro usando a rota que definimos no AppShell.xaml
        await Shell.Current.GoToAsync("//CadastroPage"); 
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        // Volta para a página anterior na pilha (InicialPage, neste caso)
        await Shell.Current.GoToAsync("..");
    }
}