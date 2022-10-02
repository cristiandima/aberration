using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Terminal : MonoBehaviour
{

    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private Color _color;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        _color = _sprite.color;
        _sprite.color = Color.green;
        StartCoroutine(LoadLevelAfterDelayFlashing());
    }

    private IEnumerator LoadLevelAfterDelayFlashing()
    {
        for (var i = 0; i < 10; i++)
        {
            _sprite.color = i%2 == 0 ? Color.yellow : Color.green;
            yield return new WaitForSeconds(0.2f);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    private IEnumerator LoadLevelAfterDelay()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
