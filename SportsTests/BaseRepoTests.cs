using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsTests
{
    [TestClass]
    public class BaseRepoTests
    {
        [TestMethod]
        public void DefaultConstructor_InitializedWithEmptyContents()
        {
            // Arrange
            var repo = new BaseRepo<string, int>();

            // Act & Assert
            Assert.IsNotNull(repo.Contents);
            Assert.AreEqual(0, repo.Contents.Count);
        }

        [TestMethod]
        public void ConstructorWithName_InitializedWithProvidedNameAndEmptyContents()
        {
            // Arrange
            string testName = "TestRepo";
            var repo = new BaseRepo<string, int>(testName);

            // Act & Assert
            Assert.IsNotNull(repo.Contents);
            Assert.AreEqual(0, repo.Contents.Count);
            Assert.AreEqual(testName, repo.Name);
        }

        [TestMethod]
        public void ConstructorWithNameAndDescription_InitializedWithProvidedNameDescriptionAndEmptyContents()
        {
            // Arrange
            string testName = "TestRepo";
            string testDescription = "This is a test repository.";
            var repo = new BaseRepo<string, int>(testName, testDescription);

            // Act & Assert
            Assert.IsNotNull(repo.Contents);
            Assert.AreEqual(0, repo.Contents.Count);
            Assert.AreEqual(testName, repo.Name);
            Assert.AreEqual(testDescription, repo.Description);
        }

        [TestMethod]
        public void ConstructorWithNameAndContents_InitializedWithProvidedNameAndContents()
        {
            // Arrange
            string testName = "TestRepo";
            var initialContents = new List<KeyValuePair<string, ICollection<int>>>
        {
            new KeyValuePair<string, ICollection<int>>("Key1", new List<int> { 1, 2, 3 }),
            new KeyValuePair<string, ICollection<int>>("Key2", new List<int> { 4, 5, 6 }),
        };

            var repo = new BaseRepo<string, int>(testName, initialContents);

            // Act & Assert
            Assert.IsNotNull(repo.Contents);
            Assert.AreEqual(2, repo.Contents.Count);
            Assert.AreEqual(testName, repo.Name);
            CollectionAssert.AreEquivalent(initialContents, repo.Contents.ToList());
        }

        [TestMethod]
        public void ConstructorWithNameAndKeyValueArrays_InitializedWithProvidedNameAndKeyValueArrays()
        {
            // Arrange
            string testName = "TestRepo";
            var keys = new[] { "Key1", "Key2" };
            var values = new List<int>[] { new List<int> { 1, 2, 3 }, new List<int> { 4, 5, 6 } };

            var repo = new BaseRepo<string, int>(testName, keys, values);

            // Act & Assert
            Assert.IsNotNull(repo.Contents);
            Assert.AreEqual(2, repo.Contents.Count);
            Assert.AreEqual(testName, repo.Name);
            CollectionAssert.AreEquivalent(keys, repo.GetKeys().ToList());
            CollectionAssert.AreEquivalent(values, repo.GetValues().ToList());
        }

        [TestMethod]
        public void Add_AddsKeyValuePairToContents()
        {
            // Arrange
            var repo = new BaseRepo<string, int>();
            string key = "Key";
            var values = new List<int> { 1, 2, 3 };

            // Act
            repo.Add(key, values);

            // Assert
            Assert.AreEqual(1, repo.Contents.Count);
            Assert.AreEqual(key, repo.Contents.First().Key);
            CollectionAssert.AreEquivalent(values, repo.Contents.First().Value.ToList());
        }

        [TestMethod]
        public void AddKey_AddsKeyWithEmptyValuesToContents()
        {
            // Arrange
            var repo = new BaseRepo<string, int>();
            string key = "Key";

            // Act
            repo.AddKey(key);

            // Assert
            Assert.AreEqual(1, repo.Contents.Count);
            Assert.AreEqual(key, repo.Contents.First().Key);
            Assert.AreEqual(0, repo.Contents.First().Value.Count);
        }

        [TestMethod]
        public void ValueAdd_AddsValueToExistingCollection()
        {
            // Arrange
            var repo = new BaseRepo<string, int>();
            string key = "Key";
            var values = new List<int> { 1, 2, 3 };
            repo.Add(key, values);

            // Act
            repo.ValueAdd(key, 4);

            // Assert
            Assert.AreEqual(1, repo.Contents.Count);
            CollectionAssert.Contains(repo.Contents.First().Value.ToList(), 4);
        }

        [TestMethod]
        public void Remove_RemovesKeyValuePairFromContents()
        {
            // Arrange
            var repo = new BaseRepo<string, int>();
            string key = "Key";
            var values = new List<int> { 1, 2, 3 };
            repo.Add(key, values);

            // Act
            repo.Remove(key);

            // Assert
            Assert.AreEqual(0, repo.Contents.Count);
        }

        [TestMethod]
        public void ValueRemove_RemovesValueFromExistingCollection()
        {
            // Arrange
            var repo = new BaseRepo<string, int>();
            string key = "Key";
            var values = new List<int> { 1, 2, 3 };
            repo.Add(key, values);

            // Act
            repo.ValueRemove(key, 2);

            // Assert
            Assert.AreEqual(1, repo.Contents.Count);
            CollectionAssert.DoesNotContain(repo.Contents.First().Value.ToList(), 2);
        }

        [TestMethod]
        public void GetKeys_ReturnsAllKeys()
        {
            // Arrange
            var repo = new BaseRepo<string, int>();
            repo.Add("Key1", new List<int> { 1, 2, 3 });
            repo.Add("Key2", new List<int> { 4, 5, 6 });

            // Act
            var keys = repo.GetKeys();

            // Assert
            CollectionAssert.AreEquivalent(new[] { "Key1", "Key2" }, keys.ToList());
        }

        [TestMethod]
        public void GetValues_ReturnsAllCollectionsOfValues()
        {
            // Arrange
            var repo = new BaseRepo<string, int>();
            repo.Add("Key1", new List<int> { 1, 2, 3 });
            repo.Add("Key2", new List<int> { 4, 5, 6 });

            // Act
            var values = repo.GetValues();

            // Assert
            CollectionAssert.AreEquivalent(new List<int>[] { new List<int> { 1, 2, 3 }, new List<int> { 4, 5, 6 } }, values.ToList());
        }

        [TestMethod]
        public void GetKey_ReturnsKeyAssociatedWithValues()
        {
            // Arrange
            var repo = new BaseRepo<string, int>();
            repo.Add("Key1", new List<int> { 1, 2, 3 });
            repo.Add("Key2", new List<int> { 4, 5, 6 });

            // Act
            var value = repo.GetValue("Key2");
            var key = repo.GetKey(value);

            // Assert
            Assert.AreEqual("Key2", key);
        }

        [TestMethod]
        public void GetValue_ReturnsValuesAssociatedWithKey()
        {
            // Arrange
            var repo = new BaseRepo<string, int>();
            repo.Add("Key1", new List<int> { 1, 2, 3 });
            repo.Add("Key2", new List<int> { 4, 5, 6 });

            // Act
            var values = repo.GetValue("Key1");

            // Assert
            CollectionAssert.AreEquivalent(new List<int> { 1, 2, 3 }, values.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void ValueAdd_ThrowsKeyNotFoundExceptionForNonexistentKey()
        {
            // Arrange
            var repo = new BaseRepo<string, int>();

            // Act & Assert
            repo.ValueAdd("NonexistentKey", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void ValueRemove_ThrowsKeyNotFoundExceptionForNonexistentKey()
        {
            // Arrange
            var repo = new BaseRepo<string, int>();

            // Act & Assert
            repo.ValueRemove("NonexistentKey", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Remove_ThrowsKeyNotFoundExceptionForNonexistentKey()
        {
            // Arrange
            var repo = new BaseRepo<string, int>();

            // Act & Assert
            repo.Remove("NonexistentKey");
        }
    }
}
