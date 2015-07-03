using Yis.Framework.Core.Caching.Contract;

namespace Yis.Framework.Core.Caching
{
    /// <summary>
    /// Value info for the cache storage.
    /// </summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    internal class CacheStorageValueInfo<TValue>
    {
        #region Constructors + Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheStorageValueInfo{TValue}"/> class.
        /// </summary>
        /// <param name="value">           The value.</param>
        /// <param name="expirationPolicy">The expiration policy.</param>
        public CacheStorageValueInfo(TValue value, IExpirationPolicy expirationPolicy = null)
        {
            _value = value;
            _expirationPolicy = expirationPolicy;
        }

        #endregion Constructors + Destructors

        #region Fields

        /// <summary>
        /// The expiration policy.
        /// </summary>
        private readonly IExpirationPolicy _expirationPolicy;

        /// <summary>
        /// The value.
        /// </summary>
        private TValue _value;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this value can expire.
        /// </summary>
        /// <value><c>true</c> if this value can expire; otherwise, <c>false</c>.</value>
        public bool CanExpire
        {
            get
            {
                return _expirationPolicy != null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this value is expired.
        /// </summary>
        /// <value><c>true</c> if this value is expired; otherwise, <c>false</c>.</value>
        public bool IsExpired
        {
            get
            {
                return CanExpire && _expirationPolicy.IsExpired;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public TValue Value
        {
            get
            {
                if (CanExpire && _expirationPolicy.CanReset)
                {
                    _expirationPolicy.Reset();
                }

                return _value;
            }
        }

        #endregion Properties
    }
}