using System;
using Cards.EnemyCards;
using UnityEngine;

namespace Enemy.Behaviours.Scripts
{    [CreateAssetMenu(fileName = "OneDice", menuName = "Behaviours/Enemy/OneDice")]
    public class OneDice : EnemyAttackBehaviour
    {
        [SerializeField] int maxValue = 6;
        public override void EnemyAttack(Action<int> callback)
        {
            GameManager.Instance.diceRoll.RequestDiceRoll(true, i =>
            {
                callback?.Invoke(i);
            }, maxValue);
        }
    }
}