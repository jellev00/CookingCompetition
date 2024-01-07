namespace Cooking.REST.Models.Output
{
    public class ImageOutput
    {
        public ImageOutput(int imageId, string imageUrl)
        {
            ImageId = imageId;
            ImageUrl = imageUrl;
        }

        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
    }
}