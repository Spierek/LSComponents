namespace LSTools
{
    // used in conjunction with an editor class to visualize current FSM state
    public abstract class AFSMModule : AModule
    {
        public FiniteStateMachine FSM { get; protected set; }

        protected override void HandleInitialization()
        {
            FSM = new FiniteStateMachine();
        }

        protected virtual void Update()
        {
            if (FSM != null)
            {
                FSM.Update();
            }
        }

        protected override void HandleUninitialization()
        {
            if (FSM != null)
            {
                FSM.Reset();
            }
        }

        protected void AddState(AState state)
        {
            FSM.AddState(state);
        }
    }
}