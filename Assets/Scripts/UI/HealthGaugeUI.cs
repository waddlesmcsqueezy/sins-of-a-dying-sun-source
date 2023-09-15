using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthGaugeUI : MonoBehaviour
{

    private RectTransform _gaugeForeground;
    // Start is called before the first frame update
    void Start()
    {
        _gaugeForeground = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControlScript player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControlScript>();
        _gaugeForeground.sizeDelta = new Vector2(_gaugeForeground.rect.width, 300 * ((float)player.CurrentHealth / (float)player.MaxHealth));
    }
}
