using IUP.ChildrenOfTheDeep.Input;

namespace IUP.ChildrenOfTheDeep
{
    public class PlayerInputState : State
    {
        public readonly struct StateContext
        {
            public StateContext(InputActions inputActions, PlayerController playerController)
            {
                InputActions = inputActions;
                PlayerController = playerController;
            }

            public readonly InputActions InputActions { get; }
            public readonly PlayerController PlayerController { get; }
        }

        public PlayerInputState(StateContext context)
        {
            Context = context;
        }

        public StateContext Context { get; }
        public PlayerController PlayerController => Context.PlayerController;
        public InputActions InputActions => Context.InputActions;
    }
}
