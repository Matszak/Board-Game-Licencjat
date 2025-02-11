using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    
    public Vector3 offset;
    public Transform target;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void Start()
    {
        GameManager.Instance.TurnStarted += OnCameraFollowed;
    }
    
    void OnDestroy()
    {
        GameManager.Instance.TurnStarted -= OnCameraFollowed;
    }

    public void OnCameraFollowed(GameManager.TurnStatedData data)
    {
        target = data.Player.PlayerObject.transform;
        StopAllCoroutines();
        StartCoroutine(SmothCameraFollowed());
    }

    private IEnumerator SmothCameraFollowed()
    {
        while (Vector3.Distance(transform.position, target.position + offset) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime);
            transform.LookAt(target);
            yield return null;
        }
    }
    
}
