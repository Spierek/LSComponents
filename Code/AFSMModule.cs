namespace LSTools
{
    // used in conjunction with an editor class to visualize current FSM state
    public abstract class AFSMModule : AModule
    {
        public FiniteStateMachine FSM { get; protected set; }
    }
}