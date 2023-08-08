using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float _mainThrust = 300f;
    [SerializeField] float _rotationThrust = 50f;
    [SerializeField] AudioClip _mainEngineClip;
    [SerializeField] ParticleSystem _particlesLeftThruster;
    [SerializeField] ParticleSystem _particlesRightThruster;
    [SerializeField] ParticleSystem _particlesBoosterThruster;

    Rigidbody _rigidbody;
    public AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    void StartThrusting()
    {
        _rigidbody.AddRelativeForce(Vector3.up * _mainThrust * Time.deltaTime);
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_mainEngineClip, 1);
        }
        if (!_particlesBoosterThruster.isPlaying)
        {
            _particlesBoosterThruster.Play();
        }
    }

    void StopThrusting()
    {
        _audioSource.Stop();
        _particlesBoosterThruster.Stop();
    }

    void RotateLeft()
    {
        Rotation(Vector3.forward);
        if (!_particlesRightThruster.isPlaying)
        {
            _particlesRightThruster.Play();
        }
    }

    void RotateRight()
    {
        Rotation(Vector3.back);
        if (!_particlesLeftThruster.isPlaying)
        {
            _particlesLeftThruster.Play();
        }
    }

    public void StopRotation()
    {
        _particlesRightThruster.Stop();
        _particlesLeftThruster.Stop();
    }


    void Rotation(Vector3 vector3)
    {
        //Freezing rotation so we can manually rotate

        _rigidbody.freezeRotation = true;
        transform.Rotate(vector3 * _rotationThrust * Time.deltaTime);
        _rigidbody.freezeRotation = false;

        //After unfreezing it
    }

    public void StopBoost()
    {
        _particlesBoosterThruster.Stop();
    }
}
