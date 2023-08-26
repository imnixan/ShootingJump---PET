using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class GameSpeedChanger : MonoBehaviour
{
    public static event UnityAction SlowMotion;

    public static float RestoreTime
    {
        get { return 3f; }
    }

    public float _currentGameSpeed;

    [SerializeField]
    private Light bgLight;

    private const float MaxGameSpeed = 1;
    private Sequence gameSpeedRestore;
    private const float SlowMoSpeed = 0.05f;
    private float CurrentGameSpeed
    {
        get { return _currentGameSpeed; }
        set
        {
            _currentGameSpeed = value;
            if (_currentGameSpeed > MaxGameSpeed)
            {
                _currentGameSpeed = MaxGameSpeed;
            }
            Time.timeScale = _currentGameSpeed;
            AudioManager.Pitch = _currentGameSpeed;
            bgLight.intensity = _currentGameSpeed;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 120;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        CurrentGameSpeed = MaxGameSpeed;
        Tween speedRestore = DOTween.To(
            () => CurrentGameSpeed,
            x => CurrentGameSpeed = x,
            1,
            RestoreTime
        );
        gameSpeedRestore = DOTween.Sequence().Append(speedRestore);
    }

    public void SlowTime()
    {
        CurrentGameSpeed = SlowMoSpeed;
        gameSpeedRestore.Restart();
        SlowMotion?.Invoke();
    }
}
