namespace ImageProcessingPipeline;

public class ImageProcessor
{
    private readonly BufferPool _bufferPool;

    public ImageProcessor(BufferPool bufferPool) => _bufferPool = bufferPool;

    public void ResizeImage(Span<byte> image, int newWidth, int newHeight)
    {        
        var resizedBuffer = _bufferPool.RentBuffer();
        image[..Math.Min(image.Length, resizedBuffer.Length)].CopyTo(resizedBuffer);
    }

    public void ConvertToGrayscale(Span<byte> image)
    {        
        for (int i = 0; i < image.Length; i += 3)
        {
            byte grayscale = (byte)((image[i] + image[i + 1] + image[i + 2]) / 3);
            image[i] = image[i + 1] = image[i + 2] = grayscale;
        }
    }

    public void DetectEdges(Span<byte> image)
    {        
        for (int i = 0; i < image.Length; i++)
        {
            image[i] = (byte)(image[i] > 128 ? 255 : 0);
        }
    }
}
