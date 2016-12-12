using System.Collections;
using UnityEngine;

public class ContainerOpenner : Interactive
{
    private GameObject container;

    private void Start()
    {
        container = transform.parent.gameObject;
    }

    public override void doStuff(string grabbedName)
    {
        Debug.Log("doStuff called on pannel with " + grabbedName);
        if (container.GetComponent<OpenablePannel>())
        {
            Debug.Log("We have an OpenablePannel object");
            if (grabbedName == "Screwdriver")
            {
                container.GetComponent<OpenablePannel>().open = true;
            }
        }
    }
}