using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraMover : MonoBehaviour
{
    private Transform playerTransform;
    private const float NormalFov = 60;
    private const float SlowMoFov = 110;
    private Transform cameraTransform;
    private Camera camera;

    private const float MinY = 0;
    private const float Speed = 1;
    private float zPos;
    private Vector3 _cameraFinalPos;

    public float _currentFov;

    private float CurrentFov
    {
        get { return _currentFov; }
        set
        {
            _currentFov = value;
            if (_currentFov < NormalFov)
            {
                _currentFov = NormalFov;
            }
            if (_currentFov > SlowMoFov)
            {
                _currentFov = SlowMoFov;
            }
        }
    }

    private Vector3 CameraFinalPos
    {
        get { return _cameraFinalPos; }
        set
        {
            _cameraFinalPos = value;
            _cameraFinalPos.z = zPos;
            if (_cameraFinalPos.y < 0)
            {
                _cameraFinalPos.y = 0;
            }
        }
    }

    public void Init(Transform player)
    {
        this.playerTransform = player;
        cameraTransform = transform;
        zPos = cameraTransform.position.z;
        camera = GetComponent<Camera>();
        CurrentFov = NormalFov;
        camera.fieldOfView = CurrentFov;
    }

    private void LateUpdate()
    {
        CameraFinalPos = playerTransform.position;

        cameraTransform.position = Vector3.MoveTowards(
            cameraTransform.position,
            CameraFinalPos,
            Speed
        );
    }

    private void FixedUpdate()
    {
        CurrentFov = Mathf.MoveTowards(CurrentFov, NormalFov, 0.25f);
        camera.fieldOfView = Mathf.MoveTowards(camera.fieldOfView, CurrentFov, 0.25f);
    }

    private void OnSlowMo()
    {
        CurrentFov = SlowMoFov;
    }

    private void OnEnable()
    {
        GameSpeedChanger.SlowMotion += OnSlowMo;
    }

    private void OnDisable()
    {
        GameSpeedChanger.SlowMotion -= OnSlowMo;
    }
}
