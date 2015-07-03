using System;
using Yis.Framework.Model;

namespace Yis.Erp.Mdm.Model
{
    /// <summary>
    /// Représentation d'un client
    /// </summary>
    public class Client : ModelBase
    {
        #region Properties

        /// <summary>
        /// Dénomination du client
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Identifiant technique
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identifiant métier
        /// </summary>
        public string Reference { get; set; }

        #endregion Properties
    }
}