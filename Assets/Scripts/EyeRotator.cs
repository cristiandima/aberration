using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;

public class EyeRotator : MonoBehaviour
{
    private float _origRotZ;

    private void Start()
    {
        _origRotZ = transform.rotation.eulerAngles.z;
    }
    
    [SerializeField]
    private float from = -45f;
    [SerializeField]
    private float to = 45f;
    [SerializeField]
    private float duration = 2;
    private void Update()
    {
        var value = Mathf.LerpAngle(from, to, Mathf.PingPong(Time.time / duration, 1));
        transform.localEulerAngles = new Vector3(0, 0, _origRotZ + value);
    }
}
