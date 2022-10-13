using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringTrader.Simulator;

namespace TuringTraderWin.Algorithm
{
    /// <summary>
    /// The Iterface for the Algorithm Manager.
    /// </summary>
    public interface IAlgorithmManager
    {
        /// <summary>
        /// Gets or sets the List of available Algorithms to use.
        /// </summary>
        List<IAlgorithm> AlgorithmList { get; set; }

        /// <summary>
        /// Gets or sets the Selected Algorithm.
        /// </summary>
        IAlgorithm SelectedAlgorithm { get; set; }



        /// <summary>
        /// Reloads the Algorithms.
        /// </summary>
        public void LoadAlgorithms();

        /// <summary>
        /// Runs the Algorithm.
        /// </summary>
        /// <param name="algorithm">The algorithm to run, or defaults to the previously Selected Algorithm.</param>
        void RunAlgorithm(IAlgorithm algorithm);
    }
}
