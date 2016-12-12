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
    public float axisDigitalThreshold;
    private GameObject grabbed;
    private Quaternion startOrientation;

    // Use this for initialization
    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        viewCam = GetComponentInChildren<Camera>();
        camTransform = viewCam.GetComponent<Transform>();
        camQuat = camTransform.localRotation;
        startOrientation = playerBody.rotation;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Joystick1Button7) || Input.GetKey(KeyCode.Escape))
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        RaycastHit hit;
        Ray ray = new Ray(viewCam.transform.position, viewCam.transform.forward);
        if (Physics.Raycast(ray, out hit, 1.5f))
        {
            Highlightable highlatable = hit.transform.GetComponent<Highlightable>();
            if (highlatable != null)
            {
                if (hit.transform.gameObject != grabbed)
                    highlatable.highlighted = true;
                if (hit.transform.GetComponent<Interactive>())
                {
                    string gname;
                    if (grabbed) gname = grabbed.name;
                    else gname = "nothing";
                    if (interact())
                        hit.transform.GetComponent<Interactive>().doStuff(gname);
                }
                else
                {
                    if (hit.transform.gameObject != grabbed)
                        highlatable.highlighted = true;

                    //Test if user want to grab object
                    if (interact())
                    {
                        ungrab();

                        //Grab object
                        if (hit.transform.GetComponent<ObjectMoverTest>())
                            hit.transform.GetComponent<ObjectMoverTest>().attached = true;
                        hit.transform.SetParent(transform.GetChild(0));
                        hit.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                        hit.transform.localPosition = Vector3.zero;
                        hit.transform.localRotation = Quaternion.Inverse(Quaternion.identity);
                        grabbed = hit.transform.gameObject;
                    }
                }
            }
        }
    }

    private void ungrab()
    {
        if (grabbed)
        {
            if (grabbed.GetComponent<ObjectMoverTest>())
                grabbed.GetComponent<ObjectMoverTest>().attached = false;
            grabbed.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            grabbed.transform.SetParent(null);
            grabbed = null;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (grabbed)
            grabbed.transform.rotation = transform.rotation;

        //Camera
        rotX += lookX();
        rotY += lookY();
        Quaternion x = Quaternion.AngleAxis(rotX, Vector3.up);
        Quaternion y = Quaternion.AngleAxis(rotY, Vector3.left);

        camTransform.localRotation = camQuat * x * y;

        if (interactSecondary())
            ungrab();

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
        if (rotateRollPlus())
            playerBody.AddRelativeTorque(0, 0, rotateForce);
        if (rotateRollMinus())
            playerBody.AddRelativeTorque(0, 0, -rotateForce);

        if (stabilize())
        {
            if (playerBody.velocity != Vector3.zero)
                playerBody.velocity = Vector3.Lerp(playerBody.velocity, Vector3.zero, 0.1f);

            //cheating by LERPing
            if (playerBody.angularVelocity != Vector3.zero)
                playerBody.angularVelocity = Vector3.Lerp(playerBody.angularVelocity, Vector3.zero, 0.1f);
            else
            {
                if (rotateModifier())
                    playerBody.rotation = Quaternion.Lerp(playerBody.rotation, startOrientation, 0.01f);
            }
        }
    }

    public bool stabilize()
    {
        if (Input.GetKey(KeyCode.Joystick1Button4))
            return true;
        if (Input.GetKey(KeyCode.Space))
            return true;
        return false;
    }

    //W or Z or GamePad ???
    private bool forward()
    {
        if (Input.GetAxis("Vertical") < -axisDigitalThreshold)
            return true;
        //Put commands to go forward there
        if (Input.GetKey(KeyCode.W))
            return true;
        if (Input.GetKey(KeyCode.Z))
            return true;

        return false;
    }

    private bool backward()
    {
        if (Input.GetAxis("Vertical") > axisDigitalThreshold)
            return true;
        if (Input.GetKey(KeyCode.S))
            return true;
        //put commands to go backward there
        return false;
    }

    private bool upward()
    {
        if (Input.GetAxis("RightTrig") > axisDigitalThreshold)
            return true;
        if (Input.GetKey(KeyCode.R))
            return true;
        return false;
    }

    private bool downward()
    {
        if (Input.GetAxis("LeftTrig") > axisDigitalThreshold)
            return true;
        if (Input.GetKey(KeyCode.F))
            return true;
        return false;
    }

    private bool left()
    {
        if (Input.GetAxis("Horizontal") < -axisDigitalThreshold)
            return true;
        if (Input.GetKey(KeyCode.A))
            return true;
        if (Input.GetKey(KeyCode.Q))
            return true;
        return false;
    }

    private bool right()
    {
        if (Input.GetAxis("Horizontal") > axisDigitalThreshold)
            return true;
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

    private bool rotateRollPlus()
    {
        return rotateModifier() && downward();
    }

    private bool rotateRollMinus()
    {
        return rotateModifier() && upward();
    }

    private bool rotateModifier()
    {
        if (Input.GetKey(KeyCode.Joystick1Button3))
            return true;
        if (Input.GetKey(KeyCode.LeftShift))
            return true;
        return false;
    }

    private bool interactSecondary()
    {
        if (Input.GetKey(KeyCode.Joystick1Button1))
            return true;
        if (Input.GetKey(KeyCode.Mouse1))
            return true;
        return false;
    }

    private bool interact()
    {
        if (Input.GetKey(KeyCode.Joystick1Button0))
            return true;
        if (Input.GetKey(KeyCode.Mouse0))
            return true;
        return false;
    }

    private float lookX()
    {
        if (Input.GetAxis("RightStick X") != 0)
            return Input.GetAxis("RightStick X");
        if (Input.GetAxis("Mouse X") != 0)
            return Input.GetAxis("Mouse X");
        return 0;
    }

    private float lookY()
    {
        if (Input.GetAxis("RightStick Y") != 0)
            return Input.GetAxis("RightStick Y");
        if (Input.GetAxis("Mouse Y") != 0)
            return Input.GetAxis("Mouse Y");
        return 0;
    }
}