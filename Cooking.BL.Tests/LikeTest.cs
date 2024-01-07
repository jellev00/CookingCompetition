using Cooking.BL.Exceptions;
using Cooking.BL.Models;

namespace Cooking.BL.Tests
{
    public class LikeTest
    {
        [Fact]
        public void Constructor_WithValidParameters_InitializesProperties()
        {
            // Arrange
            var likeId = 1;
            var likeDate = DateTime.Now;

            // Act
            var like = new Like(likeId, likeDate);

            // Assert
            Assert.Equal(likeId, like.LikeId);
            Assert.True((like.LikeDate - likeDate).TotalMilliseconds < 1000); // Allowing a margin of 1 second
        }

        [Fact]
        public void OverloadedConstructor_WithValidParameters_InitializesProperties()
        {
            // Arrange
            var likeDate = DateTime.Now;

            // Act
            var like = new Like(likeDate);

            // Assert
            Assert.True((like.LikeDate - likeDate).TotalMilliseconds < 1000); // Allowing a margin of 1 second
        }

        [Fact]
        public void LikeId_SetValidValue_AssignsValue()
        {
            // Arrange
            var like = new Like(DateTime.Now);

            // Act
            like.LikeId = 10;

            // Assert
            Assert.Equal(10, like.LikeId);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void LikeId_SetInvalidValue_ThrowsLikeException(int invalidLikeId)
        {
            // Arrange
            var like = new Like(DateTime.Now);

            // Act & Assert
            var ex = Assert.Throws<LikeException>(() => like.LikeId = invalidLikeId);
            Assert.Equal("LikeId can't be less then 1", ex.Message);
        }

        //[Fact]
        //public void LikeDate_IsAutomaticallySetToNowOnCreation()
        //{
        //    // Arrange & Act
        //    var like = new Like(1);

        //    // Assert
        //    Assert.Equal(DateTime.Now, like.LikeDate, TimeSpan.FromSeconds(1)); // Allowing a small margin for time difference
        //}


        //[Fact]
        //public void LikeDate_SetValidValue_AssignsValue()
        //{
        //    // Arrange
        //    var like = new Like(DateTime.Now);

        //    // Act
        //    like.LikeDate = DateTime.Now.AddDays(1);

        //    // Assert
        //    Assert.Equal(DateTime.Now.AddDays(1), like.LikeDate);
        //}
    }
}
