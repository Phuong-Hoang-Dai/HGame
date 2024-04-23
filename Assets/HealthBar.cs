using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform Camera;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.position);
    }
}
