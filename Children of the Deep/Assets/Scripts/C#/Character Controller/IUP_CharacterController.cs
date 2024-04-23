using UnityEngine;

namespace IUP.ChildrenOfTheDeep
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class IUP_CharacterController : MonoBehaviour
    {
        [field: SerializeField] public float Gravity { get; private set; } = Physics.gravity.y;

        public Vector3 Velocity => _velocity;
        public bool IsGrounded => _characterController.isGrounded;

        private CharacterController _characterController;
        private Vector3 _velocity;

        public void SetHorizontalMovement(Vector2 speed)
        {
            _velocity = new Vector3(speed.x, Velocity.y, speed.y);
        }

        public void StopHorizontalMovement()
        {
            _velocity = new Vector3(0.0f, Velocity.y, 0.0f);
        }

        public void ApplyVerticalMovement(float velocity)
        {
            _velocity.y += velocity;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            ApplyGravity();
            ApplyMotion();
            ResetVerticalVelocityIfGrounded();
        }

        private void ApplyGravity()
        {
            _velocity.y += Gravity * Time.fixedDeltaTime;
        }

        private void ApplyMotion()
        {
            Vector3 motion = _velocity * Time.fixedDeltaTime;
            _characterController.Move(motion);
        }

        private void ResetVerticalVelocityIfGrounded()
        {
            if (IsGrounded)
            {
                _velocity.y = 0.0f;
            }
        }
    }
}
