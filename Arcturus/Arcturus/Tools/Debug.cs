using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Arcturus.Tools
{
    /// <summary>
    /// Class for debugging purpose.
    /// </summary>
    public class Debug
    {
        /// <summary>
        /// Write down in the log file.
        /// </summary>
        /// <param name="message">Message to write.</param>
       static public void WriteLog(string message)
       {
           string text = "[" + DateTime.Now + "]" + message;

           using (StreamWriter streamWriter = new StreamWriter("DebugLog.txt", true))
           {
               DateTime currentDay = DateTime.Now;
               streamWriter.WriteLine(text);
               streamWriter.Close();
           }
       }
    }
}
