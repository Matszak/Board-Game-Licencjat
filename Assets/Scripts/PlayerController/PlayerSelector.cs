using System;
using System.Collections;
using Cards;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public bool isActive;
    
    private void OnEnable()
    {
        GameManager.Instance.OnSelectedCardOn += InstanceOnOnSelectedCardOn;
    }

    private void InstanceOnOnSelectedCardOn(Player player)
    {
        
        isActive = true;
    }

    private void OnMouseDown()
    {
        if (!isActive) return;
        if (gameObject.TryGetComponent(out PlayerController player))
        {
            GameManager.Instance.PlayerIsSelected(player);
        }
            
         

    }
 
    
    
     
}
 