using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Goal> goals;
    public GameObject winUI;
    public GameObject pauseUI;
    bool paused;
    bool levelComplete;
    SFXPlayer sfxPlayer;
    DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        winUI.SetActive(false);
        sfxPlayer = FindFirstObjectByType<SFXPlayer>();
    }

    bool isLevelComplete()
    {
        foreach (var goal in goals)
        {
            if (!goal.isComplete) return false;
        }
        return true;
    }

    public void Pause()
    {
        paused = !paused;
        sfxPlayer.PlaySFX(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            sfxPlayer.PlaySFX(0);
            FindFirstObjectByType<SceneLoader>().ReloadScene();
        }
        if (!levelComplete && isLevelComplete()) FinishLevel();
        if (Input.GetKeyDown(KeyCode.Q) && !levelComplete)
        {
            sfxPlayer.PlaySFX(0);
            paused = !paused;
        }
        pauseUI.SetActive(paused);
        ControlTime();

    }

    void ControlTime()
    {
        if (levelComplete || paused || dialogueManager.inDialogue)
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
        sfxPlayer.PlaySFX(3);
        levelComplete = true;
        winUI.SetActive(true);
    }
}
