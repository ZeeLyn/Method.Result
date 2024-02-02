using System;
using System.Linq;

namespace MethodResults
{
    public class Result : ResultBase<Result>
    {
        public static Result Fail(params string[] error)
        {
            return new Result().WithErrors(error);
        }


        public static Result Merge(params Result[] results)
        {
            return new Result().WithErrors(results.SelectMany(x => x.Errors));
        }

        public static Result Ok()
        {
            return new Result();
        }

        public static Result<TValue> Ok<TValue>(TValue value)
        {
            return new Result<TValue>().WithValue(value);
        }

        public Result<TNewValue> ToResult<TNewValue>(TNewValue newValue = default)
        {
            return new Result<TNewValue>().WithValue(newValue).WithErrors(Errors);
        }


        public static Result FailIf(bool ifFailure, string error)
        {
            return ifFailure ? Fail(error) : Ok();
        }

        public static Result FailIf(bool ifFailure, Func<string> errorMessageAction)
        {
            return ifFailure ? Fail(errorMessageAction()) : Ok();
        }

        public static Result FailIf(Func<bool> func, string error)
        {
            return func() ? Fail(error) : Ok();
        }

        public static Result FailIf(Func<bool> func, Func<string> errorMessageAction)
        {
            return func() ? Fail(errorMessageAction()) : Ok();
        }
    }

    public class Result<TValue> : ResultBase<Result<TValue>>
    {
        private TValue _value;

        public TValue Value
        {
            get
            {
                ThrowIfFailed();
                return _value;
            }
            internal set => _value = value;
        }

        public Result<TValue> WithValue(TValue value)
        {
            Value = value;
            return this;
        }


        public Result ToResult()
        {
            return new Result().WithErrors(Errors);
        }


        public static implicit operator Result<TValue>(Result result)
        {
            return result.ToResult<TValue>();
        }
    }
}