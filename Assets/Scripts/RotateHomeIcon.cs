using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHomeIcon : MonoBehaviour
{
    public Transform _bigIcon;
    public Transform _smallIcon;

    float degreesPerSecond = 20;
    private void Update()
    {
        _bigIcon.Rotate(new Vector3(0, 0, degreesPerSecond) * Time.deltaTime);
        _smallIcon.Rotate(new Vector3(0, 0, -degreesPerSecond) * Time.deltaTime);
    }
}
