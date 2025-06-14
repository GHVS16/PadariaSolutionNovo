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
        BindingContext = this; // Define o BindingContext para esta página

        // Carregar produtos quando a página aparece
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
            // Configurar HttpClientHandler para ignorar a validação do certificado SSL para desenvolvimento
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true; // Permite qualquer certificado (NÃO FAÇA ISSO EM PRODUÇÃO!)

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
            await DisplayAlert("Erro de Conexão", $"Não foi possível conectar ao servidor. Verifique se o backend está rodando e a URL está correta. Detalhes: {httpEx.Message}", "OK");
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
        await Shell.Current.GoToAsync(nameof(CadastroProdutoPage)); // Navega para a página de cadastro de produto
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FuncionarioDashboardPage));
    }

    // --- Método para Edição ---
    private async void OnEditarProdutoClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        // O CommandParameter definido no XAML passa o objeto Produto completo
        var produto = button?.CommandParameter as Produto;

        if (produto != null)
        {
            // Navega para a EditarProdutoPage, passando o ID do produto como um parâmetro de query
            // O nome do parâmetro ("ProdutoId") deve corresponder ao QueryProperty na EditarProdutoPage
            await Shell.Current.GoToAsync($"{nameof(EditarProdutoPage)}?ProdutoId={produto.Id}");
        }
    }

    // --- Método para Exclusão ---
    private async void OnExcluirProdutoClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var produto = button?.CommandParameter as Produto;

        if (produto != null)
        {
            // Pede confirmação antes de excluir
            bool confirm = await DisplayAlert("Confirmar Exclusão", $"Deseja realmente excluir o produto '{produto.Nome}'?", "Sim", "Não");
            if (confirm)
            {
                // Constrói a URL para a requisição DELETE, incluindo o ID do produto
                string requestUrl = $"{BaseUrl}/api/Produto/{produto.Id}";

                try
                {
                    // >>> ESTA É A MUDANÇA PARA O MÉTODO DELETE <<<
                    var handler = new HttpClientHandler();
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                    using (HttpClient client = new HttpClient(handler)) // <--- PASSAR O HANDLER AQUI!
                    {
                        // Envia a requisição DELETE
                        HttpResponseMessage response = await client.DeleteAsync(requestUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            await DisplayAlert("Sucesso", $"Produto '{produto.Nome}' excluído com sucesso!", "OK");
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
                    await DisplayAlert("Erro de Conexão", $"Não foi possível conectar ao servidor. Verifique a URL do backend. Detalhes: {httpEx.Message}", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro Inesperado", $"Ocorreu um erro: {ex.Message}", "OK");
                }
            }
        }
    }
}