using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollAnimation : MonoBehaviour
{
     
     [SerializeField] private TextMeshProUGUI textMesh;
    
    void Start()
    {
        DiceRoll.DiceRolled += PlayAnimation;
    }

    private void PlayAnimation(int obj)
    {
        //TODO Implement rol dice animation
        //textMesh.text = obj.ToString();
    }

 
}
