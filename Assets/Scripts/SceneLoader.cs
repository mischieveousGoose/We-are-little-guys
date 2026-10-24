using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public float waitTime;
    public Animator transition;
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevelCoroutine(sceneName));
    }

    void Start()
    {
        transition.PlayInFixedTime("SceneStart");
    }

    public IEnumerator LoadLevelCoroutine(string sceneName)
    {
        transition.PlayInFixedTime("SceneEnd");
        yield return new WaitForSecondsRealtime(waitTime);
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        LoadScene(sceneName);
    }
}
