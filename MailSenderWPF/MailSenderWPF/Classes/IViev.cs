namespace MailSenderWPF
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
        /// Электронная почта отправителя
        /// </summary>
        string Sender { get; set; }
        /// <summary>
        /// Настройка Smpt сервера
        /// </summary>
        string SmptServer { get; set; }
      
    }
}
