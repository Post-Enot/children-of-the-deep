using UnityEngine;

using InputContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace IUP.ChildrenOfTheDeep
{
    public sealed class PlayerControlState : PlayerInputState
    {
        public PlayerControlState(StateContext context) : base(context) { }

        public override void OnEnter()
        {
            SubscribeOnInputEvents();
        }

        public override void OnUpdate()
        {
            SetHorizontalMovement();
        }

        public override void OnExit()
        {
            UnsubscribeFromInputEvents();
            ResetTempState();
        }


        private void OnOpenInventoryPerformed(InputContext context)
        {
            PlayerController.OpenInventory();
        }

        private void OnAltMovementModePerformed(InputContext context)
        {
            PlayerController.EnableAltMovementMode();
        }

        private void AltMovementMode_canceled(InputContext context)
        {
            PlayerController.DisableAltMovementMode();
        }

        private void OnSwitchMovementModePerformed(InputContext context)
        {
            PlayerController.SwitchMovementMode();
        }

        private void SubscribeOnInputEvents()
        {
            InputActions.CharacterControl.SwitchMovementMode.performed += OnSwitchMovementModePerformed;
            InputActions.CharacterControl.AltMovementMode.performed += OnAltMovementModePerformed;
            InputActions.CharacterControl.AltMovementMode.canceled += AltMovementMode_canceled;
            InputActions.CharacterControl.OpenInventory.performed += OnOpenInventoryPerformed;
        }

        private void UnsubscribeFromInputEvents()
        {
            InputActions.CharacterControl.SwitchMovementMode.performed -= OnSwitchMovementModePerformed;
            InputActions.CharacterControl.AltMovementMode.performed -= OnAltMovementModePerformed;
            InputActions.CharacterControl.AltMovementMode.canceled -= AltMovementMode_canceled;
            InputActions.CharacterControl.OpenInventory.performed -= OnOpenInventoryPerformed;
        }

        private void ResetTempState()
        {
            PlayerController.SetHorizontalMovement(Vector2.zero);
            PlayerController.DisableAltMovementMode();
        }

        private void SetHorizontalMovement()
        {
            Vector2 inputDirection = InputActions.CharacterControl.Direction.ReadValue<Vector2>();
            PlayerController.SetHorizontalMovement(inputDirection);
        }
    }
}
