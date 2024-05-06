namespace IUP.ChildrenOfTheDeep
{
    public abstract class State
    {
        public virtual void OnEnter() { }

        public virtual void OnUpdate() { }

        public virtual void OnExit() { }
    }
}
