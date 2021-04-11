using System;
using System.Threading;

namespace Spam
{
    /// <summary>
    /// Класс заглушка сервиса отправки сообщения
    /// </summary>
    class ServiceEmail
    {
        private readonly Random RandomDelaySend = new Random();

        public bool SendMessage(string email, string message)
        {
            /// Случайное время отправки сообщения
            var delay = RandomDelaySend.Next(0, 5);

            /// Выжидаем отправку
            Thread.Sleep(delay);

            /// Лог для наглядности
            Console.Write($"{email} ");

            /// Случайный результат отправки
            return delay < 4;
        }
    }
}
