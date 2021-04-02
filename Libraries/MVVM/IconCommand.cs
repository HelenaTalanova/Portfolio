using System.Windows.Media;

namespace MVVM
{
    public class IconCommand
    {
        public Command Command { get; }

        /// <summary>
        /// Комментарий к команде
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Иконка на кнопку команды
        /// </summary>
        public DrawingBrush IconButton { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="comment">Комментарий</param>
        /// <param name="iconButton">Иконка</param>
        /// <param name="command">Комманда</param>
        public IconCommand(string comment, DrawingBrush iconButton, Command command)
        {
            Comment = comment;
            IconButton = iconButton;
            Command = command;
        }
    }
}
