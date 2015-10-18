using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Assemblify.Core
{
    public static class Debug
    {
        private static List<DebugLogMessage> logMessages;
        public static List<DebugLogMessage> LogMessages
        {
            get { return logMessages; }
        }

        static Debug()
        {
            logMessages = new List<DebugLogMessage>();
        }

        public static void Log(string message)
        {
            //#region Stack Trace
            //var stackTrace = new StackTrace(true);

            //var stackFrameInfo = "";
            //for (int i = 0; i < stackTrace.FrameCount; i++)
            //{
            //    var frame = stackTrace.GetFrame(i);
            //    stackFrameInfo += frame.GetFileLineNumber() + ": " +
            //    frame.GetMethod() + "\n\n";
            //}
            //#endregion

            var logMessage = new DebugLogMessage(message, /*stackFrameInfo*/"", DateTime.Now);
            logMessages.Add(logMessage);
        }
        public static void LogReturn()
        {
            logMessages.Add(new DebugLogMessage("", "", DateTime.MinValue));
        }

        public static void StoreLog(string filePath, bool debugInfo)
        {
            var lines = new List<string>(logMessages.ConvertAll(l => l.ToString()));

            //var finalLines = new List<string>(lines.Count * 2);
            //foreach (var line in lines)
            //{
            //    if (line.Contains("\n"))
            //    {
            //        var splitted = line.Split('\n');
            //        foreach (var splittedN in splitted)
            //        {
            //            finalLines.Add(splittedN);
            //        }
            //    }
            //}

            lines.Add("");
            lines.Add("---");
            lines.Add("");

            if (!File.Exists(filePath))
                File.WriteAllLines(filePath, lines);
            else
                File.AppendAllLines(filePath, lines);
        }
    }

    public struct DebugLogMessage
    {
        public readonly string message, stackFrame;
        public readonly DateTime timestamp;

        public DebugLogMessage(string message, string stackFrame, DateTime timestamp)
        {
            this.message = message;
            this.stackFrame = stackFrame;
            this.timestamp = timestamp;
        }

        public override string ToString()
        {
            return message != "" ? (timestamp.ToString() + ": " + message) : "";// +
                //(debugInfo ? "\n\n" + stackFrame : "")) : "");
        }
    }
}
