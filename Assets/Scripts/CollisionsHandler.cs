using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class CollisionsHandler : MonoBehaviour
{
    [SerializeField] private float winDelay = 1f;
    [SerializeField] private float loseDelay = 1f;
    [SerializeField] private AudioClip crashSfx;
    [SerializeField] private AudioClip winSfx;
    [SerializeField] private ParticleSystem crashVfx;
    [SerializeField] private ParticleSystem winVfx;
    
    private AudioSource _audioSource;

    private bool _isTransitioning = false;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        JumpToNextLevel();   
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Input.GetKey(KeyCode.C))
        {
            return;
        }
        if (_isTransitioning) { return; }
        var colliderTag = other.gameObject.tag;
        switch (colliderTag)
        {
            case "Finish":
                print("Nice You have finished");
                StartWinSequence();
                break;
            case "Friendly":
                //print("this is friendly");
                break;
            default:
                print("OpsYouCrashed!");
                StartCrashSequence();
                break;
        }
    }

    void StartWinSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(winSfx);
        winVfx.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel),winDelay);
    }

    
    void StartCrashSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(crashSfx);
        crashVfx.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel),loseDelay);
    }
    void JumpToNextLevel()
    {
        if (!Input.GetKey(KeyCode.L)) return;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    private void LoadNextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
