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
        [Header("Main config file")]

        [SerializeField]
        private BoardConfig _boardConfig;

        [Header("Objects on the scene")]

        [SerializeField]
        [Tooltip("Main canvas of the 3D board")]
        private RectTransform _boardCanvas;

        [SerializeField]
        [Tooltip("Parent of the horizontal lines")]
        private RectTransform _horLinesParent;

        [SerializeField]
        [Tooltip("4 lines corresponding to the hitmarks")]
        private RectTransform[] _lines;

        [SerializeField]
        [Tooltip("Board objects associated to the DFJK buttons")]
        private Image[] _hitMarks;

        // Color management of _hitMarks
        private Color _baseColor;

        private readonly List<RectTransform> _horLines = new();
        private readonly List<RectTransform> _notes = new();

        private AudioSource _source;

        private void Awake()
        {
            Assert.AreEqual(4, _hitMarks.Length);
            Assert.AreEqual(_hitMarks.Length, _lines.Length);
        }

        private void Start()
        {
            _source = GetComponent<AudioSource>();
            _baseColor = _hitMarks[0].color;

            InitBoard();
        }

        public void InitBoard()
        {
            foreach (var obj in _horLines) Destroy(obj.gameObject);
            foreach (var obj in _notes) Destroy(obj);

            _horLines.Clear();
            _notes.Clear();

            // Init horizontal lines
            for (int index = (int)_boardCanvas.sizeDelta.y; index > 0; index -= _boardConfig.DistanceHorLines)
            {
                var go = Instantiate(_boardConfig.HorLinePrefab, _horLinesParent);
                var rTransform = (RectTransform)go.transform;
                rTransform.anchoredPosition = Vector2.up * index;
                _horLines.Add(rTransform);
            }

            // Init notes
            foreach (var note in GetComponent<GameManager>().SongData)
            {
                var go = Instantiate(_boardConfig.NotePrefab, _lines[note.Line]);
                var rTransform = (RectTransform)go.transform;
                rTransform.anchoredPosition = Vector2.up * _boardConfig.FallingSpeed * note.Timer;
                _notes.Add(rTransform);
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
                line.anchoredPosition = Vector2.up * newY;

                // Reduce size of the line when they get closer to the player so we still see them when they are far away
                line.sizeDelta = new Vector2(line.sizeDelta.x, 2f + newY / 100f);
            }

            // Make notes fall down
            foreach (var note in _notes)
            {
                note.anchoredPosition = Vector2.up * (note.anchoredPosition.y - Time.deltaTime * _boardConfig.FallingSpeed);
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
