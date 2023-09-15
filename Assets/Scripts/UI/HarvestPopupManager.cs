using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class HarvestPopupManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _popupPrefab;
    [SerializeField]
    private Canvas _canvas;
    public float FadeInTime;
    public float FadeOutTime;
    public float TravelTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerGainItemText(string itemName, Color itemColor)
    {
        GameObject _newPopup = Instantiate(_popupPrefab, Vector3.zero, Quaternion.identity, _canvas.transform);
        _newPopup.transform.localPosition = new Vector3(0, 80, 0);
        Text _textField = _newPopup.GetComponent<Text>();
        _textField.text = "+" + itemName;
        _textField.color = itemColor;
        StartCoroutine(MoveText(_textField));
    }

    public void TriggerInventoryFullText()
    {
        GameObject _newPopup = Instantiate(_popupPrefab, Vector3.zero, Quaternion.identity, _canvas.transform);
        _newPopup.transform.localPosition = new Vector3(0, 80, 0);
        Text _textField = _newPopup.GetComponent<Text>();
        _textField.text = "Cargo Bay Full";
        _textField.color = Color.red;
        StartCoroutine(MoveText(_textField));
    }

    private IEnumerator MoveText(Text text)
    {
        text.transform.rotation = new Quaternion(0, 0, 0, 0);
        Vector3 startingPos = text.transform.localPosition;
        
        float currentTime = 0;
        float normalizedTime;

        StartCoroutine(FadeTextToFullAlpha(FadeInTime, text));

        while (currentTime <= TravelTime)
        {
            currentTime += Time.deltaTime;

            normalizedTime = currentTime / TravelTime;
            text.transform.localPosition = Vector3.Lerp(startingPos, startingPos + new Vector3(0, 100, 0), normalizedTime);
            yield return null;
        }
        
        Destroy(text.gameObject);
        yield return null;
    }

    public IEnumerator FadeTextToFullAlpha(float fadeTime, Text text)
    {
        
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / fadeTime));
            yield return null;
        }

        float currentTime = 0;

        while (currentTime <= TravelTime - (FadeOutTime + FadeInTime + 0.05))
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(FadeTextToZeroAlpha(FadeOutTime, text));
    }

    public IEnumerator FadeTextToZeroAlpha(float fadeTime, Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / fadeTime));
            yield return null;
        }
    }
}
