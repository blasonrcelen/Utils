using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utils.Info
{
    public class LogFile
    {
        public readonly string FilePath;

        public LogFile(string filePath, string initMessage = null, bool showDateTime = false)
        {
            FilePath = filePath;
            if (!File.Exists(FilePath))
            {
                FileStream file = new FileStream(FilePath, FileMode.Create);
                if (initMessage != null)
                {
                    byte[] bytesToWrite = Encoding.ASCII.GetBytes((showDateTime ? $"[{DateTime.Now}]: {initMessage}" : initMessage) + "\n");
                    file.Write(bytesToWrite, 0, bytesToWrite.Length);
                }
                file.Close();
            }
        }

        public void WriteError(string error, bool showDateTime = false) => WriteLine($"[Error]: {error}", showDateTime);

        public void WriteErrors(List<string> errors, bool showDateTime = false)
        {
            List<string> newErrors = new List<string> { "====== ERRORS ======" };
            newErrors.AddRange(errors);
            newErrors.Add("====================");
            WriteLines(newErrors, showDateTime);
        }

        public void WriteWarning(string warning, bool showDateTime = false) => WriteLine($"[Warning]: {warning}", showDateTime);

        public void WriteWarnings(List<string> warnings, bool showDateTime = false)
        {
            List<string> newWarnings = new List<string> { "====== Warnings ======" };
            newWarnings.AddRange(warnings);
            newWarnings.Add("====================");
            WriteLines(warnings, showDateTime);
        }

        public void WriteLine(string line, bool showDateTime = false)
        {
            if (line != null)
            {
                byte[] bytesToWrite = Encoding.ASCII.GetBytes((showDateTime ? $"[{DateTime.Now}]: {line}" : line) + "\n");
                FileStream file = new FileStream(FilePath, FileMode.Append);
                file.Write(bytesToWrite, 0, bytesToWrite.Length);
                file.Close();
            }
        }

        public void WriteLines(List<string> lines, bool showDateTime = false)
        {
            if (lines != null && lines.Count > 0)
            {
                FileStream file = new FileStream(FilePath, FileMode.Append);

                if (showDateTime)
                {
                    byte[] dateTime = Encoding.ASCII.GetBytes($"[{DateTime.Now}]:\n");
                    file.Write(dateTime, 0, dateTime.Length);
                }

                foreach (string line in lines)
                {
                    if (line != null)
                    {
                        byte[] bytesToWrite = Encoding.ASCII.GetBytes($"{line}\n");
                        file.Write(bytesToWrite, 0, bytesToWrite.Length);
                    }
                }
                file.Close();
            }
        }
    }
}
