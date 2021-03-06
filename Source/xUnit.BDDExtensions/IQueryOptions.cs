// Copyright 2010 xUnit.BDDExtensions
//   
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//       http://www.apache.org/licenses/LICENSE-2.0
//   
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//  
using System;

namespace Xunit
{
    /// <summary>
    ///   Defines a mock framework independent fluent interface for setting up behavior
    ///   for methods returning a result (queries).
    /// </summary>
    /// <typeparam name = "TReturn">
    ///   Specifies the return value of the behavior under configuration.
    /// </typeparam>
    public interface IQueryOptions<TReturn>
    {
        /// <summary>
        ///   Sets up the return value of a behavior.
        /// </summary>
        /// <param name = "returnValue">
        ///   Specifies the return value.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        IQueryOptions<TReturn> Return(TReturn returnValue);

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "valueFunction" />
        ///   will be used to evaluate the result value of a behavior.
        /// </summary>
        /// <param name = "valueFunction">
        ///   Specifies the function which is called when the method is called.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        /// <remarks>
        ///   Use this for configuring parameterless methods.
        /// </remarks>
        IQueryOptions<TReturn> Return(Func<TReturn> valueFunction);

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "valueFunction" />
        ///   will be used to evaluate the result value of a behavior.
        /// </summary>
        /// <param name = "valueFunction">
        ///   Specifies the function which is called when the method is called.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        /// <remarks>
        ///   Use this for configuring methods with a single parameter.
        /// </remarks>
        IQueryOptions<TReturn> Return<T>(Func<T, TReturn> valueFunction);

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "valueFunction" />
        ///   will be used to evaluate the result value of a behavior.
        /// </summary>
        /// <param name = "valueFunction">
        ///   Specifies the function which is called when the method is called.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        /// <remarks>
        ///   Use this for configuring methods with two parameters.
        /// </remarks>
        IQueryOptions<TReturn> Return<T1, T2>(Func<T1, T2, TReturn> valueFunction);

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "valueFunction" />
        ///   will be used to evaluate the result value of a behavior.
        /// </summary>
        /// <param name = "valueFunction">
        ///   Specifies the function which is called when the method is called.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        /// <remarks>
        ///   Use this for configuring methods with three parameters.
        /// </remarks>
        IQueryOptions<TReturn> Return<T1, T2, T3>(Func<T1, T2, T3, TReturn> valueFunction);

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "valueFunction" />
        ///   will be used to evaluate the result value of a behavior.
        /// </summary>
        /// <param name = "valueFunction">
        ///   Specifies the function which is called when the method is called.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        /// <remarks>
        ///   Use this for configuring methods with four parameters.
        /// </remarks>
        IQueryOptions<TReturn> Return<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TReturn> valueFunction);

        /// <summary>
        ///   Configures that the invocation of the related behavior
        ///   results in the specified <see cref = "Exception" /> beeing thrown.
        /// </summary>
        /// <param name = "exception">
        ///   Specifies the exception which should be thrown when the 
        ///   behavior is invoked.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        IQueryOptions<TReturn> Throw(Exception exception);
    }
}