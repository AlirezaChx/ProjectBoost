using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float force = 1f;
    [SerializeField] private float rotationForce = 1f;
    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private ParticleSystem thurstParticleSystem;
    [SerializeField] private ParticleSystem rightRotationPS;
    [SerializeField] private ParticleSystem leftRotationPS;
    
    private Rigidbody _rb;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            MoveUp();
        }
        else
        {
            StopThrust();
        }
        
    }

    private void StopThrust()
    {
        thurstParticleSystem.Stop();
        _audioSource.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ThrustLeft();
        }
        else
        {
            leftRotationPS.Stop();
        }
        if (Input.GetKey(KeyCode.D))
        {
            ThrustRight();
        }
        else
        {
            rightRotationPS.Stop();
        }
    }

    private void ThrustRight()
    {
        ApplyRotation(-rotationForce);
        if (!rightRotationPS.isPlaying)
        {
            rightRotationPS.Play();
        }
    }

    private void ThrustLeft()
    {
        ApplyRotation(rotationForce);
        if (!leftRotationPS.isPlaying)
        {
            leftRotationPS.Play();
        }
    }

    void ApplyRotation(float rotationThrust)
    {
        transform.Rotate(Vector3.forward * (rotationThrust * Time.deltaTime));
    }

    private void MoveUp()
    {
        _rb.AddRelativeForce(Vector3.up * (force * Time.deltaTime));
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(mainEngine);
            if (thurstParticleSystem.isPlaying) { return; }
            thurstParticleSystem.Play();
        }
    }
}

