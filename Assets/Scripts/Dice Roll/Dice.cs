using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice  
{
    public int numberOfSides;
    
    public Dice(int numberOfSides)
    {
        this.numberOfSides = numberOfSides;
    }

    public int RollDice()
    {
        return Random.Range(1, numberOfSides + 1);
    }
}
