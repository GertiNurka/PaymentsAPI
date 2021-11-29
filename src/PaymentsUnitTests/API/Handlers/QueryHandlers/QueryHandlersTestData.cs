using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PaymentsUnitTests.API.Handlers.QueryHandlers
{
    public class QueryHandlersTestData
    {
        public class ServiceResponseFail : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { false, StatusCodes.Status200OK };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
