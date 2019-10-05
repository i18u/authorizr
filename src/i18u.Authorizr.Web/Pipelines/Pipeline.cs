using System;

namespace i18u.Authorizr.Web.Pipelines
{
    /// <summary>
    /// A base pipeline implementation.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TSubInput">The inner step output type (or outer step input type).</typeparam>
    /// <typeparam name="TOutput">The output of the pipeline.</typeparam>
    public abstract class PipelineBase<TInput, TSubInput, TOutput> : IPipeline<TInput, TOutput>
    {
        /// <summary>
        /// The current step of the pipeline to execute.
        /// </summary>
        protected IPipelineStep<TSubInput, TOutput> CurrentStep;

        /// <summary>
        /// Creates a new instance of the <see cref="PipelineBase{TInput, TSubInput, TOutput}" /> class.
        /// </summary>
        /// <param name="step">The step to treat as the current step.</param>
        public PipelineBase(IPipelineStep<TSubInput, TOutput> step)
        {
            CurrentStep = step;
        }

        /// <inheritdoc />
        public IPipeline<TInput, TSubOutput> Then<TSubOutput>(IPipelineStep<TOutput, TSubOutput> step)
        {
            return new Pipeline<TInput, TOutput, TSubOutput>(step, this);
        }

        /// <inheritdoc />
        public IPipeline<TInput, TSubOutput> Then<TSubOutput>(Func<TOutput, PipelineContext, TSubOutput> function)
        {
            var functionWrapper = new FunctionStep<TOutput, TSubOutput>(function);
            return Then(functionWrapper);
        }

        /// <inheritdoc />
        public TOutput Execute(TInput input, PipelineContext ctx)
        {
            var result = ExecuteSubPipeline(input, ctx);
            return result;
        }

        /// <summary>
        /// Executes the sub pipeline of this pipeline.
        /// </summary>
        /// <param name="input">The input for the sub pipeline.</param>
        /// <param name="ctx">The context for the current pipeline's execution.</param>
        /// <returns>The output generated from processing the sub pipeline.</returns>
        protected abstract TOutput ExecuteSubPipeline(TInput input, PipelineContext ctx);
    }

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