// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DurationExpirationPolicy.cs" company="Catel development team">
//     Copyright (c) 2008 - 2014 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Yis.Framework.Core.Caching.Policies
{
    using System;

    /// <summary>
    /// The cache item will expire using the duration to calculate the absolute expiration from now.
    /// </summary>
    public class DurationExpirationPolicy : AbsoluteExpirationPolicy
    {
        #region Constructors + Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DurationExpirationPolicy"/> class.
        /// </summary>
        /// <param name="durationTimeSpan">The expiration.</param>
        internal DurationExpirationPolicy(TimeSpan durationTimeSpan)
            : this(durationTimeSpan, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DurationExpirationPolicy"/> class.
        /// </summary>
        /// <param name="durationTimeSpan">The expiration.</param>
        /// <param name="canReset">        The can reset.</param>
        protected DurationExpirationPolicy(TimeSpan durationTimeSpan, bool canReset)
            : base(DateTime.Now.Add(durationTimeSpan), canReset)
        {
            DurationTimeSpan = durationTimeSpan;
        }

        #endregion Constructors + Destructors

        #region Properties

        /// <summary>
        /// Gets or sets the expiration.
        /// </summary>
        protected TimeSpan DurationTimeSpan { get; set; }

        #endregion Properties
    }
}