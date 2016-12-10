using System.Collections;
using UnityEngine;

public class ObjectMoverTest : MonoBehaviour
{
    private Rigidbody myBody;

    public Vector3 linearSpeed;
    public Vector3 angularSpeed;

    private Vector3 oldLinearSpeed;
    private Vector3 oldAngularSpeed;

    public bool attached;

    // Use this for initialization
    private void Start()
    {
        attached = false;
        myBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //If not attached, and speed manually changed, update the speed
        if (!attached)
        {
            if (oldLinearSpeed != linearSpeed)
            {
                myBody.velocity = linearSpeed;
                oldLinearSpeed = linearSpeed;
            }
            if (oldAngularSpeed != angularSpeed)
            {
                myBody.angularVelocity = angularSpeed;
                oldAngularSpeed = angularSpeed;
            }
        }
        //Attached object should not move.
        else
        {
            if (myBody.velocity != Vector3.zero) myBody.velocity = Vector3.zero;
            if (myBody.angularVelocity != Vector3.zero) myBody.velocity = Vector3.zero;
        }
    }
}