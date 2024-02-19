using Minio;

namespace amorphie.contract.data.Services;

public class MinioStream : Stream, IDisposable
{
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName;
    private readonly string _objectName;
    private readonly Stream _stream;
    private long _position;

    public MinioStream(IMinioClient minioClient, string bucketName, string objectName)
    {
        _minioClient = minioClient;
        _bucketName = bucketName;
        _objectName = objectName;
        _stream = new MemoryStream();
        _position = 0;
    }

    public override bool CanRead => true;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override long Length => _stream.Length;
    public override long Position
    {
        get => _position;
        set => throw new NotSupportedException();
    }

    public override void Flush() { }

    public override int Read(byte[] buffer, int offset, int count)
    {
        int bytesRead = _stream.Read(buffer, offset, count);
        _position += bytesRead;
        return bytesRead;
    }

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
    public override void SetLength(long value) => throw new NotSupportedException();
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _stream.Dispose();
        }
        base.Dispose(disposing);
    }
}
