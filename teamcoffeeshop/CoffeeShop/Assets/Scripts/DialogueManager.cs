using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Text
using System.Collections.Generic; //List

public class DialogueManager : MonoBehaviour {
    public GameObject textBox;

    public Text texts;
    
    public List<string> npc1DialogueList = new List<string>();
    public List<string> npc2DialogueList = new List<string>();

    public TextAsset textFile;
    public string[] textLines;


    private int npc1CurrentLine;
    private int npc1EndLine;

    private int npc2CurrentLine;
    private int npc2EndLine;

    private bool npc1Turn;
    private bool npc2Turn;

    public bool isActive;

    private bool isTyping = false;
    private bool cancelTyping = false;

    public float typeSpeed;

	// Use this for initialization
	void Start () {

        npc1Turn = true;
        npc2Turn = false;

        //If there is text file, get strings
        if(textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
            ClassifyDialogue(textLines);
        }

        if(npc1EndLine == 0 || npc2EndLine == 0)
        {
            // Get dialogue length;
            npc1EndLine = npc1DialogueList.Count;
            npc2EndLine = npc2DialogueList.Count;
        }

        if(isActive)
        {
            EnableDialogueBox();
        }
        else
        {
            DisableDialogueBox();
        }
	}
	
	// Update is called once per frame
	void Update () {

        //##CHANGE## The key set enter button temporaily, it will change
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (!isActive)
                EnableDialogueBox();

            if(!isTyping)
            {
                // When there is no dialogue, disable dialogue box
                if (npc1CurrentLine >= npc1EndLine && npc2CurrentLine >= npc2EndLine)
                {
                    DisableDialogueBox();
                }
                else
                {
                    //NPC1 Turn
                    if (npc1Turn)
                    {
                        StartCoroutine(TextScroll(npc1DialogueList[npc1CurrentLine]));
                        npc1Turn = false;
                        npc2Turn = true;
                        npc1CurrentLine += 1;
                    }
                    //NPC2 Turn
                    else if (npc2Turn)
                    {
                        StartCoroutine(TextScroll(npc2DialogueList[npc2CurrentLine]));
                        npc2Turn = false;
                        npc1Turn = true;
                        npc2CurrentLine += 1;
                    }
                }
            }
            else if( isTyping && !cancelTyping)
            {
                cancelTyping = true;

            }         
        }

        if (!isActive)
        {
            return;
        }
    }

    private void ClassifyDialogue(string[] texts)
    {
        for(int i = 0; i < texts.Length; ++i)
        {
            if(texts[i].Contains("NPC1:"))
            {
                // Add string except name
                npc1DialogueList.Add(texts[i].Substring(5, texts[i].Length - 5));
            }
            else if(texts[i].Contains("NPC2:"))
            {
                // Add string except name
                npc2DialogueList.Add(texts[i].Substring(5, texts[i].Length - 5));
            }

        }
    }

    private IEnumerator TextScroll (string lineOfText)
    {
        int letter = 0;
        texts.text = "";
        isTyping = true;
        cancelTyping = false;

        // Type text one letter at a time by speed
        while(isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            if (npc1Turn)
                texts.color = Color.blue;
            else if (npc2Turn)
                texts.color = Color.red;

            texts.text += lineOfText[letter];
            ++letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        texts.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }

    public void EnableDialogueBox()
    {
        textBox.SetActive(true);
        isActive = true;
    }

    public void DisableDialogueBox()
    {
        textBox.SetActive(false);
        isActive = false;

    }
}
