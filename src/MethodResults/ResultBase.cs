using System;
using System.Collections.Generic;
using System.Linq;

namespace MethodResults
{
    public abstract class ResultBase
    {
        public bool Succeed => !Failed;

        public bool Failed => Errors.Any();

        public List<string> Errors { get; } = new List<string>();


        public string ErrorMessage => Errors.Any() ? string.Join("\n", Errors) : string.Empty;

        protected void ThrowIfFailed()
        {
            if (!Succeed)
                throw new InvalidOperationException("Result is in status failed. Value is not set.");
        }
    }

    public abstract class ResultBase<TResult> : ResultBase where TResult : ResultBase<TResult>
    {
        public TResult WithErrors(params string[] errors)
        {
            Errors.AddRange(errors.Where(x => !string.IsNullOrWhiteSpace(x)));
            return (TResult)this;
        }

        public TResult WithErrors(IEnumerable<string> errors)
        {
            Errors.AddRange(errors.Where(x => !string.IsNullOrWhiteSpace(x)));
            return (TResult)this;
        }
    }
}