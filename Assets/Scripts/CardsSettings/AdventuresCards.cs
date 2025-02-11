using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventuresCards : MonoBehaviour
{
    [SerializeField] private GameObject card;

    [SerializeField] private List<GameObject> cards = new List<GameObject>();
    private void OnEnable()
    {
        AdventureTile.OnStep += Showcard;
    }

    private void OnDisable()
    {
        AdventureTile.OnStep -= Showcard;
    }

    void Showcard()
    {
        card.SetActive(true);
    }

    private void Start()
    {
        card.SetActive(false);
    }
}
