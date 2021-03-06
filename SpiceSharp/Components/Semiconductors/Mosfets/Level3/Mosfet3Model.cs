﻿using SpiceSharp.Components.Mosfets.Level3;
using SpiceSharp.Entities;
using SpiceSharp.ParameterSets;
using System;

namespace SpiceSharp.Components
{
    /// <summary>
    /// A model for a <see cref="Mosfet3"/>
    /// </summary>
    public class Mosfet3Model : Entity<BindingContext>,
        IParameterized<ModelParameters>
    {
        /// <summary>
        /// Gets the parameter set.
        /// </summary>
        /// <value>
        /// The parameter set.
        /// </value>
        public ModelParameters Parameters { get; } = new ModelParameters();

        /// <summary>
        /// Initializes a new instance of the <see cref="Mosfet3Model"/> class.
        /// </summary>
        /// <param name="name">The name of the device.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <c>null</c>.</exception>
        public Mosfet3Model(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mosfet3Model"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="nmos">True for NMOS transistors, false for PMOS transistors.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <c>null</c>.</exception>
        public Mosfet3Model(string name, bool nmos)
            : base(name)
        {
            if (nmos)
                Parameters.SetNmos(true);
            else
                Parameters.SetPmos(true);
        }
    }
}
