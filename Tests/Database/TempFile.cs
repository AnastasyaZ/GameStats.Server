using System;
using System.IO;
using System.Text;

namespace Tests.Database
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

    //public IDiskService Disk(bool journal = true)
    //{
    //    return new FileDiskService(Filename, journal);
    //}

    //public IDiskService Disk(FileOptions options)
    //{
    //    return new FileDiskService(Filename, options);
    //}

    //public string Conn(string connectionString)
    //{
    //    return "filename=\"" + this.Filename + "\";" + connectionString;
    //}

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

    public string ReadAsText()
    {
      return File.ReadAllText(Filename);
    }

    #region LoremIpsum Generator

    public static string LoremIpsum(int minWords, int maxWords,
        int minSentences, int maxSentences,
        int numParagraphs)
    {
      var words = new[] { "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat" };

      var rand = new Random(DateTime.Now.Millisecond);
      var numSentences = rand.Next(maxSentences - minSentences) + minSentences + 1;
      var numWords = rand.Next(maxWords - minWords) + minWords + 1;

      var result = new StringBuilder();

      for (var p = 0; p < numParagraphs; p++)
      {
        for (var s = 0; s < numSentences; s++)
        {
          for (var w = 0; w < numWords; w++)
          {
            if (w > 0) { result.Append(" "); }
            result.Append(words[rand.Next(words.Length)]);
          }
          result.Append(". ");
        }
        result.AppendLine();
      }

      return result.ToString();
    }

    #endregion
  }
}