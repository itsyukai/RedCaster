﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    private Queue<string> sentences;

    public Text NameText;
    public Text DialogueText;

    public Canvas TextCanvas;

    void Start () {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue) {
        TextCanvas.gameObject.SetActive(true);

        NameText.text = dialogue.name;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence) {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()){
            DialogueText.text += letter;
            yield return null; // waits a single frame
        }
    }
    void EndDialogue() {
        TextCanvas.gameObject.SetActive(false);
        Debug.Log("End of conversation");
    }
}