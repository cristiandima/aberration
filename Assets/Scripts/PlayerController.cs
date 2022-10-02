using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _collider;
    private SpriteRenderer _sprite;
    
    [SerializeField]
    private Canvas brainwashed;

    [SerializeField] private SpriteRenderer hp;
    
    private Vector2 _direction;

    [SerializeField]
    private float speed = 5;

    private bool _isOver;

    private AudioSource _sound;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _sound = GetComponent<AudioSource>();
        brainwashed.enabled = false;
        hp.enabled = false;
    }

    private bool _waitingForRestart;
    
    private void Update()
    {
        if (_isOver)
        {
            if (!_waitingForRestart)
            {
                if (_sound) _sound.Play();
                StartCoroutine(ReloadLevel());
                _waitingForRestart = true;
            }
            return;
        }
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.y = Input.GetAxisRaw("Vertical");
        _direction.Normalize();
        if (_direction.x < -0.1f)
        {
            _sprite.flipX = true;
        }
        if (_direction.x > 0.1f)
        {
            _sprite.flipX = false;
        }
        _rb.velocity = _direction * speed;
    }

    
    
    private float _focus = 1;
    private float _maxFocus = 1;
    private float _focusSpeed = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Terminal"))
        {
            speed = 0f;
        }
        if (!other.isTrigger || _isOver || other.gameObject.CompareTag("Terminal")) return;
        hp.transform.localScale = new Vector3(1, 0.1f, 1);
        hp.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.isTrigger || _isOver || other.gameObject.CompareTag("Terminal")) return;
        _focus -= _focusSpeed * Time.deltaTime;
        var hpScale = hp.transform.localScale;
        hpScale.x = _focus / _maxFocus;
        hp.transform.localScale = hpScale;
        if (_focus < 0)
        {
            brainwashed.enabled = true;
            _isOver = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.isTrigger || _isOver) return;
        hp.enabled = false;
        _focus = 1;
    }
    
    private IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
