using System;
using UnityEngine;

namespace Enemy.Behaviours.Scripts
{   
    public abstract class EnemyAttackBehaviour : ScriptableObject
    {
        private Action<int> EnemyAttacked;

        private int damageResult;

      
        public abstract void EnemyAttack(Action<int> callback);
        
    }
}