using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{   
 
    [SerializeField] private  GameObject  uiButtonPrefab;
    public int rollResult;
    private Player _player;
 
    private List<DiceRollConfig> _diceRollConfigs;
    public static event Action<int, Player> DiceRolled;


    public struct DiceRollConfig
    {
        public Dice diceType;
        public int numberOfDices;
    }
    
    public void RequestDiceRoll(Player player, List<DiceRollConfig> diceRollConfigs)
    {
        _diceRollConfigs = diceRollConfigs;
        _player = player;
        uiButtonPrefab.SetActive(true);
    }
 


    public void OnButtonClick()
    {
        
        StartCoroutine(RollDices());
        uiButtonPrefab.SetActive(false);
    }

    public IEnumerator RollDices()
    {
        rollResult = 0;
        List<Dice> diceObjects = new List<Dice>();

        foreach (var rollConfig in _diceRollConfigs)
        {
            for (int i = 0; i < rollConfig.numberOfDices; i++)
            {
                GameObject diceObject = Instantiate(rollConfig.diceType.dicePrefab, transform);
                Dice dice = diceObject.GetComponent<Dice>();
                diceObjects.Add(dice);
            }

        }


        foreach (var diceObject in diceObjects)
        {
            rollResult += diceObject.RollDice();
        }

        yield return new WaitUntil(() => diceObjects.TrueForAll((d => d.isRollComplete)));

        DiceRolled?.Invoke(rollResult, _player);
    }

    public void RequestDiceRoll(Player player, DiceRollConfig diceRollConfig)
    {
       
      
            _diceRollConfigs =  new List<DiceRollConfig> {diceRollConfig} ;
            _player = player;
            uiButtonPrefab.SetActive(true);
      
    }
}


