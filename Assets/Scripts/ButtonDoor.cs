using System.Collections;
using UnityEngine;

public class ButtonDoor : Interactive
{
    public GameObject linkedDoor;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public override void doStuff(string gname)
    {
        Debug.Log("Button \"doStuff called\"");
        if (linkedDoor.GetComponent<Door>())
            linkedDoor.GetComponent<Door>().openDoor();
    }
}