﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiceSharp.Simulations
{
    /// <summary>
    /// Configuration for a <see cref="BaseSimulation"/>
    /// </summary>
    public class BaseConfiguration : ParameterSet
    {
        /// <summary>
        /// If true, the operating point calculation immediately skips to the GMIN stepping phase
        /// </summary>
        public bool NoOpIter = false;

        /// <summary>
        /// The GMIN parameter
        /// This is an extra conductance added in parallel to PN junction to improve convergence
        /// </summary>
        public double Gmin = 1e-12;

        /// <summary>
        /// The number of steps when using GMIN stepping to improve convergence
        /// </summary>
        public int NumGminSteps = 10;

        /// <summary>
        /// The number of steps when using SOURCE stepping to improve convergence
        /// </summary>
        public int NumSrcSteps = 10;

        /// <summary>
        /// Relative tolerance
        /// </summary>
        public double RelTol = 1e-3;

        /// <summary>
        /// Absolute tolerance on voltages
        /// </summary>
        public double VoltTol = 1e-6;

        /// <summary>
        /// Absolute tolerance
        /// </summary>
        public double AbsTol = 1e-12;

        /// <summary>
        /// Charge tolerance
        /// </summary>
        public double ChgTol = 1e-14;

        /// <summary>
        /// Number of iterations for DC simulation
        /// </summary>
        public int DcMaxIterations = 100;
    }
}
