using MockForCurrency;

var mock = new MockServer();

mock.Start();

Console.WriteLine("Press any key to stop");
Console.ReadLine();

mock.Stop();