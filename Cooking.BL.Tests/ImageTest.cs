using Cooking.BL.Exceptions;
using Cooking.BL.Models;

namespace Cooking.BL.Tests
{
    public class ImageTest
    {
        public class ImageTests
        {
            [Fact]
            public void TwoParameterConstructor_WithValidParameters_InitializesProperties()
            {
                // Arrange
                var imageId = 1;
                var imageUrl = "http://example.com/image.jpg";

                // Act
                var image = new Image(imageId, imageUrl);

                // Assert
                Assert.Equal(imageId, image.ImageId);
                Assert.Equal(imageUrl, image.ImageUrl);
            }
        }

        [Fact]
        public void OneParameterConstructor_WithValidUrl_InitializesImageUrl()
        {
            // Arrange
            var imageUrl = "http://example.com/image.jpg";

            // Act
            var image = new Image(imageUrl);

            // Assert
            Assert.Equal(imageUrl, image.ImageUrl);
        }

        [Fact]
        public void ImageId_SetValidValue_AssignsValue()
        {
            // Arrange
            var image = new Image("http://example.com/image.jpg");

            // Act
            image.ImageId = 10;

            // Assert
            Assert.Equal(10, image.ImageId);
        }

        [Fact]
        public void ImageId_SetInvalidValue_ThrowsImageException()
        {
            // Arrange
            var image = new Image("http://example.com/image.jpg");

            // Act & Assert
            var ex = Assert.Throws<ImageException>(() => image.ImageId = 0);
            Assert.Equal("ImageId can't be smaller then 1!", ex.Message);
        }

        [Fact]
        public void ImageUrl_SetValidValue_AssignsValue()
        {
            // Arrange
            var image = new Image(1, "http://example.com/initial.jpg");

            // Act
            image.ImageUrl = "http://example.com/new.jpg";

            // Assert
            Assert.Equal("http://example.com/new.jpg", image.ImageUrl);
        }



    }
}
