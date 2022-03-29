using System;
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

    [SerializeField]
    ParticleSystem explosionParticles;

    [SerializeField]
    ParticleSystem successParticles;

    [SerializeField]
    ParticleSystem rocketJet;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool isCollisionDisabled = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            isCollisionDisabled = !isCollisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || isCollisionDisabled) return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                // this is ok
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        rocketJet.Stop();
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;

        audioSource.PlayOneShot(explosionSFX);
        explosionParticles.Play();

        Invoke("Reload", crashDelay);
    }

    private void StartSuccessSequence()
    {
        rocketJet.Stop();
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;

        audioSource.PlayOneShot(successSFX);
        successParticles.Play();

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
