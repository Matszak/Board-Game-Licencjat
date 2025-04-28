using System;
using UnityEngine;

namespace Enemy.Behaviours.Scripts
{
   
        [CreateAssetMenu(fileName = "Value", menuName = "Behaviours/Enemy/Value")]
        public class Value : EnemyAttackBehaviour
        {
            public int damage;
            public override void EnemyAttack(Action<int> callback)
            {
                callback?.Invoke(damage);
            }
        }
    }
 