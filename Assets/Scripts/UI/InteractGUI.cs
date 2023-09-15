using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractGUI : MonoBehaviour
{

    [SerializeField] private Image InteractKeyImage;
    [SerializeField] private Text InteractKeyText;
    [SerializeField] private Text InteractPromptText;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void ShowInteractPrompt(string interactionText)
    {
        InteractKeyImage.enabled = true;
        InteractKeyText.enabled = true;
        InteractPromptText.enabled = true;
        InteractPromptText.text = interactionText;
    }

    public void HideInteractPrompt()
    {
        InteractKeyImage.enabled = false;
        InteractKeyText.enabled = false;
        InteractPromptText.enabled = false;
    }
}
