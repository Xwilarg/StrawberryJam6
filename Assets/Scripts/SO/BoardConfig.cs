using UnityEngine;

namespace Strawberry.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/BoardConfig", fileName = "BoardConfig")]
    public class BoardConfig : ScriptableObject
    {
        [Header("Board config")]
        [Tooltip("Distance between horizontal lines")]
        public int DistanceHorLines;

        [Tooltip("Increase of color when a key is pressed")]
        public float ColorIncrease;

        [Tooltip("Combo required before the counter is displayed on the board (inclusive)")]
        public int MinComboBeforeDisplay;

        [Header("Gameplay")]
        [Tooltip("Speed in which the notes are falling down")]
        public float FallingSpeed;

        public HitInfo[] HitInfo;

        [Header("Prefabs")]
        public GameObject HorLinePrefab;
        public GameObject NotePrefab;
    }
}