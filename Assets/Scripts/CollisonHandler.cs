using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisonHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Everything is looking good!");
                break;
            case "Finish":
                LoadNextScene();
                break;
            default:
                StartCrashSequence();
                break;
        }
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

    void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 2f);
    }
}
