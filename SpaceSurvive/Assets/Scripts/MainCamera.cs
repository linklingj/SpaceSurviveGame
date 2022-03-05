using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour 
{

    public IEnumerator Shake (float duration, float magnitude)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.position = new Vector3(x, y, -10);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(0, 0, -10);
    }

}
