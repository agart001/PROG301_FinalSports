using SportsLibrary.Interfaces;
using SportsLibrary.Models;

namespace SportsTests
{
    [TestClass]
    public class EntityTests
    {
        private IEntity entity {  get; set; }

        [TestMethod]
        [ExpectedException(typeof(MissingMethodException), "Cannot create an abstract class.")]
        public void BaseEntity_AbstractThrow()
        {
        }

        [TestMethod]
        public void Entity_NameTest()
        {

        }
    }
}