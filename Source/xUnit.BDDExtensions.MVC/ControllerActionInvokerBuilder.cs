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
using System.Linq.Expressions;
using System.Web.Mvc;
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    /// Creates a MockedRequestContext, serialize paramters to FormCollection and finally
    /// invoke the the Action
    /// </summary>
    public class ControllerActionInvokerBuilder
    {
        private Expression _actionExpression;
        private Controller _controller;
        private readonly RequestContextBuilder _contextBuilder;
        private readonly ISpecificationActionInvoker _actionInvoker;

        public ControllerActionInvokerBuilder(IDependencyAccessor dependencyAccessor,
                                              ISpecificationActionInvoker actionInvoker)
        {
            _contextBuilder = new RequestContextBuilder(dependencyAccessor);
            _actionInvoker = actionInvoker;
        }

        public IMockedRequestContext RequestContext
        {
            get { return _contextBuilder; }
        }

        public ControllerActionInvokerBuilder Action<T, TResult>(Expression<Func<T, TResult>> expression)
            where T : Controller
        {
            _actionExpression = expression;
            return this;
        }

        public object InvokeAction()
        {
            ConvertParameterToFormCollection();

            var controllerContext = new ControllerContext(_contextBuilder, _controller);
            _controller.ControllerContext = controllerContext;

            var actionName = MvcExpressionHelper.GetMemberName(_actionExpression);

            _contextBuilder.RouteData.Values["action"] = actionName;
            _contextBuilder.RouteData.Values["controller"] = GetControllerName();

            _actionInvoker.InvokeAction(controllerContext, actionName);

            return _actionInvoker.Result;
        }

        private string GetControllerName()
        {
            var name = _controller.GetType().Name;
            return name.Substring(0, name.Length - "Controller".Length);
        }

        private void ConvertParameterToFormCollection()
        {
            var parameterNames = MvcExpressionHelper.GetParameterNames(_actionExpression);

            foreach (var parameterName in parameterNames)
            {
                var parameterValue = MvcExpressionHelper.GetParameterValue(_actionExpression, parameterName);
                _contextBuilder.SerializeModelToForm(parameterValue, parameterName);

                SerializableToRouteData(parameterValue, parameterName);
            }
        }

        private void SerializableToRouteData(object parameterValue, string parameterName)
        {
            if (parameterValue == null)
            {
                return;
            }
            if (parameterValue.GetType().IsPrimitive)
            {
                _contextBuilder.RouteData.Values[parameterName] = parameterValue;
            }
        }

        public ControllerActionInvokerBuilder Controller(Controller controllerUnderTest)
        {
            _controller = controllerUnderTest;
            return this;
        }
    }
}