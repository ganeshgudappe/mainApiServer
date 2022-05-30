using System;
using System.IO;


namespace ApiServer
{
    public class WriterLog
    {
        public static void WriteErrorLog(string strMessage, string filename)
        {
            string dpath = System.IO.Directory.GetCurrentDirectory() + @"\ErrorLog.txt";

            string date = DateTime.Now.ToString();

            //TextWriter writer = new StreamWriter(path);
            //writer.Write(strMessage, filename);
            //writer.WriteLine();


            if (!Directory.Exists(dpath))
            {
                Directory.CreateDirectory(dpath);
            }
            string path = System.IO.Directory.GetCurrentDirectory() + @"\ErrorLog" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            StreamWriter log;
            if (!File.Exists(path))
            {
                log = new StreamWriter(path);
            }
            else
            {
                log = File.AppendText(path);
            }
            // Write to the file:
            //log.WriteLine();
            log.WriteLine(filename + " : " + strMessage + " : " + date);
            //Close the stream:
            log.Close();
        }
    }
}