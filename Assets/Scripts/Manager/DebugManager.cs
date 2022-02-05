using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Strawberry.Manager
{
    public class DebugManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _currentGamemode;

        private AudioSource _source;
        private GameManager _gm;
        private BoardManager _bm;

        private void Start()
        {
            _source = GetComponent<AudioSource>();
            _gm = GetComponent<GameManager>();
            _bm = GetComponent<BoardManager>();
        }

        private void EditGamemodeText()
        {
            if (_currentGamemode != null)
            {
                _currentGamemode.text = $"Current mode: {_gm.Gamemode}";
            }
        }

        public void ChangeGamemode(InputAction.CallbackContext value)
        {
            if (_gm.Gamemode == Gamemode.Infinite)
            {
                _gm.Gamemode = Gamemode.Creator;
            }
            else
            {
                _gm.Gamemode = Gamemode.Infinite;
            }
            EditGamemodeText();
            _bm.InitBoard();
        }

        public void Play(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Started && !_source.isPlaying)
            {
                _source.Play();
            }
        }

        public void Pause(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Started && _source.isPlaying)
            {
                _source.Pause();
            }
        }
    }
}
