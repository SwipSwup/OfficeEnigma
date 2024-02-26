using System;
using UnityEngine;

namespace GamePlay.Player
{
    [RequireComponent(typeof(PlayerController), typeof(CharacterController))]
    public class PlayerCharacter : MonoBehaviour
    {
        private Vector3 _nextMoveDirection;

        public Vector3 NextMoveDirection
        {
            set => _nextMoveDirection = value;
        }

        private CharacterController _characterController;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _characterController.Move(_nextMoveDirection);
        }
    }
}