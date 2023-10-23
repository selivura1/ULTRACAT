using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] Messages;

    public void StartDialogue()
    {
        FindAnyObjectByType<DialogueUI>().OpenDialogue(Messages);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartDialogue();
    }
}

[System.Serializable]
public class Message
{
    [TextArea()]
    public string TextMessage;
    public Sprite Icon;
}
