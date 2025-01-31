using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] private GameObject flickeringObject;
    [SerializeField] private bool defState = true;
    [SerializeField]  private float flickerDuration = 0.1f; 
    [SerializeField] private float minDelayBetweenFlickers = 0.5f; 
    [SerializeField]  private float maxDelayBetweenFlickers = 2f; 

    private bool isFlickering = false;

    private void Start()
    {
        flickeringObject.SetActive(defState);
        StartCoroutine(FlickerRoutine());
    }

    private System.Collections.IEnumerator FlickerRoutine()
    {
        while (true)
        {
            float delay = Random.Range(minDelayBetweenFlickers, maxDelayBetweenFlickers);
            
            yield return new WaitForSeconds(delay);
            
            if (!isFlickering)
            {
                StartCoroutine(Flicker());
            }
        }
    }

    private System.Collections.IEnumerator Flicker()
    {
        isFlickering = true; 

  
        flickeringObject.SetActive(!defState);
        yield return new WaitForSeconds(flickerDuration); 
        flickeringObject.SetActive(defState); 

        isFlickering = false; 
    }
}