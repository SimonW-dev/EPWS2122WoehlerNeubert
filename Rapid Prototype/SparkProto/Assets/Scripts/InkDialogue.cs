using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkDialogue : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    public Text textPrefab;
    public Button buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON.text);
        refreshUI();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void refreshUI()
    {
        //clear old UI
        eraseUI();

        //Make and show Dialogue Text
        Text storyText = Instantiate(textPrefab) as Text; //make textPrefab into variable
        string text = loadStoryChunk();

        //tags
        List<string> tags = story.currentTags;
        if (tags.Count > 0)
        {
            text = tags[0] + "\n" + text; //name display
        }
        
        //put the text on screen relative to parent (rn: canvas)
        storyText.text = text;
        storyText.transform.SetParent(this.transform, false);
        
        //Make Buttons for choices
        foreach (Choice currentChoice in story.currentChoices) 
        {
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            Text choiceText = buttonPrefab.GetComponentInChildren<Text>(); //Buttons have a Text Component as a Child
            choiceText.text = currentChoice.text;
            choiceButton.transform.SetParent(this.transform, false);

            //Add Button Interaction OnClick
            //delegate allows you to set a method as the parameter for a method (callback?)
            choiceButton.onClick.AddListener( delegate { ChooseStoryChoice(currentChoice); }); 
        }

    }

    void eraseUI()
    {
        //cant use foreach for transform
        for(int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }

    }

    string loadStoryChunk()
    {
        if (story.canContinue) {
            return story.ContinueMaximally();
        }
        else return "end of dialogue";
    }

    void ChooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        refreshUI();
    }

}
