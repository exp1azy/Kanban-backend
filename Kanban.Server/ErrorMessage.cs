namespace Kanban.Server
{
    public static class ErrorMessage
    {       
        public const string UserIdIsRequired = "Требуется идентификатор пользователя";
        public const string UserIdMustBeGreaterThanZero = "Идентификатор пользователя должен быть больше 0.";
        public const string UserNameIsRequired = "Требуется имя пользователя.";
        public const string CardIdIsRequired = "Требуется идентификатор карточки.";
        public const string CardIdMustBeGreaterThanZero = "Идентификатор карточки должен быть больше 0.";
        public const string CardNameIsRequired = "Требуется имя карточки.";
        public const string ColumnIdIsRequired = "Требуется идентификатор колонки.";
        public const string ColumnIdMustBeGreaterThanZero = "Идентификатор колонки должен быть больше 0.";
        public const string ColumnNameIsRequired = "Требуется имя колонки.";
        public const string BoardIdIsRequired = "Требуется идентификатор доски.";
        public const string BoardIdMustBeGreaterThanZero = "Идентификатор доски должен быть больше 0.";
        public const string BoardNameIsRequired = "Требуется имя доски.";
        public const string EmailIsRequired = "Требуется email-адрес.";
        public const string InvalidEmail = "Некорректный формат email-адреса.";
        public const string PasswordIsRequired = "Требуется пароль.";
    }
}
