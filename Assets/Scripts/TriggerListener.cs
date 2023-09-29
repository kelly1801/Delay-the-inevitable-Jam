using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerListener : MonoBehaviour
{
    private UnlockDoor door;

    private void Start()
    {
        door = transform.parent.GetComponent<UnlockDoor>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            door.switchesActivated++; 
        }
    }
}
