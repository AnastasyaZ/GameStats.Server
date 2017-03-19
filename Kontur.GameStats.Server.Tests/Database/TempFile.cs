using System;
using System.IO;

namespace Kontur.GameStats.Server.Tests.Database
{
  public class TempFile : IDisposable
  {
    public string Filename { get; }

    public TempFile(string ext = "db")
    {
      var path = Path.Combine(Environment.CurrentDirectory, "UnitTestData");
      Directory.CreateDirectory(path);
      Filename = Path.Combine(path, $"test-{Guid.NewGuid()}.{ext}");
    }

    #region Dispose

    private bool _disposed;

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    ~TempFile()
    {
      Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (_disposed)
        return;

      if (disposing)
      {
        // free other managed objects that implement
        // IDisposable only
      }

      File.Delete(Filename);

      _disposed = true;
    }

    #endregion

    public long Size => new FileInfo(Filename).Length;
    
  }
}