/*
The MIT License (MIT)

Copyright (c) 2013 FeedbackHound

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

https://github.com/devtrends/Unity.WebAPI
*/

/*
This file was extracted from the Unity.WebAPI project due to a breaking change
in Unity 4.0, which prevents installation of the original Unity.WebAPI
*/

using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace FlightNode.Api.DependencyResolution
{
    public class UnityDependencyScope : IDependencyScope
    {
        protected IUnityContainer Container { get; private set; }

        public UnityDependencyScope(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            Container = container;
        }



        public object GetService(Type serviceType)
        {
            // WebAPI tries to resolve these types, but Unity isn't setup
            // for them. Lack of resolution doesn't seem  to cause a problem -
            // but each of them is throwin exceptions and that causes a 
            // performance degradation.
            if (serviceType == null ||
                serviceType == typeof(System.Web.Http.Dispatcher.IHttpControllerActivator) ||
                serviceType == typeof(System.Web.Http.Controllers.IHttpActionInvoker) ||
                serviceType.Name == "IModelValidatorCache")
            {
                return null;
            }


            try
            {
                return Container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                // Any other exception might really be a problem, so let it escape.
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.ResolveAll(serviceType);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
