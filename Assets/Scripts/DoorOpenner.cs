using System.Collections;
using UnityEngine;

public class DoorOpenner : Interactive
{
    public GameObject DoorToOpen;

    public override void doStuff(string gname)
    {
        if (DoorToOpen.GetComponent<Door>())
        {
            DoorToOpen.GetComponent<Door>().open = true;
        }
    }
}