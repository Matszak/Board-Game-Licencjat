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
     
        {
            transform.DOMove(new Vector2(_originalPosition.x, _originalPosition.y + 150), 0.5f, true);
            transform.DOScale(3f, 0.5f);    
        }
    }

    // This method is called when the mouse exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;
        if (!_isDragging)
        {
            transform.DOMove(_originalPosition, 0.5f, true);
            transform.DOScale(1.5f, 0.5f);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Mouse clicked the card: {_card.nameText}");

        if (!_card.CanUse)
            return;
        _card.TriggerCard(_player);
        _player.playerCards.Remove(_card);
        DOTween.Kill(_card);
        Destroy(gameObject, 0.2f);
    }
    

      
}
