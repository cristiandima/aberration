using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    private Slider _slider;

    [SerializeField]
    private float notificationInterval = 5;

    [SerializeField] private UnityEvent onUnityEvent;
    
    private float _timeToNotification;

  
    // Start is called before the first frame update
    public void Start()
    {
        _slider = GetComponent<Slider>();
    }
    
    // Update is called once per frame
    public void Update()
    {
        _timeToNotification += Time.deltaTime;
        _slider.value = _timeToNotification/notificationInterval;
        if (_timeToNotification > notificationInterval)
        {
            onUnityEvent.Invoke();
            _timeToNotification = 0;
        }
    }
}
