using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] private InputAction rotation;
    [SerializeField] private float rotationStrength = 100f;
    [SerializeField] private float thrustStrength = 100f;
    [SerializeField] private AudioClip mainEngine;

    private Rigidbody _rb;
    private AudioSource _audioSource;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ApplyThrustForce();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
        }

        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        _rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * (rotationThisFrame * Time.fixedDeltaTime));
        _rb.freezeRotation = false;
    }

    private void ApplyThrustForce()
    {
        if (thrust.IsPressed())
        {
            _rb.AddRelativeForce(Vector3.up * (thrustStrength * Time.fixedDeltaTime));
            if (!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            _audioSource.Stop();
        }
    }
}