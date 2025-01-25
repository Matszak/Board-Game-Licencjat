using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollAnimation : MonoBehaviour
{
    [SerializeField] private DiceRoll diceRoll;
    void Start()
    {
        diceRoll.OnDiceRolled += PlayAnimation;
    }

    private void PlayAnimation(int obj)
    {
        for (int i = 0; i < obj; i++)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
