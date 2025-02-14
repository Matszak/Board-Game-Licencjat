using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventuresCards : MonoBehaviour
{
    [SerializeField] private GameObject card;

    //[SerializeField] private List<GameObject> cards = new List<GameObject>();
    
    
    private void Start()
    {
        card.SetActive(false);
        GameManager.Instance.OnCardTriggered += ShowCard;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCardTriggered -= ShowCard;
    }

    private void ShowCard(Player player)
    {
        
        Debug.Log("ShowCard");
        card.SetActive(true);
    }
 
}
