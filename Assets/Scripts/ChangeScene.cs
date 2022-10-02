using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //SceneManager loads your new Scene as an extra Scene (overlapping the other). This is Additive mode.
            SceneManager.LoadScene("Scenes/Minigame", LoadSceneMode.Additive);    
        }
    }
}
