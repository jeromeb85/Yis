﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Core.Messaging
{
    /// <summary>
    /// Détermine les options de threading lors de l'exécution du callback
    /// </summary>
    public enum ThreadingStrategy
    {
        /// <summary>
        /// le callback est déclenché sur le même thread que celui qui a publié l'événement
        /// </summary>
        Publisher,

        /// <summary>
        /// le callback est déclenché sur un thread d'arrière plan
        /// </summary>
        Background,

        /// <summary>
        /// le callback est déclenché sur le thread de présentation
        /// </summary>
        UI
    }
}