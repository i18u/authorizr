using System;

namespace i18u.Authorizr.Web.Pipelines
{
	/// <summary>
    /// The static Pipeline class for helper pipeline methods.
    /// </summary>
    public static class Pipeline
    {
        /// <summary>
        /// Creates a new pipeline from the given <see cref="IPipelineStep{TInput, TOutput}"/>.
        /// </summary>
        /// <param name="step">The step to create the pipeline from.</param>
        /// <typeparam name="TInput">The type of the input to the pipeline.</typeparam>
        /// <typeparam name="TOutput">The type of the output to the pipeline.</typeparam>
        /// <returns>The pipeline object generated.</returns>
        public static IPipeline<TInput, TOutput> Create<TInput, TOutput>(IPipelineStep<TInput, TOutput> step)
        {
            return new TerminalPipeline<TInput, TOutput>(step);
        }

        /// <summary>
        /// Creates a new pipeline from the given <see cref="IPipelineStep{TInput, TOutput}"/>.
        /// </summary>
        /// <param name="function">The function to create the pipeline from.</param>
        /// <typeparam name="TInput">The type of the input to the pipeline.</typeparam>
        /// <typeparam name="TOutput">The type of the output to the pipeline.</typeparam>
        /// <returns>The pipeline object generated.</returns>
        public static IPipeline<TInput, TOutput> Create<TInput, TOutput>(Func<TInput, PipelineContext, TOutput> function)
        {
            var functionWrapper = new FunctionStep<TInput, TOutput>(function);
            return Create(functionWrapper);
        }
    }
}