using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public GameObject startDialogue;
    public bool isActive;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        isActive = true;
    }
    public void FinishDialogue()
    {
        FindObjectOfType<DialogueManager>().EndDialogue();
        isActive = false;
    }
}
