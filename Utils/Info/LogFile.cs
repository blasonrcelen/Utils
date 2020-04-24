using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utils.Info
{
    public class LogFile
    {
        public readonly String FilePath;

        public LogFile(String _filePath, String _initMessage = null, bool _showDateTime = false)
        {
            FilePath = _filePath;
            if (!File.Exists(FilePath)) new FileStream(FilePath, FileMode.Create).Close();
            if (_initMessage != null) WriteLine(_initMessage, _showDateTime);
        }

        public void WriteError(String _error, bool _showDateTime = false)
        {
            WriteLine($"[Error]: {_error}", _showDateTime);
        }

        public void WriteErrors(List<String> _errors, bool _showDateTime = false)
        {
            List<String> errors = new List<string>();
            errors.Add("====== ERRORS ======");
            errors.AddRange(_errors);
            errors.Add("====================");
            WriteLines(errors, _showDateTime);
        }

        public void WriteWarning(String _warning, bool _showDateTime = false)
        {
            WriteLine($"[Warning]: {_warning}", _showDateTime);
        }

        public void WriteWarnings(List<String> _warnings, bool _showDateTime = false)
        {
            List<String> warnings = new List<string>();
            warnings.Add("====== ERRORS ======");
            warnings.AddRange(_warnings);
            warnings.Add("====================");
            WriteLines(warnings, _showDateTime);
        }

        public void WriteLine(String _line, bool _showDateTime = false)
        {
            if (_line == null) return;

            byte[] bytesToWrite = Encoding.ASCII.GetBytes((_showDateTime ? $"[{DateTime.Now}]: {_line}" : _line) + "\n");

            FileStream file = new FileStream(FilePath, FileMode.Append);
            file.Write(bytesToWrite, 0, bytesToWrite.Length);
            file.Close();
        }

        public void WriteLines(List<String> _lines, bool _showDateTime = false)
        {
            if (_lines == null) return;

            FileStream file = new FileStream(FilePath, FileMode.Append);

            if (_showDateTime)
            {
                byte[] dateTime = Encoding.ASCII.GetBytes($"[{DateTime.Now}]:\n");
                file.Write(dateTime, 0, dateTime.Length);
            }

            foreach (String line in _lines)
            {
                if (line == null) continue;

                byte[] bytesToWrite = Encoding.ASCII.GetBytes($"{line}\n");
                file.Write(bytesToWrite, 0, bytesToWrite.Length);
            }

            file.Close();
        }
    }
}
