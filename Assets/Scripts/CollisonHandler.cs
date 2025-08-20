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
                LoadNexScene();
                break;
            default:
                    ReloadLevel();
                    break;
        }
        void LoadNexScene()
        {
            int curretnSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(curretnSceneIndex + 1);
            
        }
        void ReloadLevel()
        {
            int curretnSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(curretnSceneIndex);
        }
    }
}
