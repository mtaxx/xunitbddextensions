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
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    ///   A specialized <see cref = "ObservationAttribute" /> for using 
    ///   Moq as the underlying mocking engine.
    /// </summary>
    public sealed class ObservationAttribute : ObservationAttributeBase
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "ObservationAttribute" /> class.
        /// </summary>
        static ObservationAttribute()
        {
            Core.Configure(cfg => cfg.MockingEngineIs<MoqMockingEngine>());
        }
    }
}