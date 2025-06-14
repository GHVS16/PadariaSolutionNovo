using Microsoft.Maui.Controls;
using System.Collections.ObjectModel; // Para ObservableCollection
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Security; // Adicionado para HttpClientHandler
using System.Security.Cryptography.X509Certificates; // Adicionado para HttpClientHandler

namespace PadariaFrontEndApp;

// Defina uma classe 'Produto' aqui para corresponder ao seu modelo de backend
// As propriedades devem ser IGUAIS (case-sensitive) ao seu modelo Produto.cs no backend
public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public string UnidadeMedida { get; set; }
    public string Descricao { get; set; }
}

public partial class ListaProdutosPage : ContentPage
{
    // ***** AJUSTE ESTA URL PARA A DO SEU BACKEND! *****
    // Se estiver no Windows Machine: "https://localhost:7066"
    // Se estiver no Emulador Android: "https://10.0.2.2:7066"
    private readonly string BaseUrl = "https://localhost:7066"; // Ajustado para HTTPS com base no seu Swagger UI

    public ObservableCollection<Produto> Produtos { get; set; }

    public ListaProdutosPage()
    {
        InitializeComponent();
        Produtos = new ObservableCollection<Produto>();
        BindingContext = this; // Define o BindingContext para esta p�gina

        // Carregar produtos quando a p�gina aparece
        this.Appearing += ListaProdutosPage_Appearing;
    }

    private async void ListaProdutosPage_Appearing(object sender, EventArgs e)
    {
        await CarregarProdutos();
    }

    private async Task CarregarProdutos()
    {
        ActivityIndicator.IsRunning = true; // Mostra o indicador de atividade
        Produtos.Clear(); // Limpa a lista antes de carregar

        string requestUrl = $"{BaseUrl}/api/Produto"; // Corrigido para "Produto" no singular

        try
        {
            // Configurar HttpClientHandler para ignorar a valida��o do certificado SSL para desenvolvimento
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true; // Permite qualquer certificado (N�O FA�A ISSO EM PRODU��O!)

            using (HttpClient client = new HttpClient(handler))
            {
                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    // Desserializa a resposta JSON para uma lista de objetos Produto.
                    // PropertyNameCaseInsensitive = true ajuda se a API retornar nomes de propriedade com caixa diferente (ex: "nome" vs "Nome").
                    var produtosDoBackend = JsonSerializer.Deserialize<List<Produto>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (produtosDoBackend != null)
                    {
                        foreach (var produto in produtosDoBackend)
                        {
                            Produtos.Add(produto);
                        }
                    }
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Erro", $"Falha ao carregar produtos: {response.StatusCode}\nDetalhes: {errorResponse}", "OK");
                }
            }
        }
        catch (HttpRequestException httpEx)
        {
            await DisplayAlert("Erro de Conex�o", $"N�o foi poss�vel conectar ao servidor. Verifique se o backend est� rodando e a URL est� correta. Detalhes: {httpEx.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro Inesperado", $"Ocorreu um erro: {ex.Message}", "OK");
        }
        finally
        {
            ActivityIndicator.IsRunning = false; // Esconde o indicador de atividade
        }
    }

    private async void OnAtualizarClicked(object sender, EventArgs e)
    {
        await CarregarProdutos();
    }

    private async void OnCadastrarNovoProdutoClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CadastroProdutoPage)); // Navega para a p�gina de cadastro de produto
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FuncionarioDashboardPage));
    }

    // --- M�todo para Edi��o ---
    private async void OnEditarProdutoClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        // O CommandParameter definido no XAML passa o objeto Produto completo
        var produto = button?.CommandParameter as Produto;

        if (produto != null)
        {
            // Navega para a EditarProdutoPage, passando o ID do produto como um par�metro de query
            // O nome do par�metro ("ProdutoId") deve corresponder ao QueryProperty na EditarProdutoPage
            await Shell.Current.GoToAsync($"{nameof(EditarProdutoPage)}?ProdutoId={produto.Id}");
        }
    }

    // --- M�todo para Exclus�o ---
    private async void OnExcluirProdutoClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var produto = button?.CommandParameter as Produto;

        if (produto != null)
        {
            // Pede confirma��o antes de excluir
            bool confirm = await DisplayAlert("Confirmar Exclus�o", $"Deseja realmente excluir o produto '{produto.Nome}'?", "Sim", "N�o");
            if (confirm)
            {
                // Constr�i a URL para a requisi��o DELETE, incluindo o ID do produto
                string requestUrl = $"{BaseUrl}/api/Produto/{produto.Id}";

                try
                {
                    // >>> ESTA � A MUDAN�A PARA O M�TODO DELETE <<<
                    var handler = new HttpClientHandler();
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                    using (HttpClient client = new HttpClient(handler)) // <--- PASSAR O HANDLER AQUI!
                    {
                        // Envia a requisi��o DELETE
                        HttpResponseMessage response = await client.DeleteAsync(requestUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            await DisplayAlert("Sucesso", $"Produto '{produto.Nome}' exclu�do com sucesso!", "OK");
                            // Remove o produto da ObservableCollection para que a UI seja atualizada automaticamente
                            Produtos.Remove(produto);
                        }
                        else
                        {
                            string errorResponse = await response.Content.ReadAsStringAsync();
                            await DisplayAlert("Erro", $"Falha ao excluir produto: {response.StatusCode}\nDetalhes: {errorResponse}", "OK");
                        }
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    await DisplayAlert("Erro de Conex�o", $"N�o foi poss�vel conectar ao servidor. Verifique a URL do backend. Detalhes: {httpEx.Message}", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro Inesperado", $"Ocorreu um erro: {ex.Message}", "OK");
                }
            }
        }
    }
}