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


    private void OnEnable()
    {
        GameManager.Instance.TurnStarted += OnCameraFollowed;
    }
        
    void OnDestroy()
    {
        GameManager.Instance.TurnStarted -= OnCameraFollowed;
    }

    public void OnCameraFollowed()
    {
        if (Player.CurrentPlayer.PlayerObject != null)
        {
            Transform playerTransform = Player.CurrentPlayer.PlayerObject.transform;
               
            _camera.Follow = playerTransform;
            _camera.LookAt = playerTransform;
 
        }
    }
  
}
