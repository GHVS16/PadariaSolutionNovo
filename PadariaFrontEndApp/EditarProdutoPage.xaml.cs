
using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Security; // Necessário para HttpClientHandler
using System.Security.Cryptography.X509Certificates; // Necessário para HttpClientHandler

namespace PadariaFrontEndApp;

// Adicione os atributos de QueryProperty para receber o ProdutoId
[QueryProperty(nameof(ProdutoId), "ProdutoId")]
public partial class EditarProdutoPage : ContentPage
{
   
    private readonly string BaseUrl = "https://localhost:7066"; // Usando HTTPS

    // Propriedade para receber o ID do produto via navegação
    private int _produtoId;
    public int ProdutoId
    {
        get => _produtoId;
        set
        {
            _produtoId = value;
            LoadProdutoData(value); // Chama o método para carregar os dados quando o ID é definido
        }
    }

    public EditarProdutoPage()
    {
        InitializeComponent();
    }

    private async void LoadProdutoData(int id)
    {
        string requestUrl = $"{BaseUrl}/api/Produto/{id}"; // Endpoint para GET de um produto específico 

        try
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true; // Ignora o certificado SSL

            using (HttpClient client = new HttpClient(handler))
            {
                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    // A classe 'Produto' precisa estar definida e ser acessível.
                    // Ela pode estar no ListaProdutosPage.xaml.cs (e será acessível)
                    // ou você pode copiá-la para este arquivo se preferir.
                    var produto = JsonSerializer.Deserialize<Produto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (produto != null)
                    {
                        // Preenche os campos da UI com os dados do produto
                        IdLabel.Text = produto.Id.ToString(); // Mantém o ID em um Label invisível
                        NomeEntry.Text = produto.Nome;
                        PrecoEntry.Text = produto.Preco.ToString();
                        EstoqueEntry.Text = produto.Estoque.ToString();
                        UnidadeMedidaEntry.Text = produto.UnidadeMedida;
                        DescricaoEditor.Text = produto.Descricao;
                    }
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Erro", $"Falha ao carregar dados do produto: {response.StatusCode}\nDetalhes: {errorResponse}", "OK");
                    await Shell.Current.GoToAsync(nameof(ListaProdutosPage)); // Corrigido para rota explícita
                }
            }
        }
        catch (HttpRequestException httpEx)
        {
            await DisplayAlert("Erro de Conexão", $"Não foi possível conectar ao servidor. Detalhes: {httpEx.Message}", "OK");
            await Shell.Current.GoToAsync(nameof(ListaProdutosPage)); // Corrigido para rota explícita
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro Inesperado", $"Ocorreu um erro: {ex.Message}", "OK");
            await Shell.Current.GoToAsync(nameof(ListaProdutosPage)); // Corrigido para rota explícita
        }
    }

    private async void OnSalvarAlteracoesClicked(object sender, EventArgs e)
    {
        // 1. Coletar dados da UI
        int id = ProdutoId; // Pega o ID da propriedade que veio pela navegação
        string nome = NomeEntry.Text;
        string precoText = PrecoEntry.Text;
        string estoqueText = EstoqueEntry.Text;
        string unidadeMedida = UnidadeMedidaEntry.Text;
        string descricao = DescricaoEditor.Text;

        // 2. Validação e conversão de dados (similar ao CadastroProdutoPage)
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(precoText) || string.IsNullOrWhiteSpace(estoqueText))
        {
            await DisplayAlert("Erro", "Nome, Preço e Estoque são obrigatórios.", "OK");
            return;
        }

        if (!decimal.TryParse(precoText, out decimal preco) || preco <= 0)
        {
            await DisplayAlert("Erro", "Preço inválido. Use um número maior que zero.", "OK");
            return;
        }

        if (!int.TryParse(estoqueText, out int estoque) || estoque < 0)
        {
            await DisplayAlert("Erro", "Estoque inválido. Use um número inteiro maior ou igual a zero.", "OK");
            return;
        }

        // 3. Criar o objeto Produto para enviar ao backend (incluindo o ID!)
        var produtoAtualizado = new
        {
            Id = id, // É crucial enviar o ID para uma operação PUT
            Nome = nome,
            Preco = preco,
            Estoque = estoque,
            UnidadeMedida = unidadeMedida,
            Descricao = descricao
        };

        string jsonContent = JsonSerializer.Serialize(produtoAtualizado);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // 4. Enviar a requisição HTTP PUT para o backend
        string requestUrl = $"{BaseUrl}/api/Produto/{id}"; // Endpoint para PUT de Produto (singular!)

        try
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            using (HttpClient client = new HttpClient(handler))
            {
                HttpResponseMessage response = await client.PutAsync(requestUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Sucesso", "Produto atualizado com sucesso!", "OK");
                    await Shell.Current.GoToAsync(nameof(ListaProdutosPage)); // Corrigido para rota explícita
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Erro", $"Falha ao atualizar produto: {response.StatusCode}\nDetalhes: {errorResponse}", "OK");
                }
            }
        }
        catch (HttpRequestException httpEx)
        {
            await DisplayAlert("Erro de Conexão", $"Não foi possível conectar ao servidor. Detalhes: {httpEx.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro Inesperado", $"Ocorreu um erro: {ex.Message}", "OK");
        }
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ListaProdutosPage)); // Corrigido para rota explícita
    }
}