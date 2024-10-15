using System.Text;
using RestSharp;
using ApiTestForQatl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiTestForQatl;

public class Controller
{
    private readonly RestClient _client = new(Urls.BaseUrl);
    private HttpClient _httpClient = new HttpClient{ BaseAddress = new Uri("http://shop.qatl.ru") };

    public async Task<JArray> GetAll()
    {
        var response =  _httpClient.GetAsync($"{Urls.AllProducts}").Result;
        
        if (!response.IsSuccessStatusCode) throw new Exception("Failed to get all products. Response: " + response.Content);
        
        var jsonString = await response.Content.ReadAsStringAsync();
        
        return (JArray)JToken.Parse(jsonString);
    }
    
    public async Task<JObject> Delete(int id)
    {
        var response =  _httpClient.GetAsync($"{Urls.DeleteProduct}?id={id}").Result;
        
        if (!response.IsSuccessStatusCode) throw new Exception("Failed to delete product. Response: " + response.Content);
        
        var jsonString = await response.Content.ReadAsStringAsync();
        
        return (JObject)JToken.Parse(jsonString);
    }

    public async Task<JObject> Create(Product product)
    {
        var content = ConvertProductToHttpContent(product);

        return await SendPostRequest(Urls.AddProduct, content);
    }
    
    public async Task<JObject> Edit(Product product)
    {
        /*Console.WriteLine(product.id);*/
        
        var content = ConvertProductToHttpContent(product);
        /*var biba = await content.ReadAsStringAsync();
        Console.WriteLine(biba);*/

        return await SendPostRequest(Urls.EditProduct, content);
    }

    private HttpContent ConvertProductToHttpContent(Product product)
    {
        var objProduct = JsonConvert.SerializeObject(product);
        var content = new StringContent(objProduct.ToString(), Encoding.UTF8, "application/json");

        return content;
    }

    private async Task<JObject> SendPostRequest(string url, HttpContent content)
    {
        var response = await _httpClient.PostAsync(url, content);
        
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(jsonString);
            return (JObject)JToken.Parse(jsonString);
        }
        else
        {
            throw new Exception("Failed to create product. Response: " + response.Content);
        }
    }
}