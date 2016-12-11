using System.Collections;
using UnityEngine;

public class MouseLocking : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}