namespace IUP.ChildrenOfTheDeep
{
    public class StateGraph
    {
        public StateGraph(State state)
        {
            CurrentState = state;
            CurrentState.OnEnter();
        }

        public State CurrentState { get; private set; }

        public void TransitTo(State state)
        {
            CurrentState.OnExit();
            CurrentState = state;
            CurrentState.OnEnter();
        }

        public void Update()
        {
            CurrentState.OnUpdate();
        }
    }
}
