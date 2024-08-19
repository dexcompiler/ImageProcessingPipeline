using System.Buffers;

namespace ImageProcessingPipeline;

public class BufferPool
{
    private readonly int _bufferSize;
    private readonly ArrayPool<byte> _pool;

    public BufferPool(int bufferSize)
    {
        _bufferSize = bufferSize;
        _pool = ArrayPool<byte>.Shared;
    }

    public byte[] RentBuffer()
    {
        return _pool.Rent(_bufferSize);
    }

    public void ReturnBuffer(byte[] buffer)
    {
        _pool.Return(buffer, clearArray: false);
    }
}