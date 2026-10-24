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

    [Header("UI")]
    public Image face;
    public TMP_Text nameText;
    public TMP_Text dialogueText;


    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(RunDialogue());

    }

    public void EndDialogue()
    {
        FindFirstObjectByType<SceneLoader>().LoadScene(nextScene);
    }

    IEnumerator RunDialogue()
    {
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
            nameText.text = dialogue[index].cName;
            face.sprite = dialogue[index].face;
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
