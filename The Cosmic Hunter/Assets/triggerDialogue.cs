using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerDialogue : MonoBehaviour {
    public GameObject dialogueBox;
    public SpriteRenderer sprite;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Munia")
        {
            dialogueBox.SetActive(true);
            sprite.sprite = Resources.Load<Sprite>("Images/Objects/TutorialArea/Dialogue trigger On");
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Munia")
        {
            dialogueBox.SetActive(false);
        }
    }
}
