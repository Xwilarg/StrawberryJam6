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

        private void Start()
        {
            _source = GetComponent<AudioSource>();
        }
    }
}
