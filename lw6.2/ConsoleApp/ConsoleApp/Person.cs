using System.Text.RegularExpressions;

namespace ConsoleApp;

public class Person
{
    private const int PersonMaxAge = 150;
    private const int ComingOfAge = 18;
    private string _name;
    private int _age;

    public Person(string name, int age)
    {
        SetName(name);
        SetAge(age);
    }

    public int GetPersonMaxAge()
    {
        return PersonMaxAge;
    }
    
    public int GetComingOfAge()
    {
        return ComingOfAge;
    }

    public string GetName()
    {
        return _name;
    }

    public int GetAge()
    {
        return _age;
    }

    public void SetName(string newName)
    {
        string namePattern = "^[a-zA-Z]+$";

        if (!Regex.IsMatch(newName, namePattern))
        {
            throw new Exception("The name can contain only letters of the Latin alphabet");
        }
        
        _name = newName;
    }

    public void SetAge(int newAge)
    {
        if (newAge < 0)
        {
            throw new Exception("The age cannot be less than 0");
        }

        if (newAge > PersonMaxAge)
        {
            throw new Exception($"The age cannot be more than {PersonMaxAge}");
        }
        
        _age = newAge;
    }

    public bool IsAdult()
    {
        return GetAge() >= ComingOfAge;
    }

    public void IncrementAge()
    {
        SetAge(GetAge() + 1);
    }
}