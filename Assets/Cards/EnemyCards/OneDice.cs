using System;
using UnityEngine;

namespace Cards.EnemyCards
{    [CreateAssetMenu(fileName = "OneDice", menuName = "Behaviours/Enemy/OneDice")]
    public class OneDice : EnemyBehaviour
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