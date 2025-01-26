using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class AdventureTile : MonoBehaviour
{
    public delegate void EnteredTile();

    public static event EnteredTile OnStep;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.DORotate(new Vector3(0, 0, 180), 0.5f, RotateMode.Fast).SetEase(Ease.Linear);
            
            if (OnStep != null)
            {
                OnStep();
            }
        }
        
        
    }
}
