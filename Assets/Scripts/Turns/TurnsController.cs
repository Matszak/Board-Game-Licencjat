using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnsController : MonoBehaviour
{
    [SerializeField] private Button nextTurnButton;
    [SerializeField] private GameObject[] playableObjects;
    [SerializeField] private GameObject currentTurn;
    
    int currentTurnIndex = 0;
  
    void Start()
    {
        if (nextTurnButton != null)
        {
            nextTurnButton.onClick.AddListener(NextPlayerTurn);
        }
      
    }

    private void NextPlayerTurn()
    {
        currentTurn = playableObjects[currentTurnIndex];
        
  
        currentTurnIndex++;
        if (currentTurnIndex >= playableObjects.Length)
        {
            currentTurnIndex = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
           
         
         
    }

    bool NextTurn()
    {
        return true;
    }
}
