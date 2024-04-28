using UnityEngine;

namespace IUP.ChildrenOfTheDeep
{
    [RequireComponent(typeof(CharacterController))]
    [AddComponentMenu("IUP/IUP Character Controller")]
    public sealed class IUP_CharacterController : MonoBehaviour
    {
        [Header("Params:")]
        [SerializeField] private float _gravity = Physics.gravity.y;
        [SerializeField][Range(0.0f, 360.0f)] private float _rotationYOnAwake = 180.0f;

        public float Gravity => _gravity;
        public Vector3 Velocity => _velocity;
        public bool IsGrounded => _characterController.isGrounded;
        public bool HasHorizontalMovement => (Velocity.x != 0.0f) || (Velocity.z != 0.0f);
        public bool HasVerticalMovement => Velocity.y != 0.0f;
        public float RotationY { get; private set; }

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
            RotationY = _rotationYOnAwake;
        }

        private void FixedUpdate()
        {
            ApplyGravity();
            ApplyMotion();
            ResetVerticalVelocityIfGrounded();
            UpdateRotationY();
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

        private void UpdateRotationY()
        {
            if (HasHorizontalMovement)
            {
                float rotationY_Radians = Mathf.Atan2(Velocity.x, Velocity.z);
                RotationY = Mathf.Rad2Deg * rotationY_Radians;
                if (RotationY < 0.0f)
                {
                    RotationY = 360.0f + RotationY;
                }
            }
        }
    }
}
