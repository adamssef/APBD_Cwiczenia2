using LegacyApp;

namespace LegacyAppTests;

public class UserServiceTest
{
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Without_At_And_Dot()
    {
        //Arrange
        string firstName = "John";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(1980, 1, 1);
        string email = "doe";
        int clientId = 1;

        IClientRepository clientRepository = new ClientRepository();
        var UserService = new UserService(clientRepository);
        
        //Act
        bool result = UserService.AddUser(firstName, lastName, email, birthDate, clientId);

        //Assert
        Assert.Equal(false, result);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_No_First_Name_or_Last_Name_Provided()
    {
        //Arrange
        string firstName = "";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(1980, 1, 1);
        string email = "doe@halina.com";
        int clientId = 1;

        IClientRepository clientRepository = new ClientRepository();
        var UserService = new UserService(clientRepository);
        
        //Act
        bool result = UserService.AddUser(firstName, lastName, email, birthDate, clientId);

        //Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Age_Less_Than_21()
    {
        //Arrange
        string firstName = "Halina";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(2012, 1, 1);
        string email = "doe";
        int clientId = 1;
        IClientRepository clientRepository = new ClientRepository();
        var UserService = new UserService(clientRepository);
        
        //Act
        bool result = UserService.AddUser(firstName, lastName, email, birthDate, clientId);

        //Assert
        Assert.Equal(false, result);
    }
}