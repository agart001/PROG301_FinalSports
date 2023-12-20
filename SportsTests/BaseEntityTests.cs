using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsLibrary.Models;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


public class TestEntity : BaseEntity
{
    public TestEntity() : base() { }

    public TestEntity(string? name) : base(name) { }

    public TestEntity(string? name, string? description) : base(name, description) { }

    public TestEntity(string? name, string? description, Guid id) : base(name, description, id) { }
}

[TestClass]
public class BaseEntityTests
{
    [TestMethod]
    public void DefaultConstructor_InitializedWithNewGuidAndEmptyName()
    {
        // Arrange
        var entity = new TestEntity();

        // Act & Assert
        Assert.IsNotNull(entity.ID);
        Assert.AreEqual(string.Empty, entity.Name);
        Assert.IsNull(entity.Description);
    }

    [TestMethod]
    public void ConstructorWithName_InitializedWithNewGuidAndProvidedName()
    {
        // Arrange
        string testName = "TestEntity";
        var entity = new TestEntity(testName);

        // Act & Assert
        Assert.IsNotNull(entity.ID);
        Assert.AreEqual(testName, entity.Name);
        Assert.IsNull(entity.Description);
    }

    [TestMethod]
    public void ConstructorWithNameAndDescription_InitializedWithNewGuidAndProvidedNameAndDescription()
    {
        // Arrange
        string testName = "TestEntity";
        string testDescription = "This is a test entity.";
        var entity = new TestEntity(testName, testDescription);

        // Act & Assert
        Assert.IsNotNull(entity.ID);
        Assert.AreEqual(testName, entity.Name);
        Assert.AreEqual(testDescription, entity.Description);
    }

    [TestMethod]
    public void ConstructorWithAllParameters_InitializedWithProvidedIdNameAndDescription()
    {
        // Arrange
        Guid testId = Guid.NewGuid();
        string testName = "TestEntity";
        string testDescription = "This is a test entity.";
        var entity = new TestEntity(testName, testDescription, testId);

        // Act & Assert
        Assert.AreEqual(testId, entity.ID);
        Assert.AreEqual(testName, entity.Name);
        Assert.AreEqual(testDescription, entity.Description);
    }

    [TestMethod]
    public void SetName_SetsEntityName()
    {
        // Arrange
        var entity = new TestEntity();

        // Act
        entity.SetName("NewName");

        // Assert
        Assert.AreEqual("NewName", entity.Name);
    }

    [TestMethod]
    public void SetDescription_SetsEntityDescription()
    {
        // Arrange
        var entity = new TestEntity();

        // Act
        entity.SetDescription("NewDescription");

        // Assert
        Assert.AreEqual("NewDescription", entity.Description);
    }

    [TestMethod]
    public void About_ReturnsDescription()
    {
        // Arrange
        string testDescription = "This is a test entity.";
        var entity = new TestEntity { Description = testDescription };

        // Act
        string result = entity.About();

        // Assert
        Assert.AreEqual(testDescription, result);
    }

    [TestMethod]
    public void GetObjectData_ThrowsNotImplementedException()
    {
        // Arrange
        var entity = new TestEntity();

        // Act & Assert
        Assert.ThrowsException<NotImplementedException>(() => entity.GetObjectData(null!, new StreamingContext()));
    }
}
