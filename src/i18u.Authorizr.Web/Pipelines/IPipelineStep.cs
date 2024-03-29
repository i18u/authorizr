namespace i18u.Authorizr.Web.Pipelines
{
    /// <summary>
    /// A step in a pipeline.
    /// </summary>
    /// <typeparam name="TInput">The input type for this pipeline step.</typeparam>
    /// <typeparam name="TOutput">The output type for this pipeline step.</typeparam>
    public interface IPipelineStep<TInput, TOutput>
    {
        /// <summary>
        /// The name of this pipeline step.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Executes the pipeline step, given the input and current context.
        /// </summary>
        /// <param name="input">The input for the pipeline step to operate on.</param>
        /// <param name="context">The current context for the pipeline.</param>
        /// <returns>The output generated by this step.</returns>
        TOutput Execute(TInput input, PipelineContext context);
    }
}