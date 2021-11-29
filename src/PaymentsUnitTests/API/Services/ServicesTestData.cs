using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PaymentsUnitTests.API.Services
{
    public class ServicesTestData
    {
        public class HandlerResponseOk<T> : IEnumerable<object[]> where T : new()
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new T() };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class NullObject : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { null };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class NoContent<T> : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new List<T>() };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class CollectionOf<T> : IEnumerable<object[]> where T : new()
        {
            public IEnumerator<object[]> GetEnumerator()
            {

                yield return new object[] { new List<T> { new T() } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class CancellationTk : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new CancellationToken() };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
