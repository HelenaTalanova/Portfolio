using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spam
{
    class ServiceSending
    {
        /// <summary>
        /// Очередь адресов для рассылки
        /// </summary>
        private readonly Queue<string> Emails;
        private readonly CancellationTokenSource sourceToken;

        private readonly string PathOk;
        private readonly string PathStart;
        private readonly string PathError;
        private readonly object lockOk = new object();
        private readonly object lockStart = new object();
        private readonly object lockError = new object();

        public ServiceSending(string pathEmails)
        {
            sourceToken = new CancellationTokenSource();
            PathOk = pathEmails.Insert(pathEmails.IndexOf('.') - 1, "_ok");
            PathStart = pathEmails.Insert(pathEmails.IndexOf('.') - 1, "_started");
            PathError = pathEmails.Insert(pathEmails.IndexOf('.') - 1, "_error");

            var all = ReadEmails(pathEmails);
            var ok = ReadEmails(PathOk);
            var start = ReadEmails(PathStart);
            var error = ReadEmails(PathError);

            var notKnow = start.Except(ok).Except(error);

            Emails = new Queue<string>(all.Except(ok).Except(notKnow));

            Console.WriteLine($"Всего - {all.Count()}");
            Console.WriteLine($"Отправленно - {ok.Count()}");
            Console.WriteLine($"Ошибок отправки - {error.Count()}");
            Console.WriteLine($"Не известен результат отправки - {notKnow.Count()}");
            Console.WriteLine($"Подготовленно к отправке - {Emails.Count}");
        }

        /// <summary>
        /// Отсановить рассылку
        /// Предусмотренно для завершения отправки при проблемами с питание,
        /// желательно не допускать запуск сервиса без автономного питания,
        /// ведь отключение света процесс не предстказуемый и запись в файл может быть не выполненна...
        /// что не дает возможности быть уверенным что на адрес было отправлено сообщение.
        /// </summary>
        public void StopSending()
        {
            sourceToken.Cancel();
        }

        /// <summary>
        /// Рассылка сообщения
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="send">Метод отправки сообщений</param>
        public async void SendMessage(string message, Func<string, string, bool> send)
        {
            await Task.Delay(1000);
            Console.WriteLine("Рассылка запущена");

            /// Открывааем файлы для логирования
            using (StreamWriter wOk = new StreamWriter(PathOk, true))
            using (StreamWriter wStart = new StreamWriter(PathStart, true))
            using (StreamWriter wError = new StreamWriter(PathError, true))
            {
                /// Процесс рассылки
                Action action = () =>
                {
                    var cancellation = sourceToken.Token;
                    /// Продолжаем пока есть адреса, или до сигнала остановки
                    while (Emails.Count > 0 && cancellation.IsCancellationRequested == false)
                    {
                        /// Получаем адрес
                        var email = Emails.Dequeue();
                        lock (lockStart)
                            wStart.WriteLine(email);

                        /// Отправляем средствами внешнего метода.
                        if (send(email, message))
                        {
                            /// Успешная отправки, сохраняем рузультат
                            lock (lockOk)
                                wOk.WriteLine(email);
                        }
                        else
                        {
                            /// Ошибка отправки, сохраняем результат
                            lock (lockError)
                                wError.WriteLine(email);
                        }
                    }
                };

                /// Массив методов для параллельного исполнения
                Action[] actions = new Action[Environment.ProcessorCount * 4];
                for (int i = 0; i < actions.Length; i++)
                    actions[i] = action;

                /// Запуск рассылки
                Parallel.Invoke(actions.ToArray());
            }

            Console.WriteLine();
            Console.WriteLine("Процесс рассылки завершен");
        }

        /// <summary>
        /// Чтение адресов из файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static IEnumerable<string> ReadEmails(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                        yield return reader.ReadLine();
                }
            }
        }
    }
}
