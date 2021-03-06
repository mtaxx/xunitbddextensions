// Copyright 2009 Bj�rn Rochel - http://www.bjro.de/ 
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
using System.Linq;
using Xunit.Reporting.Internal;

namespace Xunit.Reporting.Specs.Internal
{
    [Concern(typeof (Context))]
    public class When_building_a_context_from_a_valid_context_type : StaticContextSpecification
    {
        private Context _context;

        protected override void Because()
        {
            _context = Context.BuildFrom(GetType());
        }

        [Observation]
        public void Should_create_a_context()
        {
            _context.ShouldNotBeNull();
        }

        [Observation]
        public void Should_create_observations_for_all_public_methods_marked_with_the__ObservationAttribute__()
        {
            _context.Count().ShouldBeEqualTo(2);
        }
    }

    [Concern(typeof(Context))]
    public class When_building_a_context_from_a_derived_context_type : StaticContextSpecification
    {
        private Context _context;

        protected override void Because()
        {
            _context = Context.BuildFrom(typeof(Foo_bar_derived_concern));
        }

        [Observation]
        public void Should_create_a_context()
        {
            _context.ShouldNotBeNull();
        }

        [Observation]
        public void Should_create_observations_for_all_public_methods_marked_with_the__ObservationAttribute__()
        {
            _context.Count().ShouldBeEqualTo(2);
        }
    }
    [Concern(typeof(Observation))]
    public class After_creating_a_readable_representation_of_a__Context__ : StaticContextSpecification
    {
        private Context _context;
        private string _readableRepresentation;

        protected override void EstablishContext()
        {
            _context = Context.BuildFrom(typeof(After_this__fake__specification_has_been_executed));
        }

        protected override void Because()
        {
            _readableRepresentation = _context.ToString();
        }

        [Observation]
        public void Should_have_capitalized_the_first_letter()
        {
            _readableRepresentation[0].ShouldBeEqualTo('A');
        }

        [Observation]
        public void Should_have_replaced_underscores_with_spaces()
        {
            _readableRepresentation.ShouldNotContain('_');
            _readableRepresentation.ShouldContain(' ');
        }

        [Observation]
        public void Should_have_replaced_double_underscores_with_double_quotes()
        {
            _readableRepresentation.ShouldContain("\"fake\"");
        }

        [Observation]
        public void Should_have_created_the_expected_text()
        {
            _readableRepresentation.ShouldBeEqualTo("After this \"fake\" specification has been executed");
        }
    }
}