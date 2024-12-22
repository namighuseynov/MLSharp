using System.Text.Json;

namespace MLSharp.ReinforcementLearning
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class QAgent
    {
        #region Constructors
        public QAgent(List<Action> actions, List<Perceptor> perceptors, Configuration config)
        {
            _random = new Random();
            _actions = actions;
            _perceptors = perceptors;
            _configuration = config;

            if (File.Exists(config.BrainPath) && _continueLearning)
            {
                Load(config.BrainPath);
            }
            else
            {
                _qTable = new Dictionary<(string, int), double>();
            }
            ActionReceived += OnActionReceived;
            _takedAction = 0;
            _prevState = string.Empty;
        }
        #endregion

        #region Fields
        private Dictionary<(string, int), double> _qTable;
        private Random _random;
        private List<Action> _actions;
        private List<Perceptor> _perceptors;
        private event Action ActionReceived;
        private Configuration _configuration;
        private bool _learns = false;
        private bool _continueLearning = false;
        private int _takedAction;
        private string _prevState;
        #endregion

        #region Methods
        private void TakeAction()
        {
            _prevState = GetState();
            if (_learns && (_random.NextDouble() < _configuration.ExplorationRate))
            {
                int randomAction = _random.Next(_actions.Count);
                _takedAction = randomAction;
            }
            else
            {
                string state = GetState();
                int bestAction = GetBestAction(state);
                _takedAction = bestAction;
            }
            _actions[_takedAction].Invoke();
            ActionReceived?.Invoke();
        }

        protected string GetState()
        {
            string currentState = string.Empty;
            foreach (var perceptor in _perceptors)
            {
                currentState += perceptor.GetState();
            }
            return currentState;
        }

        public void Handle()
        {
            TakeAction();
        }

        private double GetQValue(string state, int action)
        {
            if (_qTable.ContainsKey((state, action)))
            {
                return _qTable[(state, action)];
            }
            return 0.0;
        }

        private void SetQValue(string state, int action, double qValue)
        {
            _qTable[(state, action)] = qValue;
        }

        private int GetBestAction(string state)
        {
            int bestAction = 0;
            double bestQValue = GetQValue(state, bestAction);

            for (int i = 0; i < _actions.Count; i++)
            {
                double currentQValue = GetQValue(state, i);
                if (bestAction < currentQValue)
                {
                    bestQValue = currentQValue;
                    bestAction = i;
                }
            }
            return bestAction;
        }

        private void UpdateQValues(double reward)
        {
            string state = _prevState;
            int action = _takedAction;
            string nextState = GetState();
            int nextAction = GetBestAction(nextState); ;

            double qValue = GetQValue(state, action);
            double nextQValue = GetQValue(nextState, nextAction);
            double newQValue = qValue + _configuration.LearningRate * (reward + _configuration.DiscountFactor * nextQValue - qValue);
            SetQValue(state, action, newQValue);
        }

        protected void SetReward(double reward)
        {
            UpdateQValues(reward);
        }

        public void Load(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DictionaryValueTupleConverter<double>());

            _qTable = JsonSerializer.Deserialize<Dictionary<(string, int), double>>(json, options)
                      ?? new Dictionary<(string, int), double>();
        }

        public void Save(string filePath)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DictionaryValueTupleConverter<double>());

            string json = JsonSerializer.Serialize(_qTable, options);
            File.WriteAllText(filePath, json);
        }

        #endregion

        #region Events
        public abstract void OnActionReceived();
        #endregion

        #region Properties
        public bool Learns { get { return _learns; } set { _learns = value; } }
        public bool ContinueLearning { get { return _continueLearning; } set { _continueLearning = value; } }
        #endregion
    }
}
