using System.Collections;
using UnityEngine;

public class ToggleEnd : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Astronaut")
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}