using ApiTestForQatl;

try
{
    Controller controller = new Controller();

    var data = (await controller.GetAll()).Last;
    
    Console.WriteLine(data);

    var product = new Product(
        "1",
        "ValidProductTitle",
        "validproducttitle",
        "ValidProductContent",
        "300",
        "301",
         "1",
        "Something",
        "Description",
        "0"
        );

    //var data2 = await controller.Create(product);
    
    //Console.WriteLine(data2);

    /*var deleteResponse = await controller.Delete(30132);
    
    Console.WriteLine(deleteResponse);*/

    var product2 = new Product
    {
        id = "900000",
        category_id = "5",
        title = "Product Title",
        alias = "product-alias",
        content = "Product Content",
        price = "100.0",
        old_price = "90.0",
        status = "1",
        keywords = "keyword1, keyword2",
        description = "Product Description",
        hit = "0"
    };
    
    var editResponse = await controller.Edit(product2);
    var last = (await controller.GetAll()).Last;
    Console.WriteLine(editResponse);
    Console.WriteLine(last);
}
catch (Exception e)
{
    Console.WriteLine("Exception handled");
    Console.WriteLine(e.Message);
}