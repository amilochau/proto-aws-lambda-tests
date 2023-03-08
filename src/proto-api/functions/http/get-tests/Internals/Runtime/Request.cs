﻿using Milochau.Proto.Http.GetTests.Internals.Runtime.Context;
using System;
using System.IO;

namespace Milochau.Proto.Http.GetTests.Internals.Runtime
{
    public class InvocationRequest : IDisposable
    {
        /// <summary>
        /// Input to the function invocation.
        /// </summary>
        public Stream? InputStream { get; set; }

        /// <summary>
        /// Context for the invocation.
        /// </summary>
        public ILambdaContext LambdaContext { get; set; }

        internal InvocationRequest(Stream? inputStream, ILambdaContext lambdaContext)
        {
            InputStream = inputStream;
            LambdaContext = lambdaContext;
        }

        public void Dispose()
        {
            InputStream?.Dispose();
        }
    }
}
