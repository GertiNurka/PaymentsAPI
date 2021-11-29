using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;

namespace PaymentsUnitTests.API.Handlers.CommandHandlers
{
    public class CommandHandlersTestData
    {
        public class ServiceResponseOk : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { true, StatusCodes.Status200OK };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

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
