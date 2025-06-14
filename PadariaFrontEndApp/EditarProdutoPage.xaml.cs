
using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Security; // Necess�rio para HttpClientHandler
using System.Security.Cryptography.X509Certificates; // Necess�rio para HttpClientHandler

namespace PadariaFrontEndApp;

// Adicione os atributos de QueryProperty para receber o ProdutoId
[QueryProperty(nameof(ProdutoId), "ProdutoId")]
public partial class EditarProdutoPage : ContentPage
{
   
    private readonly string BaseUrl = "https://localhost:7066"; // Usando HTTPS

    // Propriedade para receber o ID do produto via navega��o
    private int _produtoId;
    public int ProdutoId
    {
        get => _produtoId;
        set
        {
            _produtoId = value;
            LoadProdutoData(value); // Chama o m�todo para carregar os dados quando o ID � definido
        }
    }

    public EditarProdutoPage()
    {
        InitializeComponent();
    }

    private async void LoadProdutoData(int id)
    {
        string requestUrl = $"{BaseUrl}/api/Produto/{id}"; // Endpoint para GET de um produto espec�fico 

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
                    // A classe 'Produto' precisa estar definida e ser acess�vel.
                    // Ela pode estar no ListaProdutosPage.xaml.cs (e ser� acess�vel)
                    // ou voc� pode copi�-la para este arquivo se preferir.
                    var produto = JsonSerializer.Deserialize<Produto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (produto != null)
                    {
                        // Preenche os campos da UI com os dados do produto
                        IdLabel.Text = produto.Id.ToString(); // Mant�m o ID em um Label invis�vel
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
                    await Shell.Current.GoToAsync(nameof(ListaProdutosPage)); // Corrigido para rota expl�cita
                }
            }
        }
        catch (HttpRequestException httpEx)
        {
            await DisplayAlert("Erro de Conex�o", $"N�o foi poss�vel conectar ao servidor. Detalhes: {httpEx.Message}", "OK");
            await Shell.Current.GoToAsync(nameof(ListaProdutosPage)); // Corrigido para rota expl�cita
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro Inesperado", $"Ocorreu um erro: {ex.Message}", "OK");
            await Shell.Current.GoToAsync(nameof(ListaProdutosPage)); // Corrigido para rota expl�cita
        }
    }

    private async void OnSalvarAlteracoesClicked(object sender, EventArgs e)
    {
        // 1. Coletar dados da UI
        int id = ProdutoId; // Pega o ID da propriedade que veio pela navega��o
        string nome = NomeEntry.Text;
        string precoText = PrecoEntry.Text;
        string estoqueText = EstoqueEntry.Text;
        string unidadeMedida = UnidadeMedidaEntry.Text;
        string descricao = DescricaoEditor.Text;

        // 2. Valida��o e convers�o de dados (similar ao CadastroProdutoPage)
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(precoText) || string.IsNullOrWhiteSpace(estoqueText))
        {
            await DisplayAlert("Erro", "Nome, Pre�o e Estoque s�o obrigat�rios.", "OK");
            return;
        }

        if (!decimal.TryParse(precoText, out decimal preco) || preco <= 0)
        {
            await DisplayAlert("Erro", "Pre�o inv�lido. Use um n�mero maior que zero.", "OK");
            return;
        }

        if (!int.TryParse(estoqueText, out int estoque) || estoque < 0)
        {
            await DisplayAlert("Erro", "Estoque inv�lido. Use um n�mero inteiro maior ou igual a zero.", "OK");
            return;
        }

        // 3. Criar o objeto Produto para enviar ao backend (incluindo o ID!)
        var produtoAtualizado = new
        {
            Id = id, // � crucial enviar o ID para uma opera��o PUT
            Nome = nome,
            Preco = preco,
            Estoque = estoque,
            UnidadeMedida = unidadeMedida,
            Descricao = descricao
        };

        string jsonContent = JsonSerializer.Serialize(produtoAtualizado);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // 4. Enviar a requisi��o HTTP PUT para o backend
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
                    await Shell.Current.GoToAsync(nameof(ListaProdutosPage)); // Corrigido para rota expl�cita
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
            await DisplayAlert("Erro de Conex�o", $"N�o foi poss�vel conectar ao servidor. Detalhes: {httpEx.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro Inesperado", $"Ocorreu um erro: {ex.Message}", "OK");
        }
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ListaProdutosPage)); // Corrigido para rota expl�cita
    }
}