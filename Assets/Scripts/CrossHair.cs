using System.Collections;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public Texture2D crosshairTexture;
    private Rect position;

    private int width, height;

    // Use this for initialization
    private void Start()
    {
        width = Screen.width;
        height = Screen.height;
        updatePosition();
    }

    private void FixedUpdate()
    {
        if (width != Screen.width || height != Screen.height) updatePosition();
    }

    private void updatePosition()
    {
        width = Screen.width;
        height = Screen.height;
        position = new Rect((width - crosshairTexture.width) / 2, (height - crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height);
    }

    private void OnGUI()
    {
        GUI.DrawTexture(position, crosshairTexture);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}