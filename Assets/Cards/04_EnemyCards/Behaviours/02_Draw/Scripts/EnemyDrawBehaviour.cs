using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.EnemyCards.DrawBehaviour
{
    public abstract class EnemyDrawBehaviour : ScriptableObject
    {
        public abstract void EnemyDraw();
        public string drawText;
    }
}
