﻿// Copyright 2010 xUnit.BDDExtensions
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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Xunit.Reporting.Internal
{
    /// <summary>
    ///   A concern represents a grouping element for context specifications.
    /// </summary>
    public class Concern : IEnumerable<Context>
    {
        private readonly List<Context> _contexts = new List<Context>();
        private readonly Type _relatedType;
        private readonly string _scenario;

        /// <summary>
        ///   Creates a new instance of the <see cref = "Concern" /> class.
        /// </summary>
        /// <param name = "relatedType">
        ///   Specifies the type under test.
        /// </param>
        /// <param name = "scenario">
        ///   Specifies an optional scenario.
        /// </param>
        private Concern(Type relatedType, string scenario)
        {
            Debug.Assert(relatedType != null);

            _relatedType = relatedType;
            _scenario = scenario;
        }

        /// <summary>
        ///   Gets a number indicating the amount of observations
        ///   related to this concern.
        /// </summary>
        public int AmountOfObservations
        {
            get
            {
                return _contexts.Sum(context => context.AmountOfObservations);
            }
        }

        /// <summary>
        ///   Gets a number indicating the amount of context specifications
        ///   related to this concern.
        /// </summary>
        public int AmountOfContexts
        {
            get
            {
                return _contexts.Count();
            }
        }

        #region IEnumerable<Context> Members

        /// <summary>
        ///   Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///   A <see cref = "T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Context> GetEnumerator()
        {
            foreach (var context in _contexts)
            {
                yield return context;
            }
        }

        /// <summary>
        ///   Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///   An <see cref = "T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        ///   Adds a context specification to this concern.
        /// </summary>
        /// <param name = "context">
        ///   Specifies the context to be added.
        /// </param>
        public void AddContext(Context context)
        {
            if (!_contexts.Contains(context))
            {
                _contexts.Add(context);
            }
        }

        /// <summary>
        ///   Builds a concern from the type specified via <paramref name = "specType" />.
        /// </summary>
        /// <param name = "specType">
        ///   Specifies the type to build the concern from.
        /// </param>
        /// <returns>
        ///   A <see cref = "Concern" />.
        /// </returns>
        public static Concern BuildFrom(Type specType)
        {
            Debug.Assert(Context.Specification(specType));

            var concernAttribute = specType.GetFirstAttribute<ConcernAttribute>();

            Debug.Assert(concernAttribute != null);

            return new Concern(
                concernAttribute.Type,
                concernAttribute.Scenario);
        }

        /// <summary>
        ///   Gets a value indicating whether this concern is
        ///   related to the spec type supplied via <paramref name = "specType" />.
        /// </summary>
        /// <param name = "specType">
        ///   Specifies the type to check agains.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the type is related to this concern; Otherwise <c>false</c>.
        /// </returns>
        public bool IsRelatedTo(Type specType)
        {
            var concernAttribute = specType.GetFirstAttribute<ConcernAttribute>();

            Debug.Assert(concernAttribute != null);

            return Equals(_relatedType, concernAttribute.Type) &&
                   string.Equals(_scenario, concernAttribute.Scenario);
        }

        /// <summary>
        ///   Creates a textual representation of this <see cref = "Concern" />.
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(_relatedType.Name);

            if (!string.IsNullOrEmpty(_scenario))
            {
                sb.AppendFormat(" ({0})", _scenario);
            }

            return sb.ToString();
        }
    }
}