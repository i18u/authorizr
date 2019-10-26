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
}