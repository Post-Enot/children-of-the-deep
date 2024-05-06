using UnityEngine;

namespace IUP.ChildrenOfTheDeep
{
    [RequireComponent(typeof(Animator))]
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private string _rotationY_ParamName;
        [SerializeField] private string _isMoveParamName;
        [SerializeField] private string _isRunParamName;

        [Header("Component References:")]
        [SerializeField] private IUP_CharacterController _characterController;
        [SerializeField] private PlayerController _playerController;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            UpdateRotationY();
            UpdateIsMove();
            UpdateIsRun();
        }

        private void UpdateRotationY()
        {
            _animator.SetFloat(_rotationY_ParamName, _characterController.RotationY);
        }

        private void UpdateIsMove()
        {
            _animator.SetBool(_isMoveParamName, _characterController.HasHorizontalMovement);
        }

        private void UpdateIsRun()
        {
            bool isRun = _playerController.ActualMovementMode == MovementMode.Run;
            _animator.SetBool(_isRunParamName, isRun);
        }
    }
}
