using System;
using UnityEngine;

namespace Cards.EnemyCards
{
    [CreateAssetMenu(fileName = "TwoDicesEnemyBehaviour", menuName = "Behaviours/Enemy/TwoDices")]
    public class TwoDices : EnemyBehaviour
    {
        public override void EnemyAttack(Action<int> callback)
        {
            int damageResult = 0;

            GameManager.Instance.diceRoll.RequestDiceRoll(true, i =>
            {
                damageResult += i;

                GameManager.Instance.diceRoll.RequestDiceRoll(true, i1 =>
                {
                    damageResult += i1;

                    callback?.Invoke(damageResult);
                });
            });
        }
    }
}