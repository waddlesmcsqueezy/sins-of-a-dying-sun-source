using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerControlScript : MonoBehaviour
{
    private Vector2 _move = Vector2.zero;

    [SerializeField] 
    private bool _playerControlAuthorityFlag = true;

    public bool IsDocked = false;

    [SerializeField] 
    private BasicSonar _sonar;

    [SerializeField]
    private BasicEngine _engine;

    [SerializeField]
    private GameObject _spotlight;

    public bool SpotlightEnabled => _spotlightLightSource.enabled;

    private Light2D _spotlightLightSource;

    private AudioSource _spotlightOnAudioSource;
    private AudioSource _spotlightOffAudioSource;

    public Rigidbody2D rb;

    private Quaternion _previousRotation;

    [SerializeField]
    private GameObject _drill;

    [SerializeField]
    private List<Item> _inventory;

    public List<Item> Inventory { get; }
    public List<Item> InventorySize { get; set; }

    private int _currentHealth;
    private int _maxHealth;

    public int CurrentHealth => this._currentHealth;
    public int MaxHealth => this._maxHealth;

    public void InventoryAddItem(Item item)
    {
        _inventory.Add(item);

        switch (item.Effect)
        {
            case Item.EffectCategory.Energy:
                _engine.EnergySupply.AddEnergy(item.EffectAmount);
                break;
            case Item.EffectCategory.MaxEnergy:
                _engine.EnergySupply.IncreaseMaxEnergy(item.EffectAmount);
                break;
            case Item.EffectCategory.Health:
                Heal((int)item.EffectAmount);
                break;
            case Item.EffectCategory.MaxHealth:
                IncreaseMaxHealth((int)item.EffectAmount);
                break;
            case Item.EffectCategory.EngineSpeedMod:
                _engine.IncreaseEngineSpeedMod(item.EffectAmount);
                break;
            case Item.EffectCategory.EnergyUsage:
                _engine.DecreaseEnergyUsage(item.EffectAmount);
                break;
            case Item.EffectCategory.GeneratorSpeed:
                _engine.IncreaseGeneratorSpeed(item.EffectAmount);
                break;
            case Item.EffectCategory.DrillLevel:
                _drill.GetComponent<Drill>().SetLevel((int)item.EffectAmount);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _previousRotation = _spotlight.transform.rotation;
        _spotlightLightSource = GameObject.FindGameObjectWithTag("Spotlight").GetComponent<Light2D>();
        _spotlightOnAudioSource = GameObject.FindGameObjectWithTag("SpotlightOnAudioSource").GetComponent<AudioSource>();
        _spotlightOffAudioSource = GameObject.FindGameObjectWithTag("SpotlightOffAudioSource").GetComponent<AudioSource>();
        _inventory = new List<Item>();

        _maxHealth = 25;
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerControlAuthorityFlag)
        {
            if (_engine.IsRunning)
            {
                // TURBO ENGINE MODE
                _engine.IsTurbo = Input.GetKey(KeyCode.LeftShift);

                _move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

                if (_move.magnitude > 1) _move /= _move.magnitude;

                _move *= _engine.GetMoveSpeedMod();

                // TELL ENGINE IF WE ARE SENDING INPUT
                if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0)
                    _engine.IsReceivingInput = true;
                else
                    _engine.IsReceivingInput = false;

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    _sonar.UseSonarPing();
                }

                // TOGGLE SPOTLIGHT ON/OFF
                if (Input.GetMouseButtonDown(1))
                {
                    ToggleSpotlight();
                }

                if (Input.GetMouseButton(0) && !_drill.GetComponent<Drill>().InUse)
                {
                    _drill.GetComponent<Drill>().TurnOn();
                }
                else if (!Input.GetMouseButton(0) && _drill.GetComponent<Drill>().InUse)
                {
                    _drill.GetComponent<Drill>().TurnOff();
                }
            }
            else
            {
                // ENGINE IS OFF - FORCE NO MOVEMENT

                _move = Vector2.zero;

                // TURN OFF SPOTLIGHT
                if (_spotlightLightSource.enabled)
                    ToggleSpotlight();
            }

            // ACTIVE SONAR KEY
            

            // TOGGLE ENGINE ON/OFF
            if (Input.GetKeyDown(KeyCode.P))
            {
                _engine.ToggleEngine();
            }


            // SPOTLIGHT AIM AT MOUSE CURSOR
            Vector3 lookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(_spotlight.transform.position);
            //mouse_pos.z = 5.23f; //The distance between the camera and object
            float zAngle = (Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg);

            

            _previousRotation = Quaternion.Lerp(_previousRotation,
            Quaternion.AngleAxis(zAngle, Vector3.forward), Time.deltaTime * 5.0f);
            _spotlight.transform.rotation = _previousRotation;
            
                
            _drill.transform.rotation = _previousRotation;
            
            

        }

    }

    void FixedUpdate()
    {
        // APPLY MOVEMENT FORCE
        Vector3 inputVector = _move;
        rb.AddForce(new Vector2(inputVector.x,inputVector.y));
    }

    public void ToggleControls()
    {
        _playerControlAuthorityFlag = !_playerControlAuthorityFlag;
    }

    public void ToggleSpotlight()
    {
        _spotlightLightSource.enabled = !_spotlightLightSource.enabled;

        if (_spotlightLightSource.enabled)
            _spotlightOnAudioSource.Play();
        else
            _spotlightOffAudioSource.Play();
    }

    public void IncreaseMaxHealth(int units)
    {
        _maxHealth += units;
    }

    public void Heal(int units)
    {
        if (_currentHealth + units > _maxHealth)
             _currentHealth = _maxHealth;
        else _currentHealth += units;
    }

    public void Damage(int units)
    {
        if (_currentHealth - units < 0)
        {
            //kill
        }
        else
        {
            _currentHealth -= units;
        }

        Debug.Log("health is now " + _currentHealth + " out of " + _maxHealth + " after taking " + units + " damage.");
    }
}
