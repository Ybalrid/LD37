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
        if (container.GetComponent<OpenablePannel>())
        {
            if (grabbedName == "Screwdriver")
            {
                container.GetComponent<OpenablePannel>().open = true;
            }
        }
    }
}