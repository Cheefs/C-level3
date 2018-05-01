namespace HomeWork
{
    interface IViev
    {
        /// <summary>
        /// Тема сообщения 
        /// </summary>
        string MsgHead { get; set; }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        string MsgText { get; set; }
        /// <summary>
        /// Пароль электронной почты отправителя
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// Электронная почта отправителя
        /// </summary>
        string Sender { get; set; }
        /// <summary>
        /// Настройка Smpt сервера
        /// </summary>
        string Config { get; set; }
    }
}
