using UnityEngine;

namespace Strawberry.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/SongInfo", fileName = "SongInfo")]
    public class SongInfo : ScriptableObject
    {
        [Tooltip("Song to be played")]
        public AudioClip Song;
    }
}