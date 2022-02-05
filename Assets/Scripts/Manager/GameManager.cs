using Strawberry.Data;
using Strawberry.SO;
using System.Linq;
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

        private NoteData[] _data;

        public NoteData[] SongData
        {
            get
            {
                if (_data == null)
                {
                    _data = _currentSong.SongData.text.Replace("\r", "").Split('\n')
                        .Select(x =>
                        {
                            var elems = x.Split(',');
                            return new NoteData
                            {
                                Timer = float.Parse(elems[0]),
                                Line = int.Parse(elems[1])
                            };
                        }).ToArray();
                }
                return _data;
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
