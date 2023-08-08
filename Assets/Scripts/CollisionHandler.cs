using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float _loadDelayInSeconds = 2f;
    [SerializeField] AudioClip _finishClip;
    [SerializeField] AudioClip _crashClip;
    [SerializeField] ParticleSystem _particlesSuccess;
    [SerializeField] ParticleSystem _particlesCrash;

    AudioSource _movementSource;

    private bool isTransitioning = false;
    private bool collisionDisabled = false;
    public const string Friendly = "Friendly";
    public const string Finish = "Finish";

    private void Start()
    {
        _movementSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            ToggleCollision();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) return;

        switch (collision.gameObject.tag)
        {
            case Friendly:
                break;
            case Finish:
                StartNextLevel();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        _movementSource.Stop();
        _movementSource.PlayOneShot(_crashClip, 0.2f);

        _particlesCrash.Play();

        StopMovement();

        Invoke("ReloadLevel", _loadDelayInSeconds);

    }

    void StartNextLevel()
    {
        isTransitioning = true;
        _movementSource.Stop();
        _movementSource.PlayOneShot(_finishClip, 0.2f);

        _particlesSuccess.Play();

        StopMovement();

        Invoke("LoadNextLevel", _loadDelayInSeconds);
    }

    void ToggleCollision()
    {
        collisionDisabled = !collisionDisabled;
    }


    void StopMovement()
    {
        Movement movement = GetComponent<Movement>();

        movement.enabled = false;
        movement.StopRotation();
        movement.StopBoost();
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
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

