using static System.Console;
using ImageProcessingPipeline;

int numberOfImages = 10;
int imageSize = 1024 * 1024; // 1 MB image size

var bufferPool = new BufferPool(imageSize);
var imageProcessor = new ImageProcessor(bufferPool);

for (int i = 0; i < numberOfImages; i++)
{
    WriteLine($"Processing image {i + 1}/{numberOfImages}");

    // Rent a buffer from the pool
    var imageBuffer = bufferPool.RentBuffer();

    try
    {
        // Simulate loading the image data into the buffer
        new Random().NextBytes(imageBuffer);

        // simulate image transformations
        var imageSpan = new Span<byte>(imageBuffer, 0, imageSize);
        imageProcessor.ResizeImage(imageSpan, 800, 600);
        imageProcessor.ConvertToGrayscale(imageSpan);
        imageProcessor.DetectEdges(imageSpan);

        // Simulate saving the processed image
        WriteLine("Image processed and saved.");
    }
    finally
    {
        // Return the buffer to the pool
        bufferPool.ReturnBuffer(imageBuffer);
    }
}