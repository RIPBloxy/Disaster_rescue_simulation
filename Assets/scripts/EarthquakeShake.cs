using UnityEngine;
using System.Collections;

public class EarthquakeShake : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CameraShake());
        }
    }

    IEnumerator CameraShake()
    {
        Vector3 originalPos = transform.position;
        float duration = 2f;
        float strength = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-strength, strength);
            float y = Random.Range(-strength, strength);
            transform.position = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }
}