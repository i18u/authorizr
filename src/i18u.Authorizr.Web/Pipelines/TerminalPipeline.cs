namespace i18u.Authorizr.Web.Pipelines
{
	/// <summary>
    /// The pipeline to generate for the beginning of a pipeline.
    /// </summary>
    /// <typeparam name="TInput">The input type for the pipeline.</typeparam>
    /// <typeparam name="TOutput">The output type for the pipeline.</typeparam>
    public class TerminalPipeline<TInput, TOutput> : PipelineBase<TInput, TInput, TOutput>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TerminalPipeline{TInput, TOutput}" /> class.
        /// </summary>
        /// <param name="step">The step to initialize this pipeline with.</param>
        public TerminalPipeline(IPipelineStep<TInput, TOutput> step) : base(step)
        {
        }

        /// <inheritdoc />
        protected override TOutput ExecuteSubPipeline(TInput input, PipelineContext ctx)
        {
            return CurrentStep.Execute(input, ctx);
        }
    }
}