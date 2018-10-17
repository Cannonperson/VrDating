using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Date : MonoBehaviour {

    public TextMesh dialogue;
    public DialogueTree dialogueTree;
    public DialogueNode currentDialogue;

    bool waitingForAnswer = true;
    bool answer;

    public void SetAnswer(bool answer)
    {
        this.answer = answer;
        waitingForAnswer = false;
    }

    public void StartDate()
    {
        currentDialogue = dialogueTree.rootNode;
        StartCoroutine("RunDate");
    }

    IEnumerator RunDate()
    {
        dialogue.text = currentDialogue.message;
        while(currentDialogue.yesOption != null && currentDialogue.noOption != null)
        {
            yield return null;
            if (!waitingForAnswer)
            {
                currentDialogue = answer ? currentDialogue.yesOption : currentDialogue.noOption;
                SetText(currentDialogue.message);
            }
        }
    }

    public void SetText(string message)
    {
        dialogue.text = message;
    }

    public void SetText(string message, int jumbleWords)
    {
        dialogue.text = JumbleString(message, jumbleWords);
    }

    public string JumbleString(string message, int amount)
    {
        string[] msg = message.Split(' ');

        for(int i = 0; i < amount; i++)
        {
            int randInt = Random.Range(0, msg.Length);
            msg[randInt] = GetRandomWord(msg[randInt]);
        }

        string returnMsg = "";
        for(int i = 0; i < msg.Length; i++)
        {
            returnMsg += msg[i] + " ";
        }
        return returnMsg;
    }

    private string GetRandomWord(string replaceWord)
    {
        string msg = "";
        if(Random.Range(0,10) <= 5)
        {
            for(int i = 0; i < replaceWord.Length; i++)
            {
                msg += "*";
            }
            return msg;
        }
        else
        {
            //to-do: make a list of random words
            return "RANDOMWORD";
        }
    }
}
