using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{

    public float power = 0.7f;
    public float duration = 1.0f;
    public Transform mainCam;
    public float slowDownAmount = 1.0f;
    public bool shouldShake = false;

    private Vector3 startPosition;
    private float initialDuration;

    void start()
    {
        mainCam = Camera.main.transform;
        startPosition = mainCam.localPosition;
        initialDuration = duration;
    }


    void Update()
    {

        if (shouldShake)
        {
            if (duration > 0)
            {
                mainCam.localPosition = startPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                mainCam.localPosition = startPosition;
            }
        }

    }
}