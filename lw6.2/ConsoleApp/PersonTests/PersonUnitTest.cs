using ConsoleApp;

namespace PersonTests;

[TestFixture]
public class PersonUnitTest
{
    [Test]
    public void Сonstructor_with_valid_name_and_age_create_person()
    {
        //Arrange
        string name = "Kirill";
        int age = 20;
        
        //Act
        Person person = new Person(name, age);
        
        //Assert
        Assert.That(person.GetName(), Is.EqualTo(name));
        Assert.That(person.GetAge(), Is.EqualTo(age));
    }
    
    [Test]
    public void SetName_set_correctly_with_valid_name()
    {
        //Arrange
        string name = "Kirill";
        int age = 20;
        Person person = new Person(name, age);
        
        string newName = "Rafik";
        
        //Act
        person.SetName(newName);
        
        //Assert
        Assert.That(person.GetName(), Is.EqualTo(newName));
    }
    
    [Test]
    public void SetName_with_invalid_name_throws_exception()
    {
        //Arrange
        string name = "Kirill";
        int age = 20;
        Person person = new Person(name, age);
        
        string invalidName = "Kirill2";
        
        //Act
        TestDelegate act = () => person.SetName(invalidName);
        
        //Assert
        Assert.Throws<Exception>(act);
    }
    
    [Test]
    public void SetAge_set_age_correctly_when_age_is_max_age ()
    {
        //Arrange
        string name = "Kirill";
        int age = 20;
        Person person = new Person(name, age);
        
        int maxAge = person.GetPersonMaxAge();
        
        //Act
        person.SetAge(maxAge);
        
        //Assert
        Assert.That(person.GetAge(), Is.EqualTo(maxAge));
    }
    
    [Test]
    public void SetAge_throws_exception_when_age_greater_than_max_age ()
    {
        //Arrange
        string name = "Kirill";
        int age = 20;
        Person person = new Person(name, age);
        
        int ageGreaterThanMaxAge = person.GetPersonMaxAge() + 1;
        
        //Act
        TestDelegate act = () => person.SetAge(ageGreaterThanMaxAge);
        
        //Assert
        Assert.Throws<Exception>(act);
    }
    
    [Test]
    public void SetAge_set_age_correctly_when_age_zero ()
    {
        //Arrange
        string name = "Kirill";
        int age = 20;
        Person person = new Person(name, age);
        
        int ageZero = 0;
        
        //Act
        person.SetAge(ageZero);
        
        //Assert
        Assert.That(person.GetAge(), Is.EqualTo(ageZero));
    }
    
    [Test]
    public void SetAge_throws_exception_when_age_less_than_zero ()
    {
        //Arrange
        string name = "Kirill";
        int age = 20;
        Person person = new Person(name, age);
        
        int ageLessThanZero = -1;
        
        //Act
        TestDelegate act = () => person.SetAge(ageLessThanZero);
        
        //Assert
        Assert.Throws<Exception>(act);
    }
    
    [Test]
    public void IncrementAge_increases_age_by_one ()
    {
        //Arrange
        string name = "Kirill";
        int age = 20;
        Person person = new Person(name, age);
        
        //Act
        person.IncrementAge();
        
        //Assert
        Assert.That(person.GetAge(), Is.EqualTo(age + 1));
    }
    
    [Test]
    public void IsAdult_true_when_age_is_coming_age ()
    {
        //Arrange
        string name = "Kirill";
        int age = 20;
        Person person = new Person(name, age);
        person.SetAge(person.GetComingOfAge());
        
        //Act
        bool result = person.IsAdult();
        
        //Assert
        Assert.That(result, Is.EqualTo(true));
    }
    
    [Test]
    public void IsAdult_false_when_age_less_than_coming_age ()
    {
        //Arrange
        string name = "Kirill";
        int age = 20;
        Person person = new Person(name, age);
        person.SetAge(person.GetComingOfAge() - 1);
        
        //Act
        bool result = person.IsAdult();
        
        //Assert
        Assert.That(result, Is.EqualTo(false));
    }
}