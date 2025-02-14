using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    
    public Vector3 offset;
    public Transform target;

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }


    private void Start()
    {
        GameManager.Instance.TurnStarted += OnCameraFollowed;
    }
        
    void OnDestroy()
    {
        GameManager.Instance.TurnStarted -= OnCameraFollowed;
    }

    public void OnCameraFollowed(GameManager.TurnStatedData data)
    {
        if (data.Player.PlayerObject != null)
        {
            Transform playerTransform = data.Player.PlayerObject.transform;
               
            _camera.Follow = playerTransform;
            _camera.LookAt = playerTransform;
       
 
        }
    }
  
}
