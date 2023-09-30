using UnityEngine;

public class TriggerListener : MonoBehaviour
{
    private UnlockDoor door;
    private Transform starter; // Red part of the button
    private Vector3 buttonHeight = new Vector3(0, 0.2f, 0);

    private void Start()
    {
        door = transform.parent.GetComponent<UnlockDoor>();
        starter = transform.Find("Starter");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            door.switchesActivated++;
            starter.position -= buttonHeight;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            door.switchesActivated--;
            starter.position += buttonHeight;
        }
    }
}
