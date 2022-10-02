using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Npc : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int _currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    [SerializeField] private SpriteRenderer dangerArea;

    private bool _isConsuming;

    private CircleCollider2D _surveillanceArea;
    private SpriteRenderer _sprite;

    public SpriteRenderer visorSprite;
    
    private void Start()
    {
        visorSprite.enabled = false;
        _sprite = GetComponent<SpriteRenderer>();
        _surveillanceArea = GetComponent<CircleCollider2D>();
        _surveillanceArea.enabled = false;
        dangerArea.enabled = false;
    }
    
    private void Update()
    {
        if (_isConsuming)
        {
            return;
        }
        if (Vector2.Distance(waypoints[_currentWaypointIndex].transform.position, transform.position) < 0.01f)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= waypoints.Length)
            {
                _currentWaypointIndex = 0;
            }
        }

        _sprite.flipX = transform.position.x > waypoints[_currentWaypointIndex].transform.position.x;

        transform.position =
            Vector2.MoveTowards(transform.position, waypoints[_currentWaypointIndex].transform.position, speed * Time.deltaTime);
    }

    public void OnContent()
    {
        visorSprite.flipX = _sprite.flipX;
        if (_sprite.flipX)
        {
            visorSprite.transform.localPosition = new Vector3(-0.4f, 0.696f, 0);
        }
        else
        {
            visorSprite.transform.localPosition = new Vector3(0.4f, 0.696f, 0);
        }
        visorSprite.enabled = true;
        dangerArea.enabled = true;
        _surveillanceArea.enabled = true;
        _isConsuming = true;
    }

    public void OnContentDone()
    {
        visorSprite.enabled = false;
        dangerArea.enabled = false;
        _surveillanceArea.enabled = false;
        _isConsuming = false;
    }
}