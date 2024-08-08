using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    public Light pointLight;
    public bool isIncreasing = true;
    public float currentRange;
    public float timer = 0.0f;
    private float blinkDuration = 2.0f;
    void Start()
    {
        currentRange = 0f;
        pointLight.range = currentRange;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkDuration)
        {
            timer = 0f;
            isIncreasing = !isIncreasing;
        }
        if (timer <= blinkDuration)
        {
            if (isIncreasing)
            {
                currentRange = Mathf.Lerp(0f, 50f, timer / blinkDuration);
            }
            else
            {
                currentRange = Mathf.Lerp(50f, 0f, timer / blinkDuration);
            }

            pointLight.range = currentRange;

        }
    }
}
