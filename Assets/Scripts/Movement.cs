using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float _mainThrust = 300f;
    [SerializeField] float _rotationThrust = 50f;
    Rigidbody _rigidbody;
    AudioSource _audioSource;
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
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
            _rigidbody.AddRelativeForce(Vector3.up * _mainThrust * Time.deltaTime);
        }
        else
        {
            _audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Rotation(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotation(Vector3.back);
        }
    }

    void Rotation(Vector3 vector3)
    {
        //Freezing rotation so we can manually rotate

        _rigidbody.freezeRotation = true;
        transform.Rotate(vector3 * _rotationThrust * Time.deltaTime);
        _rigidbody.freezeRotation = false;

        //After unfreezing it
    }
}
