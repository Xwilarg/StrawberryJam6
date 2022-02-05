using Strawberry.SO;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Strawberry.Manager
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField]
        private BoardConfig _boardConfig;

        [SerializeField]
        [Tooltip("Board objects associated to the DFJK buttons")]
        private Image[] _hitMarks;

        // Color management of _hitMarks
        private Color _baseColor;

        private void Awake()
        {
            Assert.AreEqual(4, _hitMarks.Length);
        }

        private void Start()
        {
            _baseColor = _hitMarks[0].color;
            // Init horizontal lines

        }

        public void BoardButton1(InputAction.CallbackContext value)
        {
            BoardButton(value, 0);
        }

        public void BoardButton2(InputAction.CallbackContext value)
        {
            BoardButton(value, 1);
        }

        public void BoardButton3(InputAction.CallbackContext value)
        {
            BoardButton(value, 2);
        }

        public void BoardButton4(InputAction.CallbackContext value)
        {
            BoardButton(value, 3);
        }

        private void BoardButton(InputAction.CallbackContext value, int index)
        {
            if (value.phase == InputActionPhase.Started)
            {
                _hitMarks[index].color = _hitMarks[index].color - new Color(
                    _boardConfig.ColorIncrease,
                    _boardConfig.ColorIncrease,
                    _boardConfig.ColorIncrease
                );
            }
            else if (value.phase == InputActionPhase.Canceled)
            {
                _hitMarks[index].color = _baseColor;
            }
        }
    }
}
