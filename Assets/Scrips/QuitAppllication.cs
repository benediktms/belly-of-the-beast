using UnityEngine;

public class QuitAppllication : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quitting game");
            Application.Quit();
        }
    }
}
