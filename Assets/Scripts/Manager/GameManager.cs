using Strawberry.Data;
using Strawberry.SO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
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

        private List<NoteData> _data;

        public NoteData[] SongData
        {
            get
            {
                if (_data == null)
                {
                    _data = _currentSong.SongData.text.Replace("\r", "").Split('\n')
                        .Where(x => !string.IsNullOrWhiteSpace(x) && !x.TrimStart().StartsWith('#'))
                        .Select(x =>
                        {
                            var elems = x.Split(',');
                            return new NoteData
                            {
                                Timer = float.Parse(elems[0]),
                                Line = int.Parse(elems[1])
                            };
                        }).ToList();
                }
                return _data.ToArray();
            }
        }

        public void InitGenesis()
        {
            File.AppendAllText("editorData.csv", $"{Environment.NewLine}{DateTime.Now}{Environment.NewLine}");
        }

        public void AddNote(float timer, int line)
        {
            File.AppendAllText("editorData.csv", $"{timer},{line}{Environment.NewLine}");
            _data.Add(new()
            {
                Timer = timer,
                Line = line
            });
        }

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _source.clip = _currentSong.Song;
            Gamemode = Gamemode.Infinite;
        }
    }
}
