﻿using System.Numerics;
using SpiceSharp.Behaviors;
using SpiceSharp.Parameters;
using SpiceSharp.Circuits;
using SpiceSharp.Sparse;

namespace SpiceSharp.Components.ComponentBehaviors
{
    /// <summary>
    /// AC behaviour for <see cref="CurrentControlledCurrentsource"/>
    /// </summary>
    public class CurrentControlledCurrentsourceAcBehavior : CircuitObjectBehaviorAcLoad
    {
        /// <summary>
        /// Parameters
        /// </summary>
        [SpiceName("gain"), SpiceInfo("Gain of the source")]
        public Parameter CCCScoeff { get; } = new Parameter();

        [SpiceName("i"), SpiceInfo("CCCS output current")]
        public Complex GetCurrent(Circuit ckt) => new Complex(ckt.State.Solution[CCCScontBranch], ckt.State.iSolution[CCCScontBranch]) * CCCScoeff.Value;
        [SpiceName("v"), SpiceInfo("CCCS voltage at output")]
        public Complex GetVoltage(Circuit ckt) => new Complex(
            ckt.State.Solution[CCCSposNode] - ckt.State.Solution[CCCSnegNode],
            ckt.State.iSolution[CCCSposNode] - ckt.State.iSolution[CCCSnegNode]);
        [SpiceName("p"), SpiceInfo("CCCS power")]
        public Complex GetPower(Circuit ckt)
        {
            Complex current = new Complex(ckt.State.Solution[CCCScontBranch], ckt.State.iSolution[CCCScontBranch]) * CCCScoeff.Value;
            Complex voltage = new Complex(ckt.State.Solution[CCCSposNode] - ckt.State.Solution[CCCSnegNode], ckt.State.iSolution[CCCSposNode] - ckt.State.iSolution[CCCSnegNode]);
            return current * voltage;
        }

        /// <summary>
        /// Private variables
        /// </summary>
        private MatrixElement CCCSposContBrptr = null;
        private MatrixElement CCCSnegContBrptr = null;
        private int CCCScontBranch;
        private int CCCSposNode;
        private int CCCSnegNode;
        
        /// <summary>
        /// Setup the behavior
        /// </summary>
        /// <param name="component">Component</param>
        /// <param name="ckt">Circuit</param>
        public override void Setup(CircuitObject component, Circuit ckt)
        {
            var cccs = component as CurrentControlledCurrentsource;
            var matrix = ckt.State.Matrix;

            CCCSposNode = cccs.CCCSposNode;
            CCCSnegNode = cccs.CCCSnegNode;
            CCCScontBranch = cccs.CCCScontBranch;
            CCCSposContBrptr = matrix.GetElement(cccs.CCCSposNode, cccs.CCCScontBranch);
            CCCSnegContBrptr = matrix.GetElement(cccs.CCCSnegNode, cccs.CCCScontBranch);
        }

        /// <summary>
        /// Unsetup the behavior
        /// </summary>
        public override void Unsetup()
        {
            // Remove references
            CCCSposContBrptr = null;
            CCCSnegContBrptr = null;
        }

        /// <summary>
        /// Execute behaviour
        /// </summary>
        /// <param name="ckt"></param>
        public override void Load(Circuit ckt)
        {
            CCCSposContBrptr.Add(CCCScoeff.Value);
            CCCSnegContBrptr.Sub(CCCScoeff.Value);
        }
    }
}
