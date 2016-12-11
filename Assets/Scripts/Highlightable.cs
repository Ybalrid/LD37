using System.Collections;
using UnityEngine;

public class Highlightable : MonoBehaviour
{
    public Material highlight;
    private Material defaultMat;
    public bool highlighted;

    private Renderer myRenderer;

    // Use this for initialization
    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
        defaultMat = myRenderer.material;
    }

    private void FixedUpdate()
    {
        //If we have been highlighted
        if (highlighted)
            myRenderer.material = highlight;
        else
            myRenderer.material = defaultMat;

        //Reset boolean
        highlighted = false;
    }

    private void Update()
    {
    }
}