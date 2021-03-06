// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbsoluteExpirationPolicy.cs" company="Catel development team">
//   Copyright (c) 2008 - 2014 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Yis.Framework.Core.Caching.Policies
{
    using System;

    /// <summary>
    /// The cache item will expire on the absolute expiration date time.
    /// </summary>
    public class AbsoluteExpirationPolicy : ExpirationPolicy
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AbsoluteExpirationPolicy"/> class.
        /// </summary>
        /// <param name="absoluteExpirationDateTime">
        /// The expiration date time.
        /// </param>
        internal AbsoluteExpirationPolicy(DateTime absoluteExpirationDateTime)
            : this(absoluteExpirationDateTime, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbsoluteExpirationPolicy"/> class.
        /// </summary>
        /// <param name="absoluteExpirationDateTime">
        /// The expiration date time.
        /// </param>
        /// <param name="canReset">
        /// The can reset.
        /// </param>
        protected AbsoluteExpirationPolicy(DateTime absoluteExpirationDateTime, bool canReset)
            : base(canReset)
        {
            AbsoluteExpirationDateTime = absoluteExpirationDateTime;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether is expired.
        /// </summary>
        public override bool IsExpired
        {
            get
            {
                return DateTime.Now > AbsoluteExpirationDateTime;
            }
        }

        /// <summary>
        /// Gets or sets the expiration date time.
        /// </summary>
        protected DateTime AbsoluteExpirationDateTime { get; set; }

        #endregion Properties
    }
}