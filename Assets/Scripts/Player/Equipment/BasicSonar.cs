using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicSonar : MonoBehaviour
{
    [SerializeField]
    private AudioSource _pingAudioSource;
    [SerializeField]
    private GameObject _smallResponseAudioSourcePrefab;
    [SerializeField]
    private GameObject _mediumResponseAudioSourcePrefab;
    [SerializeField]
    private GameObject _largeResponseAudioSourcePrefab;

    [SerializeField] 
    private float _cooldown;
    [SerializeField] 
    private float _waveSpeed;

    private bool _isAvailable;


    public string TagMask;
    private List<GameObject> _inRangeObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _isAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseSonarPing()
    {
        if (_isAvailable)
        {
            _pingAudioSource.Play();
            EvalResponse();
            StartCoroutine(CooldownTimer());
        }
        else
        {
            Debug.Log("Sonar Ping on Cooldown");
        }
    }

    private IEnumerator CooldownTimer()
    {
        _isAvailable = false;
        yield return new WaitForSeconds(_cooldown);
        _isAvailable = true;
    }

    public void EvalResponse()
    {
        if (_inRangeObjects.Count > 0)
        {
            _inRangeObjects.ForEach(delegate(GameObject obj)
            {
                StartCoroutine(ResponseTimer(obj));
            });
        }
    }

    private IEnumerator ResponseTimer(GameObject target)
    {
        float targetDistance = Vector3.Distance(transform.position, target.transform.position);
        yield return new WaitForSeconds(targetDistance / _waveSpeed);
        GameObject _responseAudioSource;
        if (target.GetComponent<Large>())
            _responseAudioSource = Instantiate(_largeResponseAudioSourcePrefab, target.transform.position, Quaternion.identity);
        else if (target.GetComponent<Small>())
            _responseAudioSource = Instantiate(_smallResponseAudioSourcePrefab, target.transform.position, Quaternion.identity);
        else
            _responseAudioSource = Instantiate(_mediumResponseAudioSourcePrefab, target.transform.position, Quaternion.identity);
        //_responseAudioSource.transform.localPosition = Vector3.zero;
        _responseAudioSource.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(_responseAudioSource.GetComponent<AudioSource>().clip.length);
        Destroy(_responseAudioSource);
    }


    

    void OnDisable()
    {
        _inRangeObjects.Clear();
    }

    void OnTriggerEnter2D(Collider2D other) //change to 2d for 2d
    {
        //Debug.Log("Checking for enemies in trigger");
        if (!other.gameObject.GetComponent<Pingable>()) return;

        _inRangeObjects.Add(other.gameObject);
        _inRangeObjects.Sort(CompareDistances);
        //Debug.Log("Object " + other.name + " in range of detection");
    }

    void OnTriggerExit2D(Collider2D other) //change to 2d for 2d
    {
        if (!other.gameObject.GetComponent<Pingable>()) return;

        if (_inRangeObjects.Count > 0)
            _inRangeObjects.RemoveAt(_inRangeObjects.Count - 1);
        //Debug.Log("Remove object " + other.name + " from detection queue");
    }

    private int CompareDistances(GameObject a, GameObject b)
    {
        float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredRangeA.CompareTo(squaredRangeB);
    }
}
