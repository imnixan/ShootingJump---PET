using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    private Transform cameraTransform;

    private const float MinY = 0;
    private const float Speed = 1;
    private float zPos;
    private Vector3 _cameraFinalPos;
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

    private void Start()
    {
        cameraTransform = transform;
        zPos = cameraTransform.position.z;
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
}
