using AlgoLibrary;

namespace TestProject
{
    public class UnitTestHashing
    {
        [Fact]
        public void EncrtyptDecrypt_MixedCharactersString_ShouldBeEqual()
        {
            // Arrange
            string password = "1234adsa!";
            // Act
            string hash = Hashing.EncryptPassword(password);
            string unhash = Hashing.DecryptPassword(hash);
            // Assert
            Assert.Equal(unhash, password);
        }

        [Fact]
        public void EncrtyptDecrypt_NumericString_ShouldBeEqual()
        {
            // Arrange
            string password = "1234";
            // Act
            string hash = Hashing.EncryptPassword(password);
            string unhash = Hashing.DecryptPassword(hash);
            // Assert
            Assert.Equal(unhash, password);
        }
        [Fact]
        public void EncrtyptDecrypt_AlphaString_ShouldBeEqual()
        {
            // Arrange
            string password = "abcde";
            // Act
            string hash = Hashing.EncryptPassword(password);
            string unhash = Hashing.DecryptPassword(hash);
            // Assert
            Assert.Equal(unhash, password);
        }

        [Fact]
        public void EncrtyptDecrypt_EmptyString_ShouldBeEqual()
        {
            // Arrange
            string password = "";
            // Act
            string hash = Hashing.EncryptPassword(password);
            string unhash = Hashing.DecryptPassword(hash);
            // Assert
            Assert.Equal(unhash, password);
        }
    }
}