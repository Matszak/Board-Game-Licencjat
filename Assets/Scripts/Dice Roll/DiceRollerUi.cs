using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceRollerUI : MonoBehaviour
{
    public TMP_Text diceResultText; 
    public Button rollDiceButton;  

    private void Start()
    {
        rollDiceButton.onClick.AddListener(RollDice); 
    }

    private void RollDice()
    {
        int diceResult = Random.Range(1, 7); 
        Debug.Log(diceResult); 
        diceResultText.text = diceResult.ToString(); 
    }
}