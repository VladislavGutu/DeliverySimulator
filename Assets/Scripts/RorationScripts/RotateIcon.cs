using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIcon : MonoBehaviour
{
    public Transform _bigIcon;

    float degreesPerSecond = 20;
    private void Update()
    {
        _bigIcon.Rotate(new Vector3(0, 0, degreesPerSecond) * Time.deltaTime);
    }
}
