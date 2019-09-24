using System;

namespace i18u.Authorizr.Web.Pipelines
{
    /// <summary>
    /// Represents a step that is simply a function.
    /// </summary>
    /// <typeparam name="TInput">The type of the input to this step.</typeparam>
    /// <typeparam name="TOutput">The type of the output from this step.</typeparam>
    public class FunctionStep<TInput, TOutput> : PipelineStep<TInput, TOutput>
    {
        private Func<TInput, PipelineContext, TOutput> _function;

        /// <summary>
        /// Creates a new instance of the <see cref="FunctionStep{TInput, TOutput}" /> class.
        /// </summary>
        /// <param name="function">The function to execute as part of this pipeline step.</param>
        public FunctionStep(Func<TInput, PipelineContext, TOutput> function)
        {
            _function = function;
        }

        /// <inheritdoc cref="PipelineStep{TInput, TOutput}.Execute(TInput, PipelineContext)" />
        public override TOutput Execute(TInput input, PipelineContext ctx)
        {
            try 
            {
                var result = _function.Invoke(input, ctx);
                return result;
            } 
            catch (Exception ex)
            {
                ctx.Log(ex);
                ctx.Success = false;
                return default;
            }
        }
    }
}