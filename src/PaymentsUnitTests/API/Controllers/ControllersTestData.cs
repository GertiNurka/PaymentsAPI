using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;

namespace PaymentsUnitTests.API.Controllers
{
    class ControllersTestData
    {
        public class HandlerResponseOk<T> : IEnumerable<object[]> where T : new()
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { StatusCodes.Status200OK, new T() };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class HandlerResponseNotFound : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { StatusCodes.Status404NotFound };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class HandlerResponseNoContent : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { StatusCodes.Status204NoContent };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class HandlerResponseCreated<T> : IEnumerable<object[]> where T : new()
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { StatusCodes.Status201Created, new T() };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class HandlerResponseBadRequest : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { StatusCodes.Status400BadRequest };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
