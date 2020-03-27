using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public string[] moleSentences;
    public GameObject sentenceDisplay;
    public TextMeshProUGUI sentenceText;

    private string currentSentence;
    private float typingSpeed;
    private Queue<string> sentecesOrder;

    private void Start()
    {
        sentecesOrder = new Queue<string>();
    }

    public void StartDialogue()
    {
        sentecesOrder.Clear();
        foreach (string sentence in moleSentences)
            sentecesOrder.Enqueue(sentence);
    }
    public void AssignNextText() 
    {
        StopCoroutine(TimeToTyping());
        if (sentecesOrder.Count <= 0)
        {
            sentenceText.text = currentSentence;
            return;
        }
        currentSentence = sentecesOrder.Dequeue();
        sentenceText.text = currentSentence;

        IEnumerator TimeToTyping()
        {
            sentenceText.text = "";
            foreach (char letter in currentSentence.ToCharArray())
                sentenceText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        StartCoroutine(TimeToTyping());
    }
}
