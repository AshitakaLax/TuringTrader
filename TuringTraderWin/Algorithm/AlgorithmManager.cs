using AshitakalaxAlgos;
using Microsoft.Extensions.Logging;
using TuringTrader.Simulator;
using TuringTraderWin.SampleAlgorithms;

namespace TuringTraderWin.Algorithm
{
    /// <summary>
    /// This handles the loading of all the different Algorithms
    /// </summary>
    public class AlgorithmManager : IAlgorithmManager
    {
        private readonly ILogger Logger;

        /// <summary>
        /// Creates the Algorthim Manager.
        /// </summary>
        public AlgorithmManager(ILogger<AlgorithmManager> logger)
        {
            Logger = logger;

        }

        /// <summary>
        /// Gets or sets the algorithms available to run.
        /// </summary>
        public List<IAlgorithm> AlgorithmList { get; set; } = new List<IAlgorithm>();

        /// <summary>
        /// Gets or sets the Selected Algorithm to use.
        /// </summary>
        public IAlgorithm SelectedAlgorithm { get; set; }

        /// <inheritdoc />
        public void LoadAlgorithms()
        {
            //We can enhance this, but the idea is to have a simple creation of the algo's.
            AlgorithmList.Add(new SimpleMovingAverage());
            AlgorithmList.Add(new TripleMovingAverage());
    }

        /// <summary>
        /// This runs the algorithm.
        /// </summary>
        public void RunAlgorithm(IAlgorithm algorithm)
        {
            if (algorithm == null && SelectedAlgorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm), "You must select an algorithm, or pass one in.");
            }
            else if (algorithm == null)
            {
                algorithm = SelectedAlgorithm;
            }

            //setup to run algorithm.
        }
    }
}
