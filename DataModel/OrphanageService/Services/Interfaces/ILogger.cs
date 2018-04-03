using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageService.Services.Interfaces
{
    /// <summary>
    /// Used for logging
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Write a log event.
        /// For  internal control flow and diagnostic state dumps to facilitate pinpointing of recognized problems.
        /// </summary>
        /// <param name="messageTemplate">Message which should be logged</param>
        void Debug(string messageTemplate);

        /// <summary>
        /// Write a log event.
        /// For events of interest or that have relevance to outside observers; the default enabled minimum logging level.
        /// </summary>
        /// <param name="messageTemplate">Message which should be logged</param>
        void Information(string messageTemplate);

        /// <summary>
        /// Write a log event.
        /// For indicators of possible issues or service/functionality degradation.
        /// </summary>
        /// <param name="messageTemplate">Message which should be logged</param>
        void Warning(string messageTemplate);

        /// <summary>
        /// Write a log event.
        /// For indicating a failure within the application or connected system.
        /// </summary>
        /// <param name="messageTemplate">Message which should be logged</param>
        void Error(string messageTemplate);

        /// <summary>
        /// Write a log event.
        /// For critical errors causing complete failure of the application.
        /// </summary>
        /// <param name="messageTemplate">Message which should be logged</param>
        void Fatal(string messageTemplate);

        /// <summary>
        /// Write a log event.
        /// For  internal control flow and diagnostic state dumps to facilitate pinpointing of recognized problems.
        /// </summary>
        /// <param name="messageTemplate">Message which should be logged</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        void Debug(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event.
        /// For events of interest or that have relevance to outside observers; the default enabled minimum logging level.
        /// </summary>
        /// <param name="messageTemplate">Message which should be logged</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        void Information(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event.
        /// For indicators of possible issues or service/functionality degradation.
        /// </summary>
        /// <param name="messageTemplate">Message which should be logged</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        void Warning(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event.
        /// For indicating a failure within the application or connected system.
        /// </summary>
        /// <param name="messageTemplate">Message which should be logged</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        void Error(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event.
        /// For critical errors causing complete failure of the application.
        /// </summary>
        /// <param name="messageTemplate">Message which should be logged</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        void Fatal(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event.
        /// For  internal control flow and diagnostic state dumps to facilitate pinpointing of recognized problems.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message which should be logged.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        void Debug(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event.
        /// For events of interest or that have relevance to outside observers; the default enabled minimum logging level.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message which should be logged.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        void Information(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event.
        /// For indicators of possible issues or service/functionality degradation.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message which should be logged.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        void Warning(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event.
        /// For indicating a failure within the application or connected system.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message which should be logged.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        void Error(Exception exception, string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Write a log event.
        /// For critical errors causing complete failure of the application.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message which should be logged.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        void Fatal(Exception exception, string messageTemplate, params object[] propertyValues);
    }
}