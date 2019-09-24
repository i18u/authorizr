namespace i18u.Authorizr.Web.Pipelines
{
    /// <summary>
    /// A step in a pipeline.
    /// </summary>
    /// <typeparam name="TInput">The type of the input for this step.</typeparam>
    /// <typeparam name="TOutput">The type of the output for this step.</typeparam>
    public abstract class PipelineStep<TInput, TOutput> : IPipelineStep<TInput, TOutput>
    {
        /// <inheritdoc />
        public abstract TOutput Execute(TInput input, PipelineContext ctx);
    }
}