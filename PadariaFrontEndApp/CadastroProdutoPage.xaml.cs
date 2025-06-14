using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PadariaFrontEndApp;

public partial class CadastroProdutoPage : ContentPage
{
    
    private readonly string BaseUrl = "https://localhost:7066"; // porta correta do backend

    public CadastroProdutoPage()
    {
        InitializeComponent();
    }

    private async void OnSalvarProdutoClicked(object sender, EventArgs e)
    {
        // 1. Coletar dados da UI
        string nome = NomeEntry.Text;
        string precoText = PrecoEntry.Text;
        string estoqueText = EstoqueEntry.Text;
        string unidadeMedida = UnidadeMedidaEntry.Text;
        string descricao = DescricaoEntry.Text;

        // 2. Validação e conversão de dados
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

        // 3. Criar o objeto Produto para enviar ao backend
        // ATENÇÃO: Os nomes das propriedades devem corresponder EXATAMENTE ao seu modelo 'Produto' no backend (case-sensitive)!
        var novoProduto = new
        {
            Nome = nome,
            Preco = preco,
            Estoque = estoque,
            UnidadeMedida = unidadeMedida,
            Descricao = descricao
        };

        string jsonContent = JsonSerializer.Serialize(novoProduto);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // 4. Enviar a requisição HTTP POST para o backend
        string requestUrl = $"{BaseUrl}/api/Produto"; // Endpoint para POST de Produto 

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(requestUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Sucesso", "Produto cadastrado com sucesso!", "OK");
                    // Opcional: Limpar os campos após o cadastro
                    NomeEntry.Text = string.Empty;
                    PrecoEntry.Text = string.Empty;
                    EstoqueEntry.Text = string.Empty;
                    UnidadeMedidaEntry.Text = string.Empty;
                    DescricaoEntry.Text = string.Empty;

                    // Opcional: Navegar para a lista de produtos após o cadastro
                    // await Shell.Current.GoToAsync(nameof(ListaProdutosPage));
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Erro", $"Falha ao cadastrar produto: {response.StatusCode}\nDetalhes: {errorResponse}", "OK");
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

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        // Navegar de volta para o dashboard do funcionário
        await Shell.Current.GoToAsync(nameof(FuncionarioDashboardPage));
    }
}