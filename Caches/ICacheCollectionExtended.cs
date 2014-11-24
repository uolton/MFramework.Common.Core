using System;
using System.Diagnostics.CodeAnalysis;

namespace MFramework.Common.Core.Caches
{
    
        /// <summary>
        /// Extends the common interface for cache implementations to include lazy loading overloads.
        /// </summary>
        /// <remarks>
        /// .NET Framework 3.5 or above is required by this interface.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is intentional.")]
        public interface ICacheCollectionExtended : ICacheCollection
        {
            /// <summary>
            /// Retrieves the cached object with the specified <paramref name="key"/> from the cache, 
            /// invoking the <paramref name="add"/> to obtain a new object if the <paramref name="key"/> is not found.
            /// </summary>
            /// <param name="key">
            /// A unique identifier for the cached object.
            /// </param>
            /// <param name="add">
            /// A function which returns an object to be added to the cache if the <paramref name="key"/> is not found.
            /// </param>
            /// <returns>
            /// The retrieved cached object.
            /// If the <paramref name="key"/> is not found,
            /// adds and returns the result of the <paramref name="add"/> function.
            /// </returns>
            [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Following the runtime naming convention.")]
            object Get(string key,
                       Func<object> add);

            /// <summary>
            /// Retrieves the cached object with the specified <paramref name="key"/> from the cache, 
            /// invoking the <paramref name="add"/> to obtain a new object if the <paramref name="key"/> is not found.
            /// </summary>
            /// <param name="key">
            /// A unique identifier for the cached object.
            /// </param>
            /// <param name="add">
            /// A function which returns an object to be added to the cache if the <paramref name="key"/> is not found.
            /// </param>
            /// <returns>
            /// The retrieved cached object.
            /// If the <paramref name="key"/> is not found,
            /// adds and returns the result of the <paramref name="add"/> function.
            /// </returns>
            [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Following the runtime naming convention.")]
            object Get(string key,
                       Func<ICacheCollection, object> add);

            /// <summary>
            /// Retrieves the strongly typed cached object with the specified <paramref name="key"/> from the cache, 
            /// invoking the <paramref name="add"/> to obtain a new object if the <paramref name="key"/> is not found.
            /// </summary>
            /// <typeparam name="T">The expected type of the cached object.</typeparam>
            /// <param name="key">
            /// A unique identifier for the cached object.
            /// </param>
            /// <param name="add">
            /// A function which returns a strongly typed object to be added to the cache if the <paramref name="key"/> is not found.
            /// </param>
            /// <returns>
            /// The retrieved cached object.
            /// If the <paramref name="key"/> is not found,
            /// adds and returns the result of the <paramref name="add"/> function.
            /// </returns>
            [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Following the runtime naming convention.")]
            T Get<T>(string key,
                     Func<T> add);

            /// <summary>
            /// Retrieves the strongly typed cached object with the specified <paramref name="key"/> from the cache, 
            /// invoking the <paramref name="add"/> to obtain a new object if the <paramref name="key"/> is not found.
            /// </summary>
            /// <typeparam name="T">The expected type of the cached object.</typeparam>
            /// <param name="key">
            /// A unique identifier for the cached object.
            /// </param>
            /// <param name="add">
            /// A function which returns a strongly typed object to be added to the cache if the <paramref name="key"/> is not found.
            /// </param>
            /// <returns>
            /// The retrieved cached object.
            /// If the <paramref name="key"/> is not found,
            /// adds and returns the result of the <paramref name="add"/> function.
            /// </returns>
            [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Following the runtime naming convention.")]
            T Get<T>(string key,
                     Func<ICacheCollection, T> add);
        }
    }


