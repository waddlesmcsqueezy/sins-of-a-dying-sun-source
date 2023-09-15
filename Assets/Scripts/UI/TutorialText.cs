using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;

public class TutorialText : MonoBehaviour
{
    [SerializeField]
    private GameObject _textBoxPrefab;
    private GameObject _textBox;
    [SerializeField]
    private Canvas _canvas;
    public float FadeInTime = 2.0f;
    public float FadeOutTime = 2.0f;
    public float TravelTime = 5.0f;

    private Rigidbody2D _trigger;

    public bool Repeatable = true;

    // Start is called before the first frame update
    void Start()
    {
        _trigger = GetComponent<Rigidbody2D>();
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnText()
    {

        _textBox = Instantiate(_textBoxPrefab, Vector3.zero, Quaternion.identity, _canvas.transform);
        _textBox.transform.localPosition = new Vector3(-320, 0, 0);
        StartCoroutine(FadeTextToFullAlpha(FadeInTime, _textBox.GetComponent<TextMeshProUGUI>()));
    }


    public IEnumerator FadeTextToFullAlpha(float fadeTime, TextMeshProUGUI text)
    {

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / fadeTime));
            yield return null;
        }
        /*
        float currentTime = 0;

        while (currentTime <= TravelTime - (FadeOutTime + FadeInTime + 0.05))
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        */
        yield return null;
    }

    public IEnumerator FadeTextToZeroAlpha(float fadeTime, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / fadeTime));
            yield return null;
        }
        if (text) 
            Destroy(text.gameObject);
    }

    void OnTriggerEnter2D()
    {
        if (!_textBox)
            SpawnText();
    }

    void OnTriggerExit2D()
    {
        if (Repeatable)
        {
            StartCoroutine(FadeTextToZeroAlpha(FadeOutTime, _textBox.GetComponent<TextMeshProUGUI>()));
        }
    }
}
