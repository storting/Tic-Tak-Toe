using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class StyleManager : MonoBehaviour
{
    private SpriteRenderer srBG;

    private SpriteRenderer _redQueue;
    public SpriteRenderer _blueQueue;
    private GameObject[] _redBut;
    private GameObject[] _blueBut;

    public UnityEngine.UI.Image RedWinner;
    public UnityEngine.UI.Image BlueWinner;

    public Sprite NeonBG;
    public Sprite ColdBG;
    public Sprite FootbalBG;

    public Sprite CrossFootbalRed;
    public Sprite CrossColdRed;
    public Sprite CrossNeonRed;

    public Sprite CrossFootbalBlue;
    public Sprite CrossColdBlue;
    public Sprite CrossNeonBlue;

    public UnityEngine.UI.Image NewGameBut;

    private void Start()
    {
        srBG = GameObject.FindGameObjectWithTag("BackgroundSprite").GetComponent<SpriteRenderer>();
        _redQueue = GameObject.FindGameObjectWithTag("RedQueue").GetComponent<SpriteRenderer>();

        FindAllTaggedObjects();
    }

    public void ChangeAllSprites(Sprite redSprite, Sprite blueSprite)
    {
        if (_redBut != null)
        {
            foreach (GameObject redObj in _redBut)
            {
                if (redObj == null) continue;
                UnityEngine.UI.Image image = redObj.GetComponent<UnityEngine.UI.Image>();
                if (image != null)
                {
                    image.sprite = redSprite;
                }
                else
                {
                    Debug.LogWarning($"No Image component on red object: {redObj.name}");
                }
            }
        }
        else
        {
            Debug.LogError("RedBut array is null");
        }

        if (_blueBut != null)
        {
            foreach (GameObject blueObj in _blueBut)
            {
                if (blueObj == null) continue;
                UnityEngine.UI.Image image = blueObj.GetComponent<UnityEngine.UI.Image>();
                if (image != null)
                {
                    image.sprite = blueSprite;
                }
                else
                {
                    Debug.LogWarning($"No Image component on blue object: {blueObj.name}");
                }
            }
        }
        else
        {
            Debug.LogError("BlueBut array is null");
        }
    }

    void FindAllTaggedObjects()
    {
        List<GameObject> redList = new List<GameObject>();
        List<GameObject> blueList = new List<GameObject>();

        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("RedBut") && obj.scene.IsValid())
            {
                redList.Add(obj);
            }
            else if (obj.CompareTag("BlueBut") && obj.scene.IsValid())
            {
                blueList.Add(obj);
            }
        }

        _redBut = redList.ToArray();
        _blueBut = blueList.ToArray();
    }

    public void ChangeButtonColor(UnityEngine.UI.Image but, string HEX)
    {
        if (but != null)
        {
            Color newColor;
            ColorUtility.TryParseHtmlString(HEX, out newColor);

            but.color = newColor;
        }
    }

    public void SetNeon()
    {
        srBG.sprite = NeonBG;
        _redQueue.sprite = CrossNeonRed;
        _blueQueue.sprite = CrossNeonBlue;
        RedWinner.sprite = CrossNeonRed;
        BlueWinner.sprite = CrossNeonBlue;
        ChangeAllSprites(CrossNeonRed, CrossNeonBlue);
        ChangeButtonColor(NewGameBut, "#C6185A");
    }
    public void SetCold()
    {
        srBG.sprite = ColdBG;
        _redQueue.sprite = CrossColdRed;
        _blueQueue.sprite = CrossColdBlue;
        RedWinner.sprite = CrossColdRed;
        BlueWinner.sprite = CrossColdBlue;
        ChangeAllSprites(CrossColdRed, CrossColdBlue);
        ChangeButtonColor(NewGameBut, "#8484FF");
    }

    public void SetFootbal()
    {
        srBG.sprite = FootbalBG;
        _redQueue.sprite = CrossFootbalRed;
        _blueQueue.sprite = CrossFootbalBlue;
        RedWinner.sprite = CrossFootbalRed;
        BlueWinner.sprite = CrossFootbalBlue;
        ChangeAllSprites(CrossFootbalRed, CrossFootbalBlue);
        ChangeButtonColor(NewGameBut, "#DDC529");
    } 
}
