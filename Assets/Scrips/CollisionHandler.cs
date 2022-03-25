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
                Invoke("StartSuccessSequence", nextLevelDelay);
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
        // TODO: add sound effect
        // TODO: add particle effect
        GetComponent<Movement>().enabled = false;
        Invoke("Reload", crashDelay);
    }

    private void StartSuccessSequence()
    {
        // TODO: add sound effect
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", nextLevelDelay);
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
