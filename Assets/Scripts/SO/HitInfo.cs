using System;
using UnityEngine;

namespace Strawberry.SO
{
    [Serializable]
    public class HitInfo
    {
        /// <summary>
        /// Max distance for this hit to be triggered
        /// </summary>
        public float MaxDistance;

        /// <summary>
        /// Color displayed when doing the hit
        /// </summary>
        public Color Color;

        /// <summary>
        /// Amount of point the hit does
        /// </summary>
        public int Score;
    }
}
