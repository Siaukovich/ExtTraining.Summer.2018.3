using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No7.Solution
{
    // Класс-точка доступа к любому логгеру, реализующему ISimpleLogger.
    // Зависимость внедряется через свойство.
    public class LoggerService
    {
        private static readonly Lazy<LoggerService> LazyLogger = 
            new Lazy<LoggerService>(() => new LoggerService());

        public static LoggerService Instance => LazyLogger.Value;

        // Внедрение зависимости через свойство не самый лучший вариант, 
        // но для синглтона конструктор должен быть закрытым,
        // а использование "дефолтного" логгера не кажется хорошей идеей.
        public ISimpleLogger Logger { get; set; }

        private LoggerService()
        {
        }

        public void Warning(string msg)
        {
            this.ThrowForNullMessage(msg);

            this.ThrowForNullLogger();

            this.Logger.Warning(msg);
        }

        public void Info(string msg)
        {
            this.ThrowForNullMessage(msg);

            this.ThrowForNullLogger();

            this.Logger.Info(msg);
        }

        public void Error(string msg)
        {
            this.ThrowForNullMessage(msg);

            this.ThrowForNullLogger();

            this.Logger.Error(msg);
        }

        private void ThrowForNullMessage(string msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }
        }

        private void ThrowForNullLogger()
        {
            if (this.Logger == null)
            {
                throw new InvalidOperationException("Logger is null.");
            }
        }
    }
}
