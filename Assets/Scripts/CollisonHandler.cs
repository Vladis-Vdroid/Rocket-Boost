using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisonHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;
    [SerializeField] private AudioClip crashSFX;
    [SerializeField] private AudioClip successSFX;
    [SerializeField] private ParticleSystem successParticles;
    [SerializeField] private ParticleSystem crashParticles;
    
    private AudioSource _audioSource;
    
    bool isControllable = true;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    
    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable) return;
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Everything is looking good!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        
        isControllable = false;
        _audioSource.Stop();
        _audioSource.PlayOneShot(successSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", levelLoadDelay);
        successParticles.Play();
    }
    void StartCrashSequence()
    {
        
        isControllable = false;
        _audioSource.Stop();
        _audioSource.PlayOneShot(crashSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        crashParticles.Play();
    }

    void LoadNextScene()
    {
        int curretnSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = curretnSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int curretnSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curretnSceneIndex);
    }
}
