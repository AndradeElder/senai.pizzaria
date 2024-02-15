using ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.Entidades;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Services;

public class PizzasApiService
{
    private readonly IConfiguration Configuration;
    private readonly HttpClient _httpClient;
    private readonly string PizzaApiEndpoint;

    public PizzasApiService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        Configuration = configuration;
        PizzaApiEndpoint = Configuration["PizzaApiEndpoint"] + "/api/pizza";
    }

    public async Task<Pizza[]> Get()
    {
        return await _httpClient.GetFromJsonAsync<Pizza[]>(PizzaApiEndpoint);
    }

    public async Task<string> Create(Pizza pizza)
    {
        using (var response = await _httpClient.PostAsJsonAsync<Pizza>(PizzaApiEndpoint, pizza))
        {
            if (response.IsSuccessStatusCode)
            {
                return string.Empty;
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}

public class PedidosApiService
{
    private readonly IConfiguration Configuration;
    private readonly HttpClient _httpClient;
    private readonly string PedidosApiEndpoint;

    public PedidosApiService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        Configuration = configuration;
        PedidosApiEndpoint = Configuration["PizzaApiEndpoint"] + "/api/pedidos";
    }

    public async Task<Pedido[]> Get()
    {
        return await _httpClient.GetFromJsonAsync<Pedido[]>(PedidosApiEndpoint);
    }

    public async Task<string> Create(Pedido pedido)
    {
        using (var response = await _httpClient.PostAsJsonAsync<Pedido>(PedidosApiEndpoint, pedido))
        {
            if (response.IsSuccessStatusCode)
            {
                return string.Empty;
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}

public class ClientesApiService
{
    private readonly IConfiguration Configuration;
    private readonly HttpClient _httpClient;
    private readonly string ClientesApiEndpoint;

    public ClientesApiService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        Configuration = configuration;
        ClientesApiEndpoint = Configuration["PizzaApiEndpoint"] + "/api/clientes";
    }

    public async Task<Cliente[]> Get()
    {
        return await _httpClient.GetFromJsonAsync<Cliente[]>(ClientesApiEndpoint);
    }

    public async Task<string> Create(Cliente cliente)
    {
        using (var response = await _httpClient.PostAsJsonAsync<Cliente>(ClientesApiEndpoint, cliente))
        {
            if (response.IsSuccessStatusCode)
            {
                return string.Empty;
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}