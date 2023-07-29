using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameSpeedChanger : MonoBehaviour
{
    private const float MaxGameSpeed = 1;
    private const float SlowMoSpeed = 0.1f;
    private const float RestoreTime = 1.5f;
    private Sequence gameSpeedRestore;
    public float _currentGameSpeed;
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
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 120;
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
    }
}
