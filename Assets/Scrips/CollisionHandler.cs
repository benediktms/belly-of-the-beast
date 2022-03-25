using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    float crashDelay = 1f;

    [SerializeField]
    float nextLevelDelay = 1f;

    [SerializeField]
    AudioClip explosionSFX;

    [SerializeField]
    AudioClip successSFX;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(explosionSFX);
        }

        Invoke("Reload", crashDelay);
    }

    private void StartSuccessSequence()
    {
        // TODO: add sound effect
        // TODO: add particle effect
        GetComponent<Movement>().enabled = false;

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(successSFX);
        }

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
