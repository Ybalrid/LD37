using System.Collections;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public int selection;

    // Use this for initialization
    private void Start()
    {
    }

    private bool interact()
    {
        if (Input.GetKey(KeyCode.Joystick1Button0))
            return true;
        if (Input.GetKey(KeyCode.Mouse0))
            return true;
        return false;
    }

    //W or Z or GamePad ???
    private bool forward()
    {
        if (Input.GetAxis("Vertical") < -.5f)
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
        if (Input.GetAxis("Vertical") > .5f)
            return true;
        if (Input.GetKey(KeyCode.S))
            return true;
        //put commands to go backward there
        return false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (interact()) UnityEngine.SceneManagement.SceneManager.LoadScene(selection + 1);
        switch (selection)
        {
            //Tutorial
            case 0:
                transform.GetChild(0).GetComponent<TextMesh>().color = Color.red;
                transform.GetChild(1).GetComponent<TextMesh>().color = Color.white;
                if (backward()) selection++;
                break;

            //PlayGame
            case 1:
                transform.GetChild(1).GetComponent<TextMesh>().color = Color.red;
                transform.GetChild(0).GetComponent<TextMesh>().color = Color.white;
                if (forward()) selection--;
                break;

            default: break;
        }
    }
}