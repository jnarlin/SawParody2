using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogueSegment 
    {
        public string SubjectText;

        [TextArea]
        public string DialogueToPrint;
        public bool Skippable;
        
        [Range(1f, 25f)]
        public float LettersPerSecond;
    }

    [SerializeField] private DialogueSegment[] DialogueSegements;
    [Space]
    [SerializeField] private TMP_Text SubjectText;
    [SerializeField] private TMP_Text BodyText;

    private int DialogueIndex;
    private bool PlayingDialogue;
    private bool Skip;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayDialogue(DialogueSegements[DialogueIndex]));
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
       {    
            if (DialogueIndex == DialogueSegements.Length)
            {
                enabled = false;
                return;
            }

            if (!PlayingDialogue)
            {
                StartCoroutine(PlayDialogue(DialogueSegements[DialogueIndex]));
            }
            else
            {
                if (DialogueSegements[DialogueIndex].Skippable)
                {
                    Skip = true;
                }
            }
       } 
    }

    private IEnumerator PlayDialogue(DialogueSegment segment)
    {
        PlayingDialogue = true;
        BodyText.SetText(string.Empty);
        SubjectText.SetText(segment.SubjectText);

        float delay = 1f / segment.LettersPerSecond;
        for (int i = 0; i < segment.DialogueToPrint.Length; i++)
        {
            if (Skip)
            {
                BodyText.SetText(segment.DialogueToPrint);
                Skip = false;
                break;
            }

            string chunkToAdd = string.Empty;
            chunkToAdd += segment.DialogueToPrint[i];
            if (segment.DialogueToPrint[i] == ' ' && i < segment.DialogueToPrint.Length - 1)
            {
                chunkToAdd = segment.DialogueToPrint.Substring(i, 2);
                i++;
            }

            BodyText.text += chunkToAdd;
            yield return new WaitForSeconds(delay);
        }

        PlayingDialogue = false;
        DialogueIndex++;
    }
}
