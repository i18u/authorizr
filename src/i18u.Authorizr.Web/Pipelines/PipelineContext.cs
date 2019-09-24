using System.Collections.Generic;
using System.Threading.Tasks;

namespace i18u.Authorizr.Web.Pipelines
{
    /// <summary>
    /// The context for the pipeline.
    /// </summary>
    public class PipelineContext
    {
        private readonly List<string> _logs = new List<string>();

        /// <summary>
        /// Whether or not the pipeline has been successful.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// The logs for the pipeline.
        /// </summary>
        public IReadOnlyList<string> Logs => _logs;

        /// <summary>
        /// Adds a new log message to the context.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Log(string message)
        {
            Task.Run(() =>
            {
                _logs.Add(message);
            });
        }

        /// <summary>
        /// Adds a new log message to the context.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Log(object message)
        {
            Log(message.ToString());
        }
    }
}