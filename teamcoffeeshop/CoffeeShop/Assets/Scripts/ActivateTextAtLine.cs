using UnityEngine;
using System.Collections;

public class ActivateTextAtLine : MonoBehaviour {


    public TextAsset theText;

    public int startLine;
    public int endLine;

    public DialogueManager dialogueManager;
	// Use this for initialization
	void Start () {

        dialogueManager = FindObjectOfType<DialogueManager>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
