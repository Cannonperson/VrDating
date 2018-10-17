using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree {

    public DialogueNode rootNode;

}

public class DialogueNode
{
    public string message;
    public DialogueNode yesOption;
    public DialogueNode noOption;
    public DialogueNode drunkYesOption;
    public DialogueNode drunkNoOption;
}
