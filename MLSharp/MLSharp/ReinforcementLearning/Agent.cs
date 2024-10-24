namespace MLSharp.ReinforcementLearning
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Agent
    {
        #region Constructors
        public Agent(List<Action> actions, List<Perceptor> perceptors)
        {
            _qTable = new Dictionary<(string, int), double>();
            _actions = actions;
            _perceptors = perceptors;
            //_configuration = LoadConfiguration();
            ActionReceived += OnActionReceived;
        }
        #endregion

        #region Fields
        private Dictionary<(string, int), double> _qTable;
        private List<Action> _actions;
        private List<Perceptor> _perceptors;
        private event Action ActionReceived;
        private Configuration _configuration;
        #endregion

        #region Methods
        public void TakeAction()
        {
            ActionReceived?.Invoke();
        }

        public string GetState()
        {
            string currentState = string.Empty;
            foreach (var perceptor in _perceptors)
            {
                currentState += perceptor.GetState();
            }
            return currentState;
        }

        public void Start()
        {
            TakeAction();
        }

        //private Configuration LoadConfiguration(string path)
        //{
        //    return new Configuration();
        //}
        #endregion

        #region Events
        public abstract void OnActionReceived();
        #endregion
    }
}
