using UnityEngine.UI;
using UnityEngine;
using Mirror;

public class NPC : MonoBehaviour
{
    [SerializeField] DialogueTrigger dialogue = default;

    [SerializeField] Text nameUI = default;
    [SerializeField] GameObject haveQuestImg = default;

    [Header("Question")]
    [SerializeField] bool haveQuest = default;

    private void Start()
    {
        dialogue = GetComponent<DialogueTrigger>();
        nameUI.text = "" + dialogue.dialogue.name;

        haveQuestImg.SetActive(haveQuest);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                if (!dialogue.isActive)
                    dialogue.startDialogue.SetActive(true);
                else
                    dialogue.startDialogue.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
                dialogue.startDialogue.SetActive(false);

            dialogue.FinishDialogue();
        }
    }
}
