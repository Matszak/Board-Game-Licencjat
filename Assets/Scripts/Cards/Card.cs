using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

 
public class Card : MonoBehaviour, ICard
{
    public GameObject cardPrefab;
    public Sprite CardImage;
    public TextMeshProUGUI NameText;  
    
    
    public void TriggerCard(Player player)
    {
       Debug.Log("Trigger Card");
       Debug.Log($"Im card {CardImage.name}, {NameText.text}");
    }
}
