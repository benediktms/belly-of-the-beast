using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float forwardThrust = 1000f;

    [SerializeField]
    float rotationThrust = 100f;

    [SerializeField]
    AudioClip thrustersSFX;

    [SerializeField]
    ParticleSystem mainThrusters;

    [SerializeField]
    ParticleSystem leftThrusters;

    [SerializeField]
    ParticleSystem rightThrusters;

    Rigidbody rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }

    private void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * forwardThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustersSFX);
        }
        mainThrusters.Play();
    }

    private void StopThrust()
    {
        audioSource.Stop();
        mainThrusters.Stop();
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ThrustRight();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ThrustLeft();
        }
        else
        {
            StopRotationalThrust();
        }
    }

    private void ThrustLeft()
    {
        ApplyRotation(-rotationThrust);
        leftThrusters.Play();
    }

    private void ThrustRight()
    {
        ApplyRotation(rotationThrust);
        rightThrusters.Play();
    }

    private void StopRotationalThrust()
    {
        rightThrusters.Stop();
        leftThrusters.Stop();
    }


    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
