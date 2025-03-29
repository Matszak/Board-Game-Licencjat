using System;
using System.Collections;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public bool isActive;
    private void OnMouseDown()
    {
        if (!isActive) return; 
        
        Debug.Log(gameObject.name);
    }
    
     
}
 