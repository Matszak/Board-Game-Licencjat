using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICardUsage : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{

    private Card _card;

    public void SetCard(Card card)
    {
        _card = card;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Mouse is over the card: {_card.nameText}");
    }

    // This method is called when the mouse exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Mouse exited the card: {_card.nameText}");
    }
}
