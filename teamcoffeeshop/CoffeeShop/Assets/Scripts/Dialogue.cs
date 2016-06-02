using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Dialogue : MonoBehaviour {

    private Text _textComponent;

    public string[] DialogueStrings;

    public float SecondsBetweenCharacters = 0.15f;
    public float CharacterRateMultiplier = 0.5f;

    public KeyCode DialogueInput = KeyCode.Return;

    private bool _isStringBeingRevealed = false;
    private bool _isDialoguePlaying = false;


	// Use this for initialization
	void Start () {
        _textComponent = GetComponent<Text>();
        _textComponent.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(!_isStringBeingRevealed)
            {
                _isStringBeingRevealed = true;
                StartCoroutine(DisplayString(DialogueStrings[0]));
            }
        }
	}

    private IEnumerator DisplayString(string stringToDisplay)
    {
        int stringLength = stringToDisplay.Length;
        int currentCharacterIndex = 0;

        _textComponent.text = "";

        while(currentCharacterIndex < stringLength)
        {
            _textComponent.text += stringToDisplay[currentCharacterIndex];
            currentCharacterIndex++;

            if (currentCharacterIndex < stringLength)
            {
                if (Input.GetKey(DialogueInput))
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters * CharacterRateMultiplier);
                }
                else
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters);
                }
            }
            else
            {
                break;
            }
        }

        while(true)
        {
            if(Input.GetKeyDown(DialogueInput))
            {
                break;
            }

            yield return 0;
        }

        _isStringBeingRevealed = false;
        _textComponent.text = "";
    }
}
