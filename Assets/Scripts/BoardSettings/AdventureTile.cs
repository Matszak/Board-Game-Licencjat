using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class AdventureTile : MonoBehaviour
{
    public void TileAction()
    {
        transform.DORotate(new Vector3(0, 0, 180), 0.5f, RotateMode.Fast).SetEase(Ease.Linear);
        Debug.Log("3");

    }
    public delegate void EnteredTile();

    public static event EnteredTile OnStep;
    

    /*private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            if (other.TryGetComponent<Rigidbody>(out Rigidbody rb))
            { 
                if (rb.velocity.magnitude == 0)
                { 
                    
                    if (OnStep != null)
                    {
                        OnStep();
                    }
                }
  
            }
   
        }
    }*/
}
