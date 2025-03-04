using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice  : MonoBehaviour
{
    public int numberOfSides;
    public GameObject dicePrefab;
    public bool isRollComplete { get; private set; }
    public Dice(int numberOfSides)
    {
        this.numberOfSides = numberOfSides;
    }

    public int RollDice()
    {
        isRollComplete = true;
        return Random.Range(1, numberOfSides + 1);
    }
}
