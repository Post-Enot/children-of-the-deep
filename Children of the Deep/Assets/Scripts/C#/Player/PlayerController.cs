using UnityEngine;
using UnityEngine.Events;

namespace IUP.ChildrenOfTheDeep
{
    [RequireComponent(typeof(IUP_CharacterController))]
    public sealed class PlayerController : MonoBehaviour
    {
        [Header("Params:")]
        [SerializeField] private MovementMode _movementMode;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;


        [Header("Events:")]
        [SerializeField] private UnityEvent _inventoryOpened;

        public MovementMode MovementMode => _movementMode;
        public float WalkSpeed => _walkSpeed;
        public float RunSpeed => _runSpeed;
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
        public MovementMode ActualMovementMode
        {
            get
            {
                if ((MovementMode == MovementMode.Walk) ^ AltMovementModeEnabled)
                {
                    return MovementMode.Walk;
                }
                return MovementMode.Run;
            }
        }
        public bool AltMovementModeEnabled { get; private set; }

        private IUP_CharacterController _characterMovement;

        public void OpenInventory()
        {
            _inventoryOpened?.Invoke();
        }

        public void EnableAltMovementMode()
        {
            AltMovementModeEnabled = true;
        }

        public void DisableAltMovementMode()
        {
            AltMovementModeEnabled = false;
        }

        public void SwitchMovementMode()
        {
            _movementMode = MovementMode == MovementMode.Walk ? MovementMode.Run : MovementMode.Walk;
        }

        public void SetHorizontalMovement(Vector2 inputDirection)
        {
            Vector2 speed = inputDirection * ActualSpeed;
            _characterMovement.SetHorizontalMovement(speed);
        }

        private void Awake()
        {
            InitComponentLinks();
        }

        private void InitComponentLinks()
        {
            _characterMovement = GetComponent<IUP_CharacterController>();
        }
    }
}
