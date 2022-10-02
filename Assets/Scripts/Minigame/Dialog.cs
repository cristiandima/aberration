using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI startButton;
    public string[] sentences;
    private int _index;
    public float typingSpeed;

    public int secondPageAfter = 0;

    public AudioSource music;
    
    private AudioSource _sound;
    
    private void Start()
    {
        _sound = GetComponent<AudioSource>();
        startButton.enabled = false;
        StartCoroutine(Type());
    }

    private void Update()
    {
        if (_index == sentences.Length-1)
        {
            startButton.enabled = true;
        }
    }

    IEnumerator Type()
    {
        for(var i = 0; i < sentences.Length; i++)
        {
            if (secondPageAfter == i && secondPageAfter != 0)
            {
                yield return new WaitForSeconds(2);
                textDisplay.text = "";
                if (music) music.Play();
            }
            if (i != 0 && i != secondPageAfter)
            {
                textDisplay.text += "\n\n";    
            }
            textDisplay.text += "emily> ";
            yield return new WaitForSeconds(typingSpeed * 25);
            foreach (var letter in sentences[i].ToCharArray())
            {
                textDisplay.text += letter;
                if (_sound) _sound.Play();
                yield return new WaitForSeconds(typingSpeed);
            }
            _index = i;
        }
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
