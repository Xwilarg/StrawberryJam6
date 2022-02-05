using UnityEngine;

namespace Strawberry.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/BoardConfig", fileName = "BoardConfig")]
    public class BoardConfig : ScriptableObject
    {
        [Header("Config")]
        [Tooltip("Distance between horizontal lines")]
        public int DistanceHorLines;

        [Tooltip("Increase of color when a key is pressed")]
        public float ColorIncrease;

        [Header("Prefabs")]
        public GameObject HorLinePrefab;
    }
}