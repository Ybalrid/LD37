using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 startPosition;
    public Vector3 openMovement;
    public bool open;

    // Use this for initialization
    private void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (open)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition + openMovement, 0.1f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, 0.1f);
        }
    }

    public void openDoor()
    {
        open = true;
    }

    public void closeDoor()
    {
        open = false;
    }
}