using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [SerializeField]
    private float notificationDuration = 2;
    
    [SerializeField]
    private TextMeshProUGUI notificationText;

    [SerializeField]
    private Slider slider;

    private bool _active;
    private float _timeToExpiration;
    
    [SerializeField]
    private string[] notifications;
    private Animator _animator;

    [SerializeField]
    private UnityEvent contentStart;
    [SerializeField]
    private UnityEvent contentDone;
    
    private AudioSource _sound;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _sound = GetComponent<AudioSource>();
        
        var npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (var npc in npcs)
        {
            var npcComponent = npc.GetComponent<Npc>();
            contentStart.AddListener(npcComponent.OnContent);
            contentDone.AddListener(npcComponent.OnContentDone);
        }
    }

    private void Update()
    {
        if (!_active) return;
        
        _timeToExpiration += Time.deltaTime;
        slider.value = slider.maxValue - (_timeToExpiration/notificationDuration) * slider.maxValue;
        if (_timeToExpiration > notificationDuration)
        {
            _animator.Play("HideNotification");
            _timeToExpiration = 0;
            slider.value = slider.maxValue;
            _active = false;
            contentDone.Invoke();
        }
    }

    private int _notifIdx;
    
    public void OnNotificationInterval()
    {
        if (_sound) _sound.Play();
        notificationText.text = notifications[_notifIdx];
        _notifIdx++;
        if (_notifIdx >= notifications.Length)
        {
            _notifIdx = 0;
        }
        _animator.Play("ShowNotification");
        _active = true;
        contentStart.Invoke();
    }
}
