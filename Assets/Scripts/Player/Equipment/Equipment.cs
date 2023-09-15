using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : MonoBehaviour
{
    [SerializeField] protected int _level = 0;
    [SerializeField] protected Sprite _tier0Sprite;
    [SerializeField] protected Sprite _tier1Sprite;
    [SerializeField] protected Sprite _tier2Sprite;
    [SerializeField] protected Sprite _tier3Sprite;
    [SerializeField] protected Sprite _tier4Sprite;

    public int Level => this._level;

    void Start()
    {
        UpdateSprite();
    }

    public void SetLevel(int units)
    {
        _level = units;

        UpdateSprite();
    }

    public void UpdateSprite()
    {
        switch (_level)
        {
            case 0:
                gameObject.GetComponent<SpriteRenderer>().sprite = _tier0Sprite;
                break;
            case 1:
                gameObject.GetComponent<SpriteRenderer>().sprite = _tier1Sprite;
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = _tier2Sprite;
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().sprite = _tier3Sprite;
                break;
            case 4:
                gameObject.GetComponent<SpriteRenderer>().sprite = _tier4Sprite;
                break;
            default:
                gameObject.GetComponent<SpriteRenderer>().sprite = _tier0Sprite;
                break;
        }


    }
}
