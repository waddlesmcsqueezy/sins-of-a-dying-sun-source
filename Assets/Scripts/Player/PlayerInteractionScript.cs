using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    private bool _touchingHarvestable = false;
    public Harvestable _availableHarvestable = null;

    private bool _touchingInteractable = false;
    public Interactable AvailableInteractable = null;

    public InteractGUI InteractGui;

    [SerializeField]
    private HarvestPopupManager _harvestPopupManager;

    [SerializeField]
    private PlayerInventory _playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        InteractGui.HideInteractPrompt();
    }

    // Update is called once per frame
    void Update()
    {
        if (_touchingHarvestable)
        {
            InteractGui.ShowInteractPrompt($"Harvest {_availableHarvestable.HarvestableName}");

            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerControlScript>().InventoryAddItem(_availableHarvestable.HarvestEffect());
                _harvestPopupManager.TriggerGainItemText(_availableHarvestable.HarvestableName, _availableHarvestable.HarvestableColor);
                Destroy(_availableHarvestable.gameObject);
            }

        } 
        else if (_touchingInteractable)
        {
            var prompt = AvailableInteractable.InteractablePrompt;
            InteractGui.ShowInteractPrompt($"{prompt} {AvailableInteractable.InteractableName}");
            if (Input.GetKeyDown(KeyCode.F))
            {
                
                AvailableInteractable.Interact();
            }
        } 
        else { InteractGui.HideInteractPrompt(); }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Harvestable")
        {
            Debug.Log(other.name);
            _availableHarvestable = other.GetComponent<Harvestable>();
            

            _touchingHarvestable = true;
        }

        if (other.tag == "Interactable")
        {
            AvailableInteractable = other.GetComponent<Interactable>();


            _touchingInteractable = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Harvestable")
        {
            _touchingHarvestable = false;
            _availableHarvestable = null;
        }

        if (other.tag == "Interactable")
        {
            _touchingInteractable = false;
            AvailableInteractable = null;
        }
    }
}
