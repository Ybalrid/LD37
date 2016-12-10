using System.Collections;
using UnityEngine;

public class GravitySetup : MonoBehaviour
{
    public Vector3 gravity;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        Physics.gravity = gravity;
    }
}