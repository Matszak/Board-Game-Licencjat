using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICardUsage : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private Card _card;
    private Player _player;

    public void SetCard(Card card, Player player)
    {
        _card = card;
        _player = player;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Mouse clicked the card: {_card.nameText}");
        _card.TriggerCard(_player);
        _player.playerCards.Remove(_card);
        Destroy(this.gameObject);
    }
}
