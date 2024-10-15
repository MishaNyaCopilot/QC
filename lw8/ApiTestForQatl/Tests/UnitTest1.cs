using ApiTestForQatl;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Tests;

public static class ProductAssertion
{
    public static void AreProductsEqual(JToken first, JToken second)
    {
        Assert.AreEqual(first["category_id"], second["category_id"], "Category IDs are not equal");
        Assert.AreEqual(first["title"], second["title"], "Titles are not equal");
        Assert.AreEqual(first["content"], second["content"], "Contents are not equal");
        Assert.AreEqual(first["price"], second["price"], "Prices are not equal");
        Assert.AreEqual(first["old_price"], second["old_price"], "Old Prices are not equal");
        Assert.AreEqual(first["status"], second["status"], "Statuses are not equal");
        Assert.AreEqual(first["keywords"], second["keywords"], "Keywords are not equal");
        Assert.AreEqual(first["description"], second["description"], "Descriptions are not equal");
        Assert.AreEqual(first["hit"], second["hit"], "Hits are not equal");
    }
}

[TestClass]
public class ApiTests
{
    private static readonly HttpClient _httpClient = new();
    private static readonly Controller _controller = new Controller();

    private static readonly List<int> _testProductsIds = new();

    private static readonly string _productSchemaJson = File.ReadAllText(@"..\..\..\Json\Product.json");
    private static readonly string _productsListSchemaJson =
        File.ReadAllText(@"..\..\..\Json\ProductList.json");
    
    private static readonly JObject _addProductTestsJson =
        JObject.Parse(File.ReadAllText(@"..\..\..\Json\TestData\AddProductTest.json"));
    
    private static readonly JObject _editProductTestsJson =
        JObject.Parse(File.ReadAllText(@"..\..\..\Json\TestData\EditProductTest.json"));

    private static bool IsJsonValid(string schemaString, JToken json)
    {
        JsonSchema schema = JsonSchema.Parse(schemaString);

        if (!json.IsValid(schema))
        {
            return false;
        }

        return true;
    }

    
    [TestCleanup]
    public async Task TestCleanup()
    {
        foreach (var id in _testProductsIds)
        {
            await _controller.Delete(id);
        }
    }
    
    [TestMethod]
    public async Task Products_Are_Correspond_To_Json_Schema()
    {
        var products = await _controller.GetAll();

        Assert.IsTrue(products.Count > 0, "Array of products is empty");
        Assert.IsTrue(IsJsonValid(_productsListSchemaJson, products), "Products not valid");
    }
    
    [TestMethod]
    public async Task Create_Valid_Product()
    {
        var expectedStatus = 1;

        var product = _addProductTestsJson["valid"];

        var response = await _controller.Create(product!.ToObject<Product>()!);

        var productId = response["id"]!.ToObject<int>();
        _testProductsIds.Add(productId);

        var actualProduct = Product.GetById(productId, await _controller.GetAll());

        Assert.AreEqual(expectedStatus, response["status"], "Invalid status");
        Assert.IsNotNull(actualProduct, "Product not found");
        Assert.IsTrue(IsJsonValid(_productSchemaJson, actualProduct), "Product not valid");
        ProductAssertion.AreProductsEqual(product, actualProduct);
    }
    
    [DataRow("invalidCategoryIdIsLess")]
    [DataRow("invalidCategoryIdIsMore")]
    [TestMethod]
    public async Task Create_Product_With_Invalid_Category_Id(string dataRow)
    {
        var expectedStatus = 1;

        var response = await _controller.Create(_addProductTestsJson[dataRow]!.ToObject<Product>()!);

        var productId = response["id"]!.ToObject<int>();
        _testProductsIds.Add(productId);
        
        Assert.AreEqual(expectedStatus, response["status"], "Invalid status");
        Assert.IsNull(Product.GetById(productId, await _controller.GetAll()), "Product with unexpected category id was added");
    }
    
    [DataRow("validCategoryIdIsFifteen")]
    [DataRow("validCategoryIdIsOne")]
    [TestMethod]
    public async Task Create_Product_With_valid_Category_Id(string dataRow)
    {
        var expectedStatus = 1;

        var product = _addProductTestsJson[dataRow]!;
        var response = await _controller.Create(product.ToObject<Product>()!);

        var productId = response["id"]!.ToObject<int>();
        //Console.WriteLine(product);
        //Console.WriteLine(productId);
        _testProductsIds.Add(productId);
        
        var actualProduct = Product.GetById(productId, await _controller.GetAll());
        //Console.WriteLine(actualProduct);
        Assert.AreEqual(expectedStatus, response["status"], "Invalid status");
        Assert.IsNotNull(actualProduct, "Product not found");
        Assert.IsTrue(IsJsonValid(_productSchemaJson, actualProduct), "Product not valid");
        ProductAssertion.AreProductsEqual(product, actualProduct);
    }
    
    [TestMethod]
    public async Task Edit_Existing_Product()
    {
        var expectedStatus = 1;

        var product = _addProductTestsJson["validProductForEditing"]!;
        var response = await _controller.Create(product.ToObject<Product>()!);
        
        var productId = response["id"]!.ToObject<int>();
        _testProductsIds.Add(productId);
        
        var editProduct = _editProductTestsJson["valid"]!;
        editProduct["id"] = productId;

        await _controller.Edit(editProduct.ToObject<Product>()!);

        var actualProduct = Product.GetById(productId, await _controller.GetAll());

        Assert.AreEqual(expectedStatus, response["status"]!, "Invalid status");
        Assert.IsNotNull(actualProduct, "Product not found");
        Assert.IsTrue(IsJsonValid(_productSchemaJson, actualProduct), "Product not valid");
        ProductAssertion.AreProductsEqual(editProduct, actualProduct);
    }
    
    [TestMethod]
    public async Task Edit_Not_Existing_Product()
    {
        var expectedStatus = 0;
        var expectedProduct = _editProductTestsJson["withNotExistingId"]!;

        var response = await _controller.Edit(expectedProduct.ToObject<Product>()!);
        Assert.AreEqual(expectedStatus, response["status"]!, 
            "Сhanging a non-existent product should return the status zero");
    }
    
    [TestMethod]
    public async Task Edit_Product_Without_Id()
    {
        var expectedStatus = 0;

        var response = await _controller.Edit(_editProductTestsJson["withoutId"]!.ToObject<Product>()!);

        Assert.AreEqual(expectedStatus, response["status"]!,
            "Сhanging an product without id should return the status zero");
    }
    
    [TestMethod]
    public async Task Delete_Existing_Product()
    {
        var expectedStatus = 1;

        var creationResponse = await _controller.Create(_addProductTestsJson["validProductForEditing"]!.ToObject<Product>()!);

        var productId = creationResponse["id"]!.ToObject<int>();

        var deletionResponse = await _controller.Delete(productId);

        var actualProduct = Product.GetById(productId, await _controller.GetAll());
        
        Assert.AreEqual(expectedStatus, deletionResponse["status"]!, "Invalid status");
        Assert.IsNull(actualProduct, "After removing a product with such an ID, it should not exist");
    }
    
    [TestMethod]
    public async Task Delete_Non_Existing_Product()
    {
        var expectedStatus = 0;
        var nonExistingId = -1;

        var deletionResponse = await _controller.Delete(nonExistingId);
        
        Assert.AreEqual(expectedStatus, deletionResponse["status"]!, "Deliting a non-existent product should return the status zero");
    }
}