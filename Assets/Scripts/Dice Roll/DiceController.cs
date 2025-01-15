using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceController : MonoBehaviour
{
    [SerializeField] private GameObject dicePrefab;
    List<Dice> dices = new List<Dice>();
    int totalvalue;
        
    private class Dice
    {
        int numberOfSides = 6;
        
        public int throwDice()
        {
            return Random.Range(1, numberOfSides + 1);
        }
    }

    public void CreateDices(int numberOfDices)
    {
        dices.Clear();
        for (int i = 0; i < numberOfDices; i++)
        {
            Dice dice = new Dice();
            Debug.Log($"Dice is created, totalDices {dices.Count + 1}");
            dices.Add(dice);
        }
    }
    
    public int throwDices()
    {
        if (dices.Count == 0)
        {
            return 0;
        }
        for (int i = 0; i < dices.Count; i++)
        {
            totalvalue += dices[i].throwDice();
        }
        return totalvalue;

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var value = 0;
            CreateDices(4);
            
            value = throwDices();
            Debug.Log(value);
        }
    }
}
