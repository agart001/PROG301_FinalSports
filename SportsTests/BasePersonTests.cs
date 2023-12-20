using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsLibrary.Models;
using System;

[TestClass]
public class BasePersonTests
{
    [TestMethod]
    public void DefaultConstructor_InitializedWithDefaultValues()
    {
        // Arrange
        var person = new BasePerson();

        // Act & Assert
        Assert.AreEqual(Guid.Empty, person.ID);
        Assert.IsNull(person.FirstName);
        Assert.IsNull(person.LastName);
        Assert.IsNull(person.Name);
        Assert.IsNull(person.Age);
        Assert.IsNull(person.Position);
    }

    [TestMethod]
    public void ConstructorWithName_InitializedWithProvidedNameAndDefaultValues()
    {
        // Arrange
        string testName = "John Doe";
        var person = new BasePerson(testName);

        // Act & Assert
        Assert.AreNotEqual(Guid.Empty, person.ID);
        Assert.AreEqual(testName, person.Name);
        Assert.IsNull(person.FirstName);
        Assert.IsNull(person.LastName);
        Assert.AreEqual(0, person.Age);
        Assert.AreEqual(string.Empty, person.Position);
    }

    [TestMethod]
    public void ConstructorWithFirstAndLastName_InitializedWithProvidedNamesAndGeneratedFullName()
    {
        // Arrange
        string firstName = "John";
        string lastName = "Doe";
        var person = new BasePerson(firstName, lastName);

        // Act & Assert
        Assert.AreNotEqual(Guid.Empty, person.ID);
        Assert.AreEqual($"{firstName} {lastName}", person.Name);
        Assert.AreEqual(firstName, person.FirstName);
        Assert.AreEqual(lastName, person.LastName);
        Assert.AreEqual(0, person.Age);
        Assert.AreEqual(string.Empty, person.Position);
    }

    [TestMethod]
    public void ConstructorWithAllParameters_InitializedWithProvidedParametersAndGeneratedFullName()
    {
        // Arrange
        string firstName = "John";
        string lastName = "Doe";
        int age = 30;
        string position = "Developer";
        var person = new BasePerson(firstName, lastName, age, position);

        // Act & Assert
        Assert.AreNotEqual(Guid.Empty, person.ID);
        Assert.AreEqual($"{firstName} {lastName}", person.Name);
        Assert.AreEqual(firstName, person.FirstName);
        Assert.AreEqual(lastName, person.LastName);
        Assert.AreEqual(age, person.Age);
        Assert.AreEqual(position, person.Position);
    }

    [TestMethod]
    public void SetFirstName_SetsFirstName()
    {
        // Arrange
        var person = new BasePerson();

        // Act
        person.SetFirstName("John");

        // Assert
        Assert.AreEqual("John", person.FirstName);
    }

    [TestMethod]
    public void SetLastName_SetsLastName()
    {
        // Arrange
        var person = new BasePerson();

        // Act
        person.SetLastName("Doe");

        // Assert
        Assert.AreEqual("Doe", person.LastName);
    }

    [TestMethod]
    public void SetAge_SetsAge()
    {
        // Arrange
        var person = new BasePerson();

        // Act
        person.SetAge(25);

        // Assert
        Assert.AreEqual(25, person.Age);
    }

    [TestMethod]
    public void SetPosition_SetsPosition()
    {
        // Arrange
        var person = new BasePerson();

        // Act
        person.SetPosition("Manager");

        // Assert
        Assert.AreEqual("Manager", person.Position);
    }
}
