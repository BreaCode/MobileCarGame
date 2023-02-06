using JoostenProductions;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputKeyboard : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 0.05f;


        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);


        private void Move()
        {
            Vector3 direction = CalcDirection();
            float moveValue = _speed * _inputMultiplier * Time.deltaTime * direction.x;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (sign > 0)
                OnRightMove(abs);
            else
                OnLeftMove(abs);
        }

        private void Jump()
        {
            //_jumpHeight;
        }

        private Vector3 CalcDirection()
        {
            const float normalizedMagnitude = 1;

            Vector3 direction = Vector3.zero;
            direction.x = Input.GetAxis("Horizontal");
            direction.z = Input.GetAxis("Vertical");

            if (direction.sqrMagnitude > normalizedMagnitude)
                direction.Normalize();
            if (direction.z > 0)
            {
                Jump();
            }
            return direction;
        }
    }
}
