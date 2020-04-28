using Xunit;

namespace VendorApp.Tests
{
    public class TempTest
    {
        [Fact] // Fact Attribute
        public void Pass()
        {
            Assert.Equal(4, 4);
        }

        [Fact] // Fact Attribute
        public void Fail()
        {
            Assert.Equal(4, 4);
        }
    }
} 