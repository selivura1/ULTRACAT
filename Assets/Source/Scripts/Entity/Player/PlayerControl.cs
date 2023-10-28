using UnityEngine;
using UnityEngine.InputSystem;
namespace Ultracat
{
    public class PlayerControl : MonoBehaviour
    {
        Movement _movement;
        Combat _combat;
        EntityBase _entity;
        Inventory _inventory;
        PlayerInput _input;
        Vector2 dir;
        public static bool enableIngameControls = true;
        public static PlayerControl Singleton;
        public static System.Action submitInput;
        public static System.Action cancelInput;
        private void Awake()
        {
            if (Singleton)
                Destroy(this);
        }
        private void Start()
        {
            _entity = GameManager.PlayerSpawner.GetPlayer();
            _movement = _entity.GetComponent<Movement>();
            _combat = _entity.GetComponent<Combat>();
            _inventory = _entity.GetComponent<Inventory>();
            _input = GetComponent<PlayerInput>();
        }
        public static Vector2 GetMouseDirection(Vector3 from)
        {
            var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return (mouse_pos - from).normalized;
        }
        public void MoveInput(InputAction.CallbackContext act)
        {
            dir = Vector2.zero;
            if (enableIngameControls)
                dir = new Vector2(act.ReadValue<Vector2>().x, act.ReadValue<Vector2>().y);
        }
        public void SubmitInput(InputAction.CallbackContext act)
        {
            if (act.performed)
                submitInput?.Invoke();
        }
        public void CancelInput(InputAction.CallbackContext act)
        {
            if (act.performed)
                cancelInput?.Invoke();
        }
        public void SkillInput(InputAction.CallbackContext act)
        {
            if (!enableIngameControls) return;
            _inventory.ActivateArtifact();
        }
        private void Update()
        {
            if (!enableIngameControls) return;
            if (_input.actions.FindAction("Fire").IsPressed())
            {
                _combat.StartAttack(GetMouseDirection(_combat.transform.position), _inventory.Weapon);
            }
            if (_input.actions.FindAction("AltFire").WasPressedThisFrame())
                _combat.StartCharging(GetMouseDirection(_combat.transform.position), _inventory.Weapon);
            if (_input.actions.FindAction("AltFire").WasReleasedThisFrame())
                _combat.StopCharging(GetMouseDirection(_combat.transform.position), _inventory.Weapon);
        }
        private void FixedUpdate()
        {
            _movement.Move(dir);
        }
    }
}