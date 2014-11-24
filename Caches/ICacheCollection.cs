using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MFramework.Common.Core.Caches
{
    public interface ICacheCollection : IEnumerable<object>
    {
        /// <summary>
        /// Gets the number of objects stored in the cache.
        /// </summary>
        /// <value>
        /// The number of objects stored in the cache.
        /// </value>
        int Count { get; }

        /// <summary>
        /// Gets or sets the cached object with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">
        /// A unique identifier for the cached object.
        /// </param>
        /// <returns>
        /// The cached object with the specified <paramref name="key"/>.
        /// </returns>
        /// <remarks>
        /// You can use this property to retrieve a cached object, or to insert an object and a key for it to the cache.
        /// </remarks>
        object this[string key] { get; set; }

        /// <summary>
        /// Adds an object into the cache without overwriting any existing cached object.
        /// </summary>
        /// <param name="key">
        /// A unique identifier for the cached object.
        /// </param>
        /// <param name="value">The object to add.</param>
        void Add(string key,
                 object value);

        /// <summary>
        /// Adds an object into the cache without overwriting any existing cached object.
        /// </summary>
        /// <param name="key">
        /// A unique identifier for the cached object.
        /// </param>
        /// <param name="value">The object to add.</param>
        /// <param name="absoluteExpiration">
        /// The fixed date and time at which the added object expires and is removed from the cache.
        /// </param>
        void Add(string key,
                 object value,
                 DateTime absoluteExpiration);

        /// <summary>
        /// Adds an object into the cache without overwriting any existing cached object.
        /// </summary>
        /// <param name="key">
        /// A unique identifier for the cached object.
        /// </param>
        /// <param name="value">The object to add.</param>
        /// <param name="slidingExpiration">
        /// The interval between the time the added object was last accessed and the time at which that object expires.
        /// </param>
        void Add(string key,
                 object value,
                 TimeSpan slidingExpiration);

        /// <summary>
        /// Removes all objects from the cache.
        /// </summary>
        void Clear();

        /// <summary>
        /// Determines whether the cache contains an object with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">
        /// A unique identifier for the cached object.
        /// </param>
        /// <returns>
        /// <see langword="false"/> if the cache contains an object with the <paramref name="key"/>; otherwise, <see langword="false"/>.
        /// </returns>
        bool ContainsKey(string key);

        /// <summary>
        /// Retrieves the cached object with the specified <paramref name="key"/> from the cache.
        /// </summary>
        /// <param name="key">
        /// A unique identifier for the cached object.
        /// </param>
        /// <returns>
        /// The retrieved cached object.
        /// If the <paramref name="key"/> is not found, returns <see langword="null"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Following the runtime naming convention.")]
        object Get(string key);

        /// <summary>
        /// Retrieves the strongly typed cached object with the specified <paramref name="key"/> from the cache.
        /// </summary>
        /// <typeparam name="T">The expected type of the cached object.</typeparam>
        /// <param name="key">
        /// A unique identifier for the cached object.
        /// </param>
        /// <returns>
        /// The retrieved cached object.
        /// If the <paramref name="key"/> is not found, returns <see langword="null"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Following the runtime naming convention.")]
        T Get<T>(string key);

        /// <summary>
        /// Removes and returns the cached object with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">
        /// A unique identifier for the cached object.
        /// </param>
        /// <returns>
        /// The object removed from the Cache.
        /// If the <paramref name="key"/> is not found, returns <see langword="null"/>.
        /// </returns>
        object Remove(string key);

        /// <summary>
        /// Removes and returns the cached object with the specified <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="T">The expected type of the cached object.</typeparam>
        /// <param name="key">
        /// A unique identifier for the cached object.
        /// </param>
        /// <returns>
        /// The object removed from the Cache.
        /// If the <paramref name="key"/> is not found,
        /// returns the <see langword="default"/> object of <typeparamref name="T"/>.
        /// </returns>
        T Remove<T>(string key);

        /// <summary>
        /// Inserts an object into the cache, overwriting any existing cached object with a matching <paramref name="key"/>.
        /// </summary>
        /// <param name="key">
        /// A unique identifier for the cached object.
        /// </param>
        /// <param name="value">The object to insert.</param>
        /// <remarks>
        /// This method will overwrite an existing cached object whose key matches the <paramref name="key"/>.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set", Justification = "Following the runtime naming convention.")]
        void Set(string key,
                 object value);

        /// <summary>
        /// Inserts an object into the cache, overwriting any existing cached object with a matching <paramref name="key"/>.
        /// </summary>
        /// <param name="key">
        /// A unique identifier for the cached object.
        /// </param>
        /// <param name="value">The object to insert.</param>
        /// <param name="absoluteExpiration">
        /// The fixed date and time at which the inserted object expires and is removed from the cache.
        /// </param>
        /// <remarks>
        /// This method will overwrite an existing cached object whose key matches the <paramref name="key"/>.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set", Justification = "Following the runtime naming convention.")]
        void Set(string key,
                 object value,
                 DateTime absoluteExpiration);

        /// <summary>
        /// Inserts an object into the cache, overwriting any existing cached object with a matching <paramref name="key"/>.
        /// </summary>
        /// <param name="key">
        /// A unique identifier for the cached object.
        /// </param>
        /// <param name="value">The object to insert.</param>
        /// <param name="slidingExpiration">
        /// The interval between the time the inserted object was last accessed and the time at which that object expires.
        /// </param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set", Justification = "Following the runtime naming convention.")]
        void Set(string key,
                 object value,
                 TimeSpan slidingExpiration);
    }
}

