using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public List<Dialogue> dialogue;
    int index = -1;
    public string nextScene;
    AudioSource source;
    public ParticleSystem alienParticles;
    public bool isLevelDialogue;
    public bool hasDialogue;

    [Header("UI")]
    public Image face;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public bool inDialogue = false;

    public GameObject dialogueBox;


    void Start()
    {

        if (!hasDialogue) return;
        if (isLevelDialogue && SettingsManager.instance.didReset) return;
        if (isLevelDialogue) dialogueBox.SetActive(true);
        else Time.timeScale = 1;


        source = GetComponent<AudioSource>();
        StartCoroutine(RunDialogue());

    }

    public void EndDialogue()
    {
        inDialogue = false;
        if (!isLevelDialogue)
            FindFirstObjectByType<SceneLoader>().LoadScene(nextScene);
        else
            dialogueBox.SetActive(false);
    }

    IEnumerator RunDialogue()
    {
        inDialogue = true;
        if (index == -1)
            yield return new WaitForSecondsRealtime(1f);

        index++;
        if (index >= dialogue.Count)
        {
            EndDialogue();
        }
        else
        {
            if (index == 3 && alienParticles != null)
            {
                alienParticles.Play();
            }
            dialogueText.maxVisibleCharacters = 0;
            dialogueText.text = dialogue[index].text;

            if (!isLevelDialogue)
            {
                face.sprite = dialogue[index].face;
                nameText.text = dialogue[index].cName;
            }
            source.PlayOneShot(dialogue[index].voiceLine);

            while (dialogueText.maxVisibleCharacters < dialogue[index].text.Length)
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSecondsRealtime(dialogue[index].delay);
            }

            StartCoroutine(RunDialogue());
        }
    }
}

[Serializable]
public class Dialogue
{
    public Sprite face;
    public string cName;
    public float delay;
    public AudioClip voiceLine;

    [TextArea(3, 5)]
    public string text;
}
