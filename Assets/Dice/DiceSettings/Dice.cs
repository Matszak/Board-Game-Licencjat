using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice   
{
    private int _numberOfSides;

    public Dice(int numberOfSides)
    {
        _numberOfSides = numberOfSides;
    }

    public int RollDice()
    {
        return Random.Range(1, _numberOfSides + 1);
    }
}
