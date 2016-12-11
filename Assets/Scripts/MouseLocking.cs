using System.Collections;
using UnityEngine;

public class MouseLocking : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}