using Strawberry.SO;
using UnityEngine;

namespace Strawberry.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Current song that need to be played")]
        private SongInfo _currentSong;

        private AudioSource _source;

        private Gamemode _gamemode;
        public Gamemode Gamemode
        {
            get => _gamemode;
            set
            {
                _gamemode = value;
                _source.Stop();
            }
        }

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _source.clip = _currentSong.Song;
            Gamemode = Gamemode.Infinite;
        }
    }
}
