using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogueManager: MonoBehaviour
{
    public TextAsset inkAsset;
    public GameObject textBox;
    public GameObject customButton;
    public GameObject optionPanel;

    static Story _inkStory;
    Text nametag;
    Text message;
    static Choice choice;

    //for later implementation, so that you can put #tags in inky to set text color and so on.
    //List<string> tags;

    private void Start()    //calls before first frame
    {
        //init
        _inkStory = new Story(inkAsset.text);
        nametag = textBox.transform.GetChild(0).GetComponent<Text>();
        message = textBox.transform.GetChild(1).GetComponent<Text>();
        choice = null;
        //init tags
    }

    private void Update()   //calls every frame
    {
        if(Input.GetButtonDown("Submit"))
        {
            //Is there more to the story?
            if(_inkStory.canContinue)   //this should be done with a loop but we put it in update so it works?
            {
                nametag.text = "Spark";
                AdvanceDialogue();

                //Are there any choices?
                if (_inkStory.currentChoices.Count != 0)
                {
                    StartCoroutine(ShowChoices());  //learn how to do choices
                }
            }
            else
            {
                FinishDialogue();
            }
        }
    }

    private void AdvanceDialogue()
    {
        string currentSentence = _inkStory.Continue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    private void FinishDialogue()
    {
        Debug.Log("End of Dialogue.");
    }

    //Type out the sentence letter by letter
    IEnumerator TypeSentence(string sentence)
    {
        message.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return null;
        }
        yield return null;
    }

    //Create then show the choices on the screen until one got selected
    IEnumerator ShowChoices()
    {
        Debug.Log("There are choices need to be made here!");
        List<Choice> _choices = _inkStory.currentChoices;

        for (int i = 0; i < _choices.Count; i++)
        {
            GameObject temp = Instantiate(customButton, optionPanel.transform);
            temp.transform.GetChild(0).GetComponent<Text>().text = _choices[i].text;
            temp.AddComponent<Selectable>();
            temp.GetComponent<Selectable>().element = _choices[i];
            temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });
        }

        optionPanel.SetActive(true);

        yield return new WaitUntil(() => { return choice != null; });

        AdvanceFromDecision();
    }

     // Tells the story which branch to go to
    public static void SetDecision(object element)
    {
        choice = (Choice)element;
        _inkStory.ChooseChoiceIndex(choice.index);
    }

    // After a choice was made, turn off the panel and advance from that choice
    void AdvanceFromDecision()
    {
        optionPanel.SetActive(false);
        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        choice = null; //Forgot to reset the choice. Otherwise, it would select an option without player intervention.
        AdvanceDialogue();
    }

    void parseTags() 
    {
        //check inkAsset for #tags
        //parse for #textcolor, #name, #namecolor etc.
        //using a switch

    }
}