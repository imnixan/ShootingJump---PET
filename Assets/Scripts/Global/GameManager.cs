using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int Gun;

    [SerializeField]
    private GameObject[] guns;

    [SerializeField]
    ScoresCounter scoreCounter;

    [SerializeField]
    private CameraMover cameraMove;

    private void Start()
    {
        Gun = PlayerPrefs.GetInt("CurrentGun");
        cameraMove.Init(Instantiate(guns[Gun]).transform);
    }

    public void EndGame()
    {
        Debug.Log("EndGame");
    }
}
