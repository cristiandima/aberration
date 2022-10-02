using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscribeNpcs : MonoBehaviour
{
    private void Start()
    {
        var npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (var npc in npcs)
        {
        }
    }
}
