namespace No7.Solution
{
    using System;

    // Класс-точка доступа к любому логгеру, реализующему ISimpleLogger.
    // Зависимость внедряется через свойство.
    public static class LoggerService
    {
        // Внедрение зависимости через свойство не самый лучший вариант, 
        // но использование "дефолтного" логгера не кажется хорошей идеей,
        // поскольку его могут удалить и тогда всё рухнет.
        public static ISimpleLogger Logger { get; set; }

        public static void Warning(string msg)
        {
            ThrowForNullMessage(msg);

            ThrowForNullLogger();

            Logger.Warning(msg);
        }

        public static void Info(string msg)
        {
            ThrowForNullMessage(msg);

            ThrowForNullLogger();

            Logger.Info(msg);
        }

        public static void Error(string msg)
        {
            ThrowForNullMessage(msg);

            ThrowForNullLogger();

            Logger.Error(msg);
        }

        private static void ThrowForNullMessage(string msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }
        }

        private static void ThrowForNullLogger()
        {
            if (Logger == null)
            {
                throw new InvalidOperationException("Logger is null.");
            }
        }
    }
}
