using UnityEngine;

namespace Cards.EnemyCards.DefeatBehaviours
{
    public abstract class EnemyDefeatedBehaviour : ScriptableObject
    {
        public abstract void EnemyDefeated();
        public string winText;
    }
}