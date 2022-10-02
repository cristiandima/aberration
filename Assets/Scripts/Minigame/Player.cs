using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _collider;
    private SpriteRenderer _sprite;
    private TrailRenderer _trail;
    private Light2D _light;
    private AudioSource _sound;
    public bool canMove;

    private Vector2 _direction;

    public enum MsgType
    {
        Sex,
        Power,
        Food,
        Aww,
        Outrage
    }

    [SerializeField]
    public MsgType msgType;

    public int msgIndex;
    
    [SerializeField]
    private float speed = 5;

    public Transform[] inputs;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _trail = GetComponent<TrailRenderer>();
        _light = GetComponent<Light2D>();
        _light.enabled = false;
        _sound = GetComponent<AudioSource>();
    }

    public void OnStartButton()
    {
        canMove = true;
        _light.enabled = true;
    }
    
    private Vector2 _inputDir; 
    private void Update()
    {
        if (!canMove) return;
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            _inputDir = new Vector2(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            _inputDir = new Vector2(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _inputDir = new Vector2(0, -1);
        }
        _rb.velocity = _inputDir * speed;
    }
    
    private bool _reset;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if ( _sound)
        {
            if (!col.gameObject.CompareTag("Receiver"))
            {
                _sound.Play();
            }
        }
        _reset = true;
        _rb.velocity = Vector2.zero;
        _inputDir = Vector2.zero;
        if (msgIndex < inputs.Length)
        {
            transform.position = inputs[msgIndex].position;
        }
    }

    private void LateUpdate()
    {
        if (_reset) _trail.Clear();
        _reset = false;
    }
}
