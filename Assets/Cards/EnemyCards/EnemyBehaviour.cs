using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Cards.EnemyCards
{
      [CreateAssetMenu(fileName = "EnemyCard", menuName = "Behaviours/Enemy/TwoDices")]
    public class EnemyBehaviour : ScriptableObject
    {
        
        // Tutaj np tylko dane?
        // Enemy ma np 2 kosci
        // 1hp
        
        
     
        public int currentValue;
        public void OnEnable()
        {
         
        }

        public void EnemyAttack()
        {
            GameManager.Instance.diceRoll.RequestDiceRoll(true);       
        }
    }
}