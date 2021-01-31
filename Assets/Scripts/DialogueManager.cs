using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI sentenceText;
    [SerializeField] private Animator dialogueBoxAnimator;

    private Queue<string> _sentencesQueue;
    public bool _isOpened;
    
    private static DialogueManager _instance;
    public static DialogueManager Instance{
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<DialogueManager>();
            }

            return _instance;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _sentencesQueue = new Queue<string>();
        GameManager.Instance.ShowStartDialogue();
    }

    private void Update()
    {
        if (_isOpened)
        {
            if (Input.GetKeyDown("space"))
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBoxAnimator.SetBool("IsOpened", true);
        GameManager.Instance.movementInputLock = true;

        nameText.text = dialogue.name;
        sentenceText.text = "";

        _sentencesQueue.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            _sentencesQueue.Enqueue(sentence);
        }
        
        StartCoroutine(DelayOpenBox());
    }
    
    IEnumerator DelayOpenBox()
    {
        yield return new WaitForSeconds(0.25f);
        _isOpened = true;
        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (_sentencesQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string currSentence = _sentencesQueue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        sentenceText.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            sentenceText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void EndDialogue()
    {
        dialogueBoxAnimator.SetBool("IsOpened", false);
        StartCoroutine(DelayReadyForInput());
    }
    
    IEnumerator DelayReadyForInput()
    {
        yield return new WaitForSeconds(0.25f);
        _isOpened = false;
        GameManager.Instance.movementInputLock = false;
    }
}
