using System;
using Cards.EnemyCards;
using UnityEngine;

namespace Enemy.Behaviours.Scripts
{    [CreateAssetMenu(fileName = "OneDice", menuName = "Behaviours/Enemy/OneDice")]
    public class OneDice : EnemyAttackBehaviour
    {
        public override void EnemyAttack(Action<int> callback)
        {
            GameManager.Instance.diceRoll.RequestDiceRoll(true, i =>
            {
                callback?.Invoke(i);
            });
        }
    }
}