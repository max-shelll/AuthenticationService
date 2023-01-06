using AuthenticationService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;

namespace AuthenticationService.Middlewares
{
    public class Logger : ILogger
    {
        private ReaderWriterLockSlim lock_ = new ReaderWriterLockSlim();

        private string logDirectory { get; set; }

        public Logger()
        {
            logDirectory = AppDomain.CurrentDomain.BaseDirectory + @"/_logs/" + DateTime.Now.ToString("dd-MM-yy HH-mm-ss") + @"/";
            Console.WriteLine(logDirectory);
            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);
        }

        /// <summary>
        /// Метод для записи событий в event.txt
        /// </summary>
        /// <param name="eventMessage">Сообщение события</param>
        public void WriteEvent(string eventMessage)
        {
            lock_.EnterWriteLock();
            try
            {
                using (StreamWriter writer = new StreamWriter(logDirectory + "events.txt", append: true))
                {
                    writer.WriteLine(eventMessage);
                }
            }
            finally
            {
                lock_.ExitWriteLock();
            }

        }

        /// <summary>
        /// Метод для записи ошибок в error.txt
        /// </summary>
        /// <param name="errorMessage">Сообщение ошибки</param>
        public void WriteError(string errorMessage)
        {
            lock_.EnterWriteLock();
            try
            {
                using (StreamWriter writer = new StreamWriter("errors.txt", append: true))
                {
                    writer.WriteLine(errorMessage);
                }
            }
            finally
            {
                lock_.ExitWriteLock();
            }

        }
    }
}
