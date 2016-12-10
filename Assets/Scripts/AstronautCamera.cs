using System.Collections;
using UnityEngine;

public class AstronautCamera : MonoBehaviour
{
    private Rigidbody playerBody;
    private Camera viewCam;
    private Quaternion camQuat;
    private Transform camTransform;

    private float rotX, rotY;

    public float translateForce, rotateForce;
    public float rotateSpeedRealign;

    // Use this for initialization
    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        viewCam = GetComponentInChildren<Camera>();
        camTransform = viewCam.GetComponent<Transform>();
        camQuat = camTransform.localRotation;
    }

    // Update is called once per frame
    private void Update()
    {
        //Camera
        rotX += lookX();
        rotY += lookY();
        Quaternion x = Quaternion.AngleAxis(rotX, Vector3.up);
        Quaternion y = Quaternion.AngleAxis(rotY, Vector3.left);

        camTransform.localRotation = camQuat * x * y;

        //Jetpack command code :
        //TODO : handle fuel depletion
        if (forward() && !rotateModifier())
            playerBody.AddRelativeForce(0, 0, translateForce);
        if (backward() && !rotateModifier())
            playerBody.AddRelativeForce(0, 0, -translateForce);
        if (upward() && !rotateModifier())
            playerBody.AddRelativeForce(0, translateForce, 0);
        if (downward() && !rotateModifier())
            playerBody.AddRelativeForce(0, -translateForce, 0);
        if (left() && !rotateModifier())
            playerBody.AddRelativeForce(-translateForce, 0, 0);
        if (right() && !rotateModifier())
            playerBody.AddRelativeForce(translateForce, 0, 0);

        if (rotatePitchPlus())
            playerBody.AddRelativeTorque(rotateForce, 0, 0);
        if (rotatePitchMinus())
            playerBody.AddRelativeTorque(-rotateForce, 0, 0);
        if (rotateYawPlus())
            playerBody.AddRelativeTorque(0, rotateForce, 0);
        if (rotateYawMinus())
            playerBody.AddRelativeTorque(0, -rotateForce, 0);

        if (stabilize())
        {
            if (playerBody.velocity.x > 0)
                playerBody.AddRelativeForce(-translateForce, 0, 0);
            if (playerBody.velocity.x < 0)
                playerBody.AddRelativeForce(+translateForce, 0, 0);
            if (playerBody.velocity.y > 0)
                playerBody.AddRelativeForce(0, -translateForce, 0);
            if (playerBody.velocity.y < 0)
                playerBody.AddRelativeForce(0, +translateForce, 0);
            if (playerBody.velocity.z > 0)
                playerBody.AddRelativeForce(0, 0, -translateForce);
            if (playerBody.velocity.z < 0)
                playerBody.AddRelativeForce(0, 0, +translateForce);
            /*
            if (playerBody.angularVelocity.x > 0)
                playerBody.AddRelativeTorque(-rotateForce, 0, 0);
            if (playerBody.angularVelocity.x < 0)
                playerBody.AddRelativeTorque(+rotateForce, 0, 0);
            if (playerBody.angularVelocity.y > 0)
                playerBody.AddRelativeTorque(0, -rotateForce, 0);
            if (playerBody.angularVelocity.y < 0)
                playerBody.AddRelativeTorque(0, +rotateForce, 0);
                */
            //cheating by LERPing
            if (playerBody.angularVelocity != Vector3.zero)
                playerBody.angularVelocity = Vector3.Lerp(playerBody.angularVelocity, Vector3.zero, 0.1f);
            else
                playerBody.rotation = Quaternion.Lerp(playerBody.rotation, Quaternion.identity, 0.01f);
        }
        Debug.Log("Linear Velocity : " + playerBody.velocity);
        Debug.Log("Angular velocity : " + playerBody.angularVelocity);
    }

    public bool stabilize()
    {
        if (Input.GetKey(KeyCode.Space))
            return true;
        return false;
    }

    //W or Z or GamePad ???
    private bool forward()
    {
        //Put commands to go forward there
        if (Input.GetKey(KeyCode.W))
            return true;
        if (Input.GetKey(KeyCode.Z))
            return true;

        return false;
    }

    private bool backward()
    {
        if (Input.GetKey(KeyCode.S))
            return true;
        //put commands to go backward there
        return false;
    }

    private bool upward()
    {
        if (Input.GetKey(KeyCode.R))
            return true;
        return false;
    }

    private bool downward()
    {
        if (Input.GetKey(KeyCode.F))
            return true;
        return false;
    }

    private bool left()
    {
        if (Input.GetKey(KeyCode.A))
            return true;
        if (Input.GetKey(KeyCode.Q))
            return true;
        return false;
    }

    private bool right()
    {
        if (Input.GetKey(KeyCode.D))
            return true;
        return false;
    }

    private bool rotateYawPlus()
    {
        return rotateModifier() && right();
    }

    private bool rotateYawMinus()
    {
        return rotateModifier() && left();
    }

    private bool rotatePitchPlus()
    {
        return rotateModifier() && forward();
    }

    private bool rotatePitchMinus()
    {
        return rotateModifier() && backward();
    }

    private bool rotateModifier()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            return true;
        return false;
    }

    private float lookX()
    {
        if (Input.GetAxis("Mouse X") != 0)
            return Input.GetAxis("Mouse X");
        return 0;
    }

    private float lookY()
    {
        if (Input.GetAxis("Mouse Y") != 0)
            return Input.GetAxis("Mouse Y");
        return 0;
    }
}