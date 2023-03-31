using Newtonsoft.Json;
using System.IO;
using WebApiTienda.Models;

namespace WebApiTienda.Services
{
    public class ExternalDataService
    {
        private IConfiguration Configuration;
        private string apiEndpointFakeStore;
        private string apiEndpointDummyJson;

        public ExternalDataService(IConfiguration _Configuracion)
        {
            Configuration = _Configuracion;
            apiEndpointFakeStore = Configuration["ExternalHosts:FakeStoreApi"];
            apiEndpointDummyJson = Configuration["ExternalHosts:DummyJson"];
        }

        // Obtiene todos los productos del API FakeStore
        public async Task<List<FakeStoreProduct>> GetFakeStoreProducts()
        {
            // Listado de productos de FakeStore
            List<FakeStoreProduct> FakeStoreProducts = new List<FakeStoreProduct>();
            var apiClient = new HttpClient();

            // Consulta los productos llamando al API de FakeStore
            var responseGet = await apiClient.GetAsync(apiEndpointFakeStore);

            // Si la respuesta es exitosa deserializa y retorna los resultados
            if (responseGet.IsSuccessStatusCode)
            {
                var data = await responseGet.Content.ReadAsStringAsync();
                FakeStoreProducts = JsonConvert.DeserializeObject<List<FakeStoreProduct>>(data);
                return FakeStoreProducts;
            }

            return FakeStoreProducts;
        }

        public async Task<List<DummyJsonProduct>> GetDummyJsonProducts()
        {
            // Inicializa respuesta del API DummyJson
            DummyJsonResponse dummyJsonResponse = new();

            var apiClient = new HttpClient();

            //Consulta los productos del API DummyJson
            var responseGet = await apiClient.GetAsync(apiEndpointDummyJson);

            // Si la respuesta es exitosa deserializa y retorna los resultados
            if (responseGet.IsSuccessStatusCode)
            {
                var data = await responseGet.Content.ReadAsStringAsync();
                dummyJsonResponse = JsonConvert.DeserializeObject<DummyJsonResponse>(data);
                return dummyJsonResponse.Products.ToList();
            }

            return dummyJsonResponse.Products.ToList();
        }

        // Obtiene 1 producto particular del API FakeStore
        public async Task<FakeStoreProduct> GetFakeStoreProduct(int id)
        {
            FakeStoreProduct fakeStoreProduct = new FakeStoreProduct();
            var apiClient = new HttpClient();
            var responseGet = await apiClient.GetAsync(apiEndpointFakeStore+"/"+id);

            if (responseGet.IsSuccessStatusCode)
            {
                var data = await responseGet.Content.ReadAsStringAsync();
                fakeStoreProduct = JsonConvert.DeserializeObject<FakeStoreProduct>(data);
                return fakeStoreProduct;
            }

            return fakeStoreProduct;
        }

        // Obtiene todos los productos del API DummyJson
        public async Task<DummyJsonProduct> GetDummyJsonProduct(int id)
        {
            DummyJsonProduct dummyJsonProduct = new();

            var apiClient = new HttpClient();
            var responseGet = await apiClient.GetAsync(apiEndpointDummyJson+"/"+id);

            if (responseGet.IsSuccessStatusCode)
            {
                var data = await responseGet.Content.ReadAsStringAsync();
                dummyJsonProduct = JsonConvert.DeserializeObject<DummyJsonProduct>(data);
                return dummyJsonProduct;
            }

            return dummyJsonProduct;
        }
    }
}
