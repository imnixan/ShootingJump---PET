using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] private GameObject lamp;
    [SerializeField]
    private float minDelay = 0.1f; // Минимальная задержка между миганиями
    [SerializeField]
    private float maxDelay = 0.5f; // Максимальная задержка между миганиями

    private void Start()
    {
       
        StartCoroutine(Flicker());
    }

    private System.Collections.IEnumerator Flicker()
    {
        while (true) // Бесконечный цикл
        {
            lamp.SetActive(!lamp.activeSelf);
            float delay = Random.Range(minDelay, maxDelay);


            yield return new WaitForSeconds(delay);
        }
    }
}