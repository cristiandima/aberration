using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentConsumer : MonoBehaviour
{
    public void OnNotificationInterval()
    {
        Debug.Log("Called to consume");
    }
}
