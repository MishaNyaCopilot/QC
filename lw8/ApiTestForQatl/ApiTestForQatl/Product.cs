using Newtonsoft.Json.Linq;

namespace ApiTestForQatl;

public class Product
{
    public Product(
        string categoryId, 
        string titleI, 
        string aliasI, 
        string contentI, 
        string priceI, 
        string oldPriceI, 
        string statusI, 
        string keywordsI, 
        string descriptionI, 
        string hitI)
    {
        category_id = categoryId;
        title = titleI;
        alias = aliasI;
        content = contentI;
        price = priceI;
        old_price = oldPriceI;
        status = statusI;
        keywords = keywordsI;
        description = descriptionI;
        hit = hitI;
    }
    
    public Product()
    {
        category_id = "-1";
        title = "";
        alias = "";
        content = "";
        price = "-1";
        old_price = "-1";
        status = "0";
        keywords = "";
        description = "";
        hit = "0";
    }

    public string id { get; set; }
    public string category_id { get; set; }
    public string title { get; set; }
    public string alias { get; set; }
    public string content { get; set; }
    public string price { get; set; }
    public string old_price { get; set; }
    public string status { get; set; }
    public string keywords { get; set; }
    public string description { get; set; }
    public string hit { get; set; }

    public static JToken? GetById(int id, JArray products)
    {
        foreach (var product in products)
        {
            if (product["id"]!.ToObject<int>() == id) return product;
        }

        return null;
    }
}