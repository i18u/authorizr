using System;

namespace i18u.Authorizr.Web.Pipelines
{
    /// <summary>
    /// A pipeline object representing all of the steps required to convert an 
    /// input object of type 'a' to the output object of type 'b', inclduing any 
    /// intermediary conversions required.
    /// </summary>
    /// <typeparam name="TInput">The type of the input to the pipeline.</typeparam>
    /// <typeparam name="TSubInput">The type of the output of the first step, or input of the second step.</typeparam>
    /// <typeparam name="TOutput">The type of the output of the last step.</typeparam>
    public class Pipeline<TInput, TSubInput, TOutput> : PipelineBase<TInput, TSubInput, TOutput>
    {
        private IPipeline<TInput, TSubInput> _previousPipeline;

        /// <summary>
        /// Creates a new instance of the <see cref="Pipeline{TInput, TSubInput, TOutput}" /> class.
        /// </summary>
        /// <param name="step">The current (latest) step for the pipeline.</param>
        /// <param name="previousPipeline">The previous pipeline object that we call back to for input to our pipeline step.</param>
        public Pipeline(IPipelineStep<TSubInput, TOutput> step, IPipeline<TInput, TSubInput> previousPipeline) : base(step)
        {
            _previousPipeline = previousPipeline;
        }

        /// <inheritdoc />
        protected override TOutput ExecuteSubPipeline(TInput input, PipelineContext ctx)
        {
            var previousPipelineResult = _previousPipeline.Execute(input, ctx);

            if (!ctx.Success)
            {
                Console.WriteLine($"The pipeline step before {CurrentStep.Name} failed.");
                return default;
            }

            return CurrentStep.Execute(previousPipelineResult, ctx);
        }
    }
}