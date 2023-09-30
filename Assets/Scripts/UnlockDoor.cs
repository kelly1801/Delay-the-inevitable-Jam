using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnlockDoor : MonoBehaviour
{
    private Collider[] switches;
    public int switchesActivated = 0;
    private bool isOpen = false;
    
    private float speed = 2.0f;
    private float verticalDistance = 10.0f;
    
    private Vector3 targetPosition;

    void Start()
    {
        // Door opening limit
        targetPosition = transform.position + new Vector3(0, verticalDistance, 0);

        Collider[] allColliders = GetComponentsInChildren<Collider>();
        switches = System.Array.FindAll(allColliders, collider => collider.isTrigger);
        foreach (Collider doorSwitch in switches) 
        {
            doorSwitch.gameObject.AddComponent<TriggerListener>(); // Add Listener to the switches
        }
    }
    private void Update()
    {
        if (switchesActivated == switches.Length && switchesActivated > 0) isOpen = true;
        if (isOpen)
        {
            SeparateChildren();
            OpenTheDoor();
        }

    }
    private void OpenTheDoor()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        if (transform.position == targetPosition) Destroy(gameObject);
    }

    private void SeparateChildren()
    {
        foreach (Collider doorSwitch in switches) 
        {
            doorSwitch.gameObject.transform.SetParent(null); 
        }
    }
}
