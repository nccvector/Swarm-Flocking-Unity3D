using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMotion : MonoBehaviour
{
    public float speed = 0.1f;
    public float distance = 10f;

    // Update is called once per frame
    void Update()
    {
        speed = Mathf.Cos(Time.timeSinceLevelLoad * 0.2f) * 0.3f;
        distance = Mathf.Sin(Time.timeSinceLevelLoad * 0.5f) * 20f;
        transform.position = new Vector3(Mathf.Cos(Time.timeSinceLevelLoad * speed) * distance, 
        0f, Mathf.Sin(Time.timeSinceLevelLoad * speed) * distance);
    }
}
