using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Vector3 offset;
    private float _smoothTime = 0.25f;
    private Vector3 _velocity = Vector3.zero;
    private float _zoom;
    private float _zoomMultipler = 4f;
    private float _minZoom = 2f;
    private float _maxZoom = 8f;
    private float _zoomVelocity = 0f;

    [SerializeField] private Transform _target;
    [SerializeField] private Camera _cam;

    private void Start()
    {
        _zoom = _cam.orthographicSize;
    }
    private void Update()
    {
        Vector3 targetPosition = _target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _zoom -= scroll * _zoomMultipler;
        _zoom = Mathf.Clamp(_zoom, _minZoom, _maxZoom);
        _cam.orthographicSize = Mathf.SmoothDamp(_cam.orthographicSize, _zoom, ref _zoomVelocity, _smoothTime);
    }
}
