using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSpeedChanger : MonoBehaviour
{
    public static event UnityAction SlowMotionStart;
    public static event UnityAction SlowMotionEnd;

    [SerializeField]
    private bool slowMode = false;

    public static float RestoreTime
    {
        get { return 0.5f; }
    }

    public float _currentGameSpeed;

    private const float MaxGameSpeed = 1;
    private const float SlowMoSpeed = 0.05f;
    [SerializeField]
    private  float SpeedChangeRateIn = 2f; 
    [SerializeField]
    private  float SpeedChangeRateOut = 10f; 

    private float targetGameSpeed;

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
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 120;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        CurrentGameSpeed = MaxGameSpeed;
        targetGameSpeed = MaxGameSpeed;
    }

    private void Update()
    {
        float speedChangeRate = CurrentGameSpeed > targetGameSpeed ? SpeedChangeRateIn : SpeedChangeRateOut;
        CurrentGameSpeed = Mathf.MoveTowards(CurrentGameSpeed, targetGameSpeed, speedChangeRate * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!slowMode)
            {
                StartSlowTime();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (slowMode)
            {
                StopSlowTime();
            }
        }
    }

    public void StartSlowTime()
    {
        slowMode = true;
        targetGameSpeed = SlowMoSpeed;
        SlowMotionStart?.Invoke();
    }

    public void StopSlowTime()
    {
        slowMode = false;
        targetGameSpeed = MaxGameSpeed;
        SlowMotionEnd?.Invoke();
    }
} 