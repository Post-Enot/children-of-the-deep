using IUP.ChildrenOfTheDeep.Input;
using UnityEngine;

namespace IUP.ChildrenOfTheDeep
{
    [RequireComponent(typeof(PlayerController))]
    public sealed class PlayerInputStateGraph : MonoBehaviour
    {
        public void GoToControlState()
        {
            _stateGraph.TransitTo(_controlState);
        }

        public void GoToUncontrolState()
        {
            _stateGraph.TransitTo(_uncontrolState);
        }

        private PlayerControlState _controlState;
        private PlayerUncontrolState _uncontrolState;
        private StateGraph _stateGraph;
        private InputActions _inputActions;

        private void Awake()
        {
            InitStates();
            _stateGraph = new StateGraph(_controlState);
        }

        private void Update()
        {
            _stateGraph.Update();
        }

        private void OnEnable()
        {
            _inputActions?.Enable();
        }

        private void OnDisable()
        {
            _inputActions?.Disable();
        }

        private void InitStates()
        {
            PlayerInputState.StateContext context = CreateStateContext();
            _controlState = new PlayerControlState(context);
            _uncontrolState = new PlayerUncontrolState(context);
        }

        private PlayerInputState.StateContext CreateStateContext()
        {
            _inputActions ??= new InputActions();
            PlayerController playerController = GetComponent<PlayerController>();
            return new PlayerInputState.StateContext(_inputActions, playerController);
        }
    }
}
