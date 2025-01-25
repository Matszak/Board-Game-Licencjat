using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollAnimation : MonoBehaviour
{
    [SerializeField] private DiceRoll diceRoll;
     [SerializeField] private TextMeshProUGUI textMesh;
    
    void Start()
    {
        diceRoll.OnDiceRolled += PlayAnimation;
    }

    private void PlayAnimation(int obj)
    {
        textMesh.text = obj.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
