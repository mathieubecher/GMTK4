using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class Controller : MonoBehaviour
    {
        private PlayerInput m_playerInput;
        private Camera m_mainCameraRef;
        public Vector2 moveDirection { get; set; }

        private Vector2 m_targetDirection;
        public Vector2 targetDirection
        {
            get
            {
                if (m_playerInput.currentControlScheme == "Keyboard&Mouse")
                {
                    Vector3 mousePos = m_mainCameraRef.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    Vector3 direction = (mousePos - transform.position);
                    direction.z = 0f;
                    direction.Normalize();
                    m_targetDirection = direction;
                    return direction;
                }
                return m_targetDirection;
            }
        }
        
        public delegate void Shoot();
        public event Shoot OnShoot;

        private void Awake()
        {
            m_playerInput = GetComponent<PlayerInput>();
            m_mainCameraRef = Camera.main;
        }

        public void ReadEscape()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        public void ReadMoveDirection(InputAction.CallbackContext _context)
        {
            moveDirection = _context.ReadValue<Vector2>();  
            Debug.Log(moveDirection);
        }

        public void ReadTargetDirection(InputAction.CallbackContext _context)
        {
            Vector2 targetDirection = _context.ReadValue<Vector2>();
            if (targetDirection.magnitude < 0.3f)
                return;
            
            m_targetDirection = targetDirection;
        }

        public void ReadShootAction(InputAction.CallbackContext _context)
        {
            if (_context.performed)
                OnShoot?.Invoke();
        }
    }
}
