using AlgoLibrary;

namespace TestProject
{
    public class UnitTestHashing
    {
        [Fact]
        public void TestEncrtyptDecrypt()
        {
            // Arrange
            string password = "1234adsa";
            // Act
            string hash = Hashing.EncryptPassword(password);
            string unhash = Hashing.DecryptPassword(hash);
            // Assert
            Assert.Equal(unhash, password);
        }
    }
}