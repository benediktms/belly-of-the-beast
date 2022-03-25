using System.Runtime.CompilerServices;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHanReloaddler : MonoBehaviour
{
    [SerializeField]
    float crashDelay = 1f;

    [SerializeField]
    float nextLevelDelay = 1f;

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Don't worry this is ok");
                break;
            case "Finish":
                // Invoke("LoadNextLevel", nextLevelDelay);
                LoadNextLevel();
                break;
            case "Fuel":
                Debug.Log("You found some fuel! Too bad it doesn't do anything.");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("Reload", crashDelay);
    }

    private void Reload()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

}
