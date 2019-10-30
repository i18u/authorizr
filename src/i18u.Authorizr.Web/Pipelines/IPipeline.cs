using System;

namespace i18u.Authorizr.Web.Pipelines
{
    /// <summary>
    /// A conceptual representation of a code-defined pipeline.
    /// </summary>
    /// <typeparam name="TInput">The input to the pipeline.</typeparam>
    /// <typeparam name="TOutput">The output from the pipeline.</typeparam>
    public interface IPipeline<TInput, TOutput>
    {
        /// <summary>
        /// Executes the pipeline.
        /// </summary>
        /// <param name="input">The input parameter.</param>
        /// <param name="ctx">The pipeline context.</param>
        /// <returns>The output object.</returns>
        TOutput Execute(TInput input, PipelineContext ctx);

        /// <summary>
        /// Applies the next step to execute.
        /// </summary>
        /// <param name="step">The step to apply.</param>
        /// <typeparam name="TSubOutput">The type of the step's output.</typeparam>
        /// <returns>A new pipeline, comprised of the parent and child pipeline.</returns>
        IPipeline<TInput, TSubOutput> Then<TSubOutput>(IPipelineStep<TOutput, TSubOutput> step);

        /// <summary>
        /// Applies the next step to execute.
        /// </summary>
        /// <param name="function">The function to apply as the next step in the pipeline.</param>
        /// <typeparam name="TSubOutput">The type of the step's output.</typeparam>
        /// <returns>A new pipeline, comprised of the parent and child pipeline.</returns>
        IPipeline<TInput, TSubOutput> Then<TSubOutput>(Func<TOutput, PipelineContext, TSubOutput> function);
    }
}