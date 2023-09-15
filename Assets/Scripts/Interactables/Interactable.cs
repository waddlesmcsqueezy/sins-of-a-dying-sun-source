using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    private string _interactableName;
    [SerializeField]
    private string _interactablePrompt;

    public string InteractableName => _interactableName;
    public string InteractablePrompt => _interactablePrompt;


    public abstract void Interact();
}
