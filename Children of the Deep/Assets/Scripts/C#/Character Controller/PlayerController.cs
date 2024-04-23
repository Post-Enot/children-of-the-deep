using IUP.ChildrenOfTheDeep.Input;
using UnityEngine;

using InputContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace IUP.ChildrenOfTheDeep
{
    [RequireComponent(typeof(IUP_CharacterController))]
    public sealed class PlayerController : MonoBehaviour
    {
        [field: SerializeField] public MovementMode MovementMode { get; set; }
        [field: SerializeField] public float WalkSpeed { get; set; }
        [field: SerializeField] public float RunSpeed { get; set; }
        public float ActualSpeed
        {
            get
            {
                if ((MovementMode == MovementMode.Walk) ^ AltMovementModeEnabled)
                {
                    return WalkSpeed;
                }
                return RunSpeed;
            }
        }
        public bool AltMovementModeEnabled => _inputActions.CharacterControl.AltMovementMode.inProgress;

        private InputActions _inputActions;
        private IUP_CharacterController _characterMovement;

        public void EnableInput()
        {
            _inputActions.CharacterControl.Enable();
        }

        public void DisableInput()
        {
            _inputActions.CharacterControl.Disable();
        }

        private void Awake()
        {
            InitComponentLinks();
            SubscribeOnInputEvents();
        }

        private void Update()
        {
            HandleDirectionInput();
        }

        private void OnEnable()
        {
            EnableInput(); // TEMP.
        }

        private void OnDisable()
        {
            DisableInput(); // TEMP.
        }

        private void InitComponentLinks()
        {
            _inputActions = new InputActions();
            _characterMovement = GetComponent<IUP_CharacterController>();
        }

        private void SubscribeOnInputEvents()
        {
            _inputActions.CharacterControl.SwitchMovementMode.performed += HandleSwitchMovementModeInput;
        }

        private void HandleSwitchMovementModeInput(InputContext context)
        {
            SwitchMovementMode();
        }

        private void SwitchMovementMode()
        {
            MovementMode = MovementMode == MovementMode.Walk ? MovementMode.Run : MovementMode.Walk;
        }

        private void HandleDirectionInput()
        {
            Vector2 inputDirection = _inputActions.CharacterControl.Direction.ReadValue<Vector2>();
            Vector2 speed = inputDirection * ActualSpeed;
            _characterMovement.SetHorizontalMovement(speed);
        }
    }
}
