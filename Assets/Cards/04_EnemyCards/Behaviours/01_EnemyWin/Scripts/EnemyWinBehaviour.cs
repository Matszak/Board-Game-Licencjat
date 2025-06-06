using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.EnemyCards.WinBehaviour
{
    public abstract class EnemyWinBehaviour : ScriptableObject
    {
        public abstract void EnemyWin();
        public string loseText;
    }
}
