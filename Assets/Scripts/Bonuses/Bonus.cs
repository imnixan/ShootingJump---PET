using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private Dictionary<int, string> bonusModels = new Dictionary<int, string>()
    {
        { 0, "Prefabs/PistolMagazin" },
        { 1, "Prefabs/RevolverDrum" },
        { 2, "Prefabs/UziMagazin" },
        { 3, "Prefabs/M4Magazin" },
        { 4, "Prefabs/AKMagazin" }
    };

    private Transform model;

    private void Start()
    {
        gameObject.tag = "Ammo";
        int gunType = PlayerPrefs.GetInt("CurrentGun");
        model = Instantiate(Resources.Load<GameObject>(bonusModels[gunType]), transform).transform;
    }

    private void FixedUpdate()
    {
        model.Rotate(0, 1f, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            Destroy(gameObject);
        }
    }
}
