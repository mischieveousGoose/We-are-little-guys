using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Goal> goals;
    public GameObject winUI;
    public GameObject pauseUI;
    bool paused;
    bool levelComplete;

    void Start()
    {
        winUI.SetActive(false);
    }

    bool isLevelComplete()
    {
        foreach (var goal in goals)
        {
            if (!goal.isComplete) return false;
        }
        return true;
    }

    void Update()
    {
        if (!levelComplete && isLevelComplete()) FinishLevel();
        if (Input.GetKeyDown(KeyCode.Q) && !levelComplete) paused = !paused;
        pauseUI.SetActive(paused);
        ControlTime();

    }

    void ControlTime()
    {
        if (levelComplete || paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void FinishLevel()
    {
        levelComplete = true;
        winUI.SetActive(true);
    }
}
