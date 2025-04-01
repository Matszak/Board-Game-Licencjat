using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UICardUsage : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private Card _card;
    private Player _player;
    public Vector2 targetPosition;
    
    private Canvas canvas;
    private RectTransform rectTransform;
    
    private bool _isDragging;
    private bool _isHovering;
    
    private Vector2 _originalPosition;

    
    public void Start()
    {
        _originalPosition = transform.position;
    }


    public void SetCard(Card card, Player player)
    {
        _card = card;
        _player = player;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovering = true;
        if (!_isDragging)
        {
            transform.DOMove(new Vector2(_originalPosition.x, _originalPosition.y + 100f), 0.5f, true);
        }
        Debug.Log($"Mouse is over the card: {_card.nameText}");
    }

    // This method is called when the mouse exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;
        if (!_isDragging)
        {
            transform.DOMove(_originalPosition, 0.5f, true);
        }

        Debug.Log($"Mouse exited the card: {_card.nameText}");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Mouse clicked the card: {_card.nameText}");
        _card.TriggerCard(_player);
        _player.playerCards.Remove(_card);
        DOTween.Kill(_card);
        Destroy(this.gameObject);
    }
    

    
    public void CardUsed()
    {
        Destroy(this.gameObject);
    }
}
