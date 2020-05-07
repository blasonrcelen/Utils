using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utils.Info
{
    public class LogFile
    {
        public readonly String FilePath;

        public LogFile(String filePath, String initMessage = null, bool showDateTime = false)
        {
            FilePath = filePath;
            if (!File.Exists(FilePath)) new FileStream(FilePath, FileMode.Create).Close();
            if (initMessage != null) WriteLine(initMessage, showDateTime);
        }

        public void WriteError(String error, bool showDateTime = false)
        {
            WriteLine($"[Error]: {error}", showDateTime);
        }

        public void WriteErrors(List<String> errors, bool showDateTime = false)
        {
            List<String> errors = new List<string>();
            errors.Add("====== ERRORS ======");
            errors.AddRange(errors);
            errors.Add("====================");
            WriteLines(errors, showDateTime);
        }

        public void WriteWarning(String warning, bool showDateTime = false)
        {
            WriteLine($"[Warning]: {warning}", showDateTime);
        }

        public void WriteWarnings(List<String> warnings, bool showDateTime = false)
        {
            List<String> warnings = new List<string>();
            warnings.Add("====== Warnings ======");
            warnings.AddRange(warnings);
            warnings.Add("====================");
            WriteLines(warnings, showDateTime);
        }

        public void WriteLine(String line, bool showDateTime = false)
        {
            if (line == null) return;

            byte[] bytesToWrite = Encoding.ASCII.GetBytes((showDateTime ? $"[{DateTime.Now}]: {line}" : line) + "\n");

            FileStream file = new FileStream(FilePath, FileMode.Append);
            file.Write(bytesToWrite, 0, bytesToWrite.Length);
            file.Close();
        }

        public void WriteLines(List<String> lines, bool showDateTime = false)
        {
            if (lines == null) return;

            FileStream file = new FileStream(FilePath, FileMode.Append);

            if (showDateTime)
            {
                byte[] dateTime = Encoding.ASCII.GetBytes($"[{DateTime.Now}]:\n");
                file.Write(dateTime, 0, dateTime.Length);
            }

            foreach (String line in lines)
            {
                if (line == null) continue;

                byte[] bytesToWrite = Encoding.ASCII.GetBytes($"{line}\n");
                file.Write(bytesToWrite, 0, bytesToWrite.Length);
            }

            file.Close();
        }
    }
}
