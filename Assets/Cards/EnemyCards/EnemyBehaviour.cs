using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Cards.EnemyCards
{   
    public abstract class EnemyBehaviour : ScriptableObject
    {
        private Action<int> EnemyAttacked;

        private int damageResult;

      
        public abstract void EnemyAttack(Action<int> callback);
        
    }
}