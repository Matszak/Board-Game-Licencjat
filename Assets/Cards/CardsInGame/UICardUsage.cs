using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UICardUsage : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{

    private Card _card;
    public Vector2 targetPosition;
    
    private Canvas canvas;
    
    private bool _isDragging;
    private bool _isHovering;
    
    private Vector2 _originalPosition;
    
    [SerializeField] float hoverDuration = 10;
    public void Start()
    {
        _originalPosition = transform.position;
    }
    
    public void SetCard(Card card)
    {
        _card = card;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovering = true;
        if (!_isDragging)
        {
            transform.DOMove(new Vector2(_originalPosition.x, _originalPosition.y + 150f), hoverDuration, true);
        }
        Debug.Log($"Mouse is over the card: {_card.nameText}");
    }

    // This method is called when the mouse exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;
        if (!_isDragging)
        {
            transform.DOMove(_originalPosition, hoverDuration);
        }
        Debug.Log($"Mouse exited the card: {_card.nameText}");
    }
}
