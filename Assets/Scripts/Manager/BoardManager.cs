using Strawberry.SO;
using System.Collections.Generic;
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
        private RectTransform _boardCanvas;

        [SerializeField]
        private RectTransform _horLinesParent;

        [SerializeField]
        [Tooltip("Board objects associated to the DFJK buttons")]
        private Image[] _hitMarks;

        // Color management of _hitMarks
        private Color _baseColor;

        private readonly List<RectTransform> _horLines = new();

        private AudioSource _source;

        private void Awake()
        {
            Assert.AreEqual(4, _hitMarks.Length);
        }

        private void Start()
        {
            _source = GetComponent<AudioSource>();
            _baseColor = _hitMarks[0].color;

            InitBoard();
        }

        public void InitBoard()
        {
            _horLines.Clear();

            // Init horizontal lines
            for (int index = (int)_boardCanvas.sizeDelta.y; index > 0; index -= _boardConfig.DistanceHorLines)
            {
                var go = Instantiate(_boardConfig.HorLinePrefab, _horLinesParent);
                var rTransform = (RectTransform)go.transform;
                rTransform.anchoredPosition = new Vector2(0f, index);
                _horLines.Add(rTransform);
            }
        }

        private void Update()
        {
            // Clip not playing, nothing to do...
            if (!_source.isPlaying)
            {
                return;
            }
            // Update the horizontal lines so the player feel like the board is going toward him
            foreach (var line in _horLines)
            {
                var newY = line.anchoredPosition.y - Time.deltaTime * _boardConfig.FallingSpeed;
                if (newY < 0f) // Move the bar back at the end of the board
                {
                    newY += _boardCanvas.sizeDelta.y;
                }
                line.anchoredPosition = new Vector2(0f, newY);

                // Reduce size of the line when they get closer to the player so we still see them when they are far away
                line.sizeDelta = new Vector2(line.sizeDelta.x, 2f + newY / 100f);
            }
        }

        #region inputs
        public void BoardButton1(InputAction.CallbackContext value)
            => BoardButton(value, 0);

        public void BoardButton2(InputAction.CallbackContext value)
            => BoardButton(value, 1);

        public void BoardButton3(InputAction.CallbackContext value)
            => BoardButton(value, 2);

        public void BoardButton4(InputAction.CallbackContext value)
            => BoardButton(value, 3);

        private void BoardButton(InputAction.CallbackContext value, int index)
        {
            if (value.phase == InputActionPhase.Started)
            {
                _hitMarks[index].color = _hitMarks[index].color + new Color(
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
        #endregion
    }
}
