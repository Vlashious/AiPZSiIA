using System.IO;

namespace Library
{
    public class LogWriter
    {
        private string _logFile;
        private FileStream _fileStream;
        private StreamWriter _writer;

        public LogWriter(string fileName)
        {
            File.Create(fileName);
            _logFile = fileName;
        }

        public void Write(string log)
        {
            _writer.WriteLine(log);
        }

        public void Close()
        {
            Write("RPC Client disposed.");
            _writer.Close();
            _fileStream.Close();
        }

        public void Open()
        {
            _fileStream = File.Open(_logFile, FileMode.Open);
            _writer = new StreamWriter(_fileStream);
        }
    }
}