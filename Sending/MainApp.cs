using System;
using System.IO;
using System.Threading.Tasks;

namespace Spam
{
    class MainApp
    {
        static void Main(string[] args)
        {
            /// создать файл адресов для теста
            CreateEmailsList("Emails.txt", 10_000);

            /// создаем сервис рассылки
            var serviceSending = new ServiceSending("Emails.txt");
            var serviceEmail = new ServiceEmail();

            Console.ReadLine();
            

            serviceSending.SendMessage("test", serviceEmail.SendMessage);

            /// Тест внешней остановки процесса
            Task.Run(() =>
            {
                Task.Delay(5000).Wait();
                serviceSending.StopSending();
                Console.WriteLine();
                Console.WriteLine("Сигнал остановки рассылки");
            });

            Console.WriteLine();
            Console.ReadLine();
        }

        /// <summary>
        /// Создаем список адресов.
        /// </summary>
        /// <param name="path">Файл</param>
        /// <param name="count">Количество адресов</param>
        static void CreateEmailsList(string path, int count)
        {
            if (!File.Exists(path))
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    for (int i = 0; i < count; i++)
                        writer.WriteLine(i);
                }

                Console.WriteLine($"Создан файл {path} - добавлено адресов: {count}");
                Console.WriteLine();
            }
        }
    }
}
