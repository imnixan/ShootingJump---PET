﻿using System.Collections;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class CameraMover : MonoBehaviour
{
    [SerializeField]
    private float NormalFov = 75;
    [SerializeField]
    private float SlowMoFov = 110;
    [SerializeField]
    private float MinY = 0;
    [SerializeField]
    private float Speed = 1;
    [SerializeField]
    private Vector3 _cameraFinalPos;
    [SerializeField]
    private float MaxVerticalAngle = 5f;
    [SerializeField]
    private float VerticalAngleSpeed = 100f;
    [SerializeField]
    private float FovChangeSpeedIn = 2f; // Скорость изменения FOV при входе в слоу-мо
    [SerializeField]
    private float FovChangeSpeedOut = 10f; // Скорость изменения FOV при выходе из слоу-мо
    [SerializeField]
    private float _currentFov;

    private Transform playerTransform;
    private Transform cameraTransform;
    private Camera camera;
    private float zPos;

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
        Vector3 targetPosition = new Vector3(playerTransform.position.x, cameraTransform.position.y, zPos);
        cameraTransform.position = Vector3.MoveTowards(
            cameraTransform.position,
            targetPosition,
            Speed * Time.deltaTime
        );

        float verticalOffset = playerTransform.position.y - cameraTransform.position.y;
        float targetAngle = Mathf.Clamp(-verticalOffset * 1f, -MaxVerticalAngle, MaxVerticalAngle);
        float currentAngle = cameraTransform.eulerAngles.x > 180 ? cameraTransform.eulerAngles.x - 360 : cameraTransform.eulerAngles.x;
        float newAngle = Mathf.MoveTowards(currentAngle, targetAngle, VerticalAngleSpeed * Time.deltaTime);
        cameraTransform.rotation = Quaternion.Euler(newAngle, 0, 0);

        float fovChangeSpeed = CurrentFov > camera.fieldOfView ? FovChangeSpeedIn : FovChangeSpeedOut;
        camera.fieldOfView = Mathf.MoveTowards(camera.fieldOfView, CurrentFov, fovChangeSpeed * Time.deltaTime);
    }

    private void OnSlowMoStart()
    {
        CurrentFov = SlowMoFov;
    }

    private void OnSlowMoEnd()
    {
        CurrentFov = NormalFov;
    }

    private void OnEnable()
    {
        GameSpeedChanger.SlowMotionStart += OnSlowMoStart;
        GameSpeedChanger.SlowMotionEnd += OnSlowMoEnd;
    }

    private void OnDisable()
    {
        GameSpeedChanger.SlowMotionStart -= OnSlowMoStart;
        GameSpeedChanger.SlowMotionEnd -= OnSlowMoEnd;
    }
}