using System;

namespace MVVM
{
    public class CommandIcon<T> : Command
    {
        /// <summary>
        /// Комментарий к команде
        /// </summary>
        public string Comment { get; }

        /// <summary>
        /// Иконка на кнопку команды
        /// </summary>
        public T Icon { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Execute">Действие команды</param>
        /// <param name="Icon">Иконка кнопки</param>
        /// <param name="Comment">Коментарий</param>
        public CommandIcon(Action Execute, T Icon = default, string Comment = default)
            : base(Execute)
        {
            this.Comment = Comment;
            this.Icon = Icon;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Execute">Действие команды</param>
        /// <param name="CanExecute">Условие выполения команды</param>
        /// <param name="Icon">Иконка кнопки</param>
        /// <param name="Comment">Коментарий</param>
        public CommandIcon(Action Execute, Func<bool> CanExecute, T Icon = default, string Comment = default)
            : base(Execute, CanExecute)
        {
            this.Comment = Comment;
            this.Icon = Icon;
        }
    }
}
