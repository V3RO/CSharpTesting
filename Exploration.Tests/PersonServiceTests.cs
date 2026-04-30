using Spw4.Exploration;
using NSubstitute;
using Xunit;

namespace Exploration.Tests;

public class PersonServiceTests
{
    private readonly IPersonRepository _personRepository;
    private readonly PersonService _personService;


    public PersonServiceTests()
    {
        _personRepository = Substitute.For<IPersonRepository>();
        _personService = new PersonService(_personRepository);
    }

    [Fact]
    public void Register_WithValidNameAndAge_Succeeds()
    {
        // Arrange
        var name = "Alice";
        var age = 20;
        
        // Act
        _personService.Register(name, age);
        
        // Assert
        _personRepository
            .Received(1)
            .CreatePerson(Arg.Is<Person>(p => p.Name == name && p.Age == age));
    }

    [Fact]
    public void Register_WithInvalidAge_ThrowsArgumentException()
    {
        var name = "Alice";
        var age = 12;
        
        Assert.Throws<ArgumentException>(() => _personService.Register(name, age));
    }
    
    [Fact]
    public void GetAverageAge_ReturnsCorrectResult()
    {
        _personRepository.ReadAllPersons().Returns(new List<Person>
        {
            new("Alice", 20),
            new("Bob", 30),
            new("Hans", 40)
        });

        var actual = _personService.GetAverageAge();
        
        Assert.Equal(30, actual);
    }
}