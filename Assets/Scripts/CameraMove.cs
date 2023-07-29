using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    private const float NormalFov = 60;
    private const float SlowMoFov = 110;
    private Transform cameraTransform;
    private Camera camera;

    private const float MinY = 0;
    private const float Speed = 1;
    private float zPos;
    private Vector3 _cameraFinalPos;
    private Sequence fishEyeAnim;
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
        camera = GetComponent<Camera>();
        CreateFishEyeAnim();
    }

    private void CreateFishEyeAnim()
    {
        fishEyeAnim = DOTween.Sequence();

        fishEyeAnim.Append(camera.DOFieldOfView(SlowMoFov, GameSpeedChanger.RestoreTime / 2));
        fishEyeAnim.Append(camera.DOFieldOfView(NormalFov, GameSpeedChanger.RestoreTime / 2));
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

    private void OnSlowMo()
    {
        fishEyeAnim.Restart();
    }

    private void OnEnable()
    {
        GameSpeedChanger.SlowMotion += OnSlowMo;
    }

    private void Disable()
    {
        GameSpeedChanger.SlowMotion -= OnSlowMo;
    }
}
