using UnityEngine;
using UnityEngine.InputSystem;

namespace GamePlay.Player
{
    [RequireComponent(typeof(PlayerCharacter), typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private PlayerCharacter _playerCharacter; 
        private InputActionMap _activeActionMap;

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerCharacter = GetComponent<PlayerCharacter>();
            
            SetupInput();
        }
        
        private void SetupInput()
        {
            _playerInput.actions.FindAction("Move").performed += OnMovePerformed;
            _playerInput.actions.FindAction("Move").canceled += OnMoveCanceled;
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            Vector3 vec = new Vector3(context.ReadValue<Vector2>().x, .0f, context.ReadValue<Vector2>().y);

            _playerCharacter.NextMoveDirection = vec;
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            Vector3 vec = new Vector3(context.ReadValue<Vector2>().x, .0f, context.ReadValue<Vector2>().y);

            _playerCharacter.NextMoveDirection = vec;
        }
    }
}
