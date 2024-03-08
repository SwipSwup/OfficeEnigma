using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace GamePlay.Player
{
    [RequireComponent(typeof(PlayerController), typeof(CharacterController), typeof(AudioSource))]
    [ExecuteAlways]
    public class PlayerCharacter : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private Transform playerBody;
        [SerializeField] private Transform playerCamera;
        [Header("Gravity")] [SerializeField] private Transform gravityCheckOrigin;
        [SerializeField] private float gravityCheckDistance;
        [SerializeField] private float gravityMultiplier = 1f;

        [Header("Movement & look")] [SerializeField]
        private float moveSpeedMultiplier = 1f;

        [SerializeField] private float lookSpeedMultiplier = 10f;

        [Header("Audio")] [SerializeField] private float pitchWobble = .05f;
        [SerializeField] private AudioClip[] footStepSounds;

        private CharacterController _characterController;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            _characterController = GetComponent<CharacterController>();

            _characterController.center = playerBody.position;
        }

        private void Update()
        {
            Move();
            Gravity();
        }

        //Not physically accurate but it does the job
        private void Gravity()
        {
#if UNITY_EDITOR
            Debug.DrawLine(gravityCheckOrigin.position,
                gravityCheckOrigin.position + Vector3.down * gravityCheckDistance, Color.cyan);
#endif
            if (Physics.Raycast(gravityCheckOrigin.position, Vector3.down * gravityCheckDistance, gravityCheckDistance,
                    1 << 8))
                return;

            _characterController.Move(gravityMultiplier * Time.deltaTime * Vector3.down);
        }

        public Vector3 NextMoveDirection
        {
            set => _nextMoveDirection = value;
        }

        private Vector3 _nextMoveDirection;

        private void Move()
        {
            if (_nextMoveDirection == Vector3.zero)
                return;

            Vector3 preparedMovementVector =
                _nextMoveDirection.x * playerBody.right + _nextMoveDirection.z * playerBody.forward;

            _characterController.Move(moveSpeedMultiplier * Time.deltaTime * preparedMovementVector);

            PlayFootstepSound();
        }

        private float verticalRotation = 0f;

        public void Look(Vector2 delta)
        {
            Vector2 preparedDelta = Time.deltaTime * lookSpeedMultiplier * delta;

            playerBody.Rotate(0f, preparedDelta.x, 0f);

            verticalRotation -= preparedDelta.y;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }

        private void PlayFootstepSound()
        {
            if(_audioSource.isPlaying)
                return;

            _audioSource.clip = footStepSounds[Random.Range(0, footStepSounds.Length)];
            _audioSource.pitch = Random.Range(1f - pitchWobble, 1f + pitchWobble);
            _audioSource.Play();
        }
    }
}