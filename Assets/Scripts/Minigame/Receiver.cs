using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Object = System.Object;

public class Receiver : MonoBehaviour
{
    private Light2D _light;
    
    public Player.MsgType receiverType;
    public Transform[] inputs;
    public Player.MsgType[] inputTypes;
    public Color[] colors;
    public Image winScreen;
    public TextMeshProUGUI winText;


    public Notification notification;
    
    private void Start()
    {
        if (winScreen && winText)
        {
            winScreen.enabled = false;
            winText.enabled = false;
        }
            
        _light = GetComponent<Light2D>();
    }

    private TrailRenderer _colTrail;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!_light.enabled) return;
        
        var player = col.gameObject.GetComponent<Player>();
        var colTransform = col.gameObject.GetComponent<Transform>();
        var colLight = col.gameObject.GetComponent<Light2D>();
        if (player.msgType == receiverType)
        {
            _light.enabled = false;
            player.msgIndex++;
            if (player.msgIndex >= inputs.Length)
            {
                // GAME WON
                colLight.enabled = false;
                player.canMove = false;
                winScreen.enabled = true;
                winText.enabled = true;
                StartCoroutine(LoadLevelAfterDelay());
                return;
            }
            colTransform.position = inputs[player.msgIndex].position;
            player.msgType = inputTypes[player.msgIndex];
            colLight.color = colors[player.msgIndex];
            if (notification is not null)
            {
                notification.OnNotificationInterval();    
            }
        }
        else
        {
            colTransform.position = inputs[player.msgIndex].position;
        }
        _colTrail = col.gameObject.GetComponent<TrailRenderer>();
    }

    private void LateUpdate()
    {
        if (_colTrail is null) return;
        _colTrail.Clear();
        _colTrail = null;
    }
    
    private IEnumerator LoadLevelAfterDelay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
