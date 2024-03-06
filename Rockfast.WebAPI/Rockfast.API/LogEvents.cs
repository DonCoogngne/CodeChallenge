using System;
namespace Rockfast.API
{
	public class LogEvents
	{
        public static void LogToFile(string Title, string LogMessage, IWebHostEnvironment env)
        {
            bool exists = Directory.Exists(env.ContentRootPath + "/" + "LogFolder");
            if (!exists)
            {
                Directory.CreateDirectory(env.ContentRootPath + "/" + "LogFolder");
            }

            StreamWriter swlog;
            string logPath;

            string fileName = DateTime.Now.ToString("ddMMyyyy") + ".txt";
            logPath = Path.Combine(env.ContentRootPath + "/" + "LogFolder" + "/" + fileName);
            if (!File.Exists(logPath))
            {
                swlog = new StreamWriter(logPath);
            }
            else
            {
                swlog = File.AppendText(logPath);
            }

            swlog.WriteLine("Log Entry");
            swlog.WriteLine("{0} {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
            swlog.WriteLine("Message Title : {0}", Title);
            swlog.WriteLine("Message : {0}", LogMessage);
            swlog.WriteLine("----------------------------------------------------------------------------");
            swlog.WriteLine("");
            swlog.Close();
        }
    }
}

