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


    public Sprite NeonBG;
    public Sprite ColdBG;
    public Sprite FootbalBG;

    public Sprite CrossFootbalRed;
    public Sprite CrossColdRed;
    public Sprite CrossNeonRed;

    public Sprite CrossFootbalBlue;
    public Sprite CrossColdBlue;
    public Sprite CrossNeonBlue;

    private void Start()
    {
        srBG = GameObject.FindGameObjectWithTag("BackgroundSprite").GetComponent<SpriteRenderer>();
        _redQueue = GameObject.FindGameObjectWithTag("RedQueue").GetComponent<SpriteRenderer>();

        FindAllTaggedObjects();
    }

    public void ChangeAllSprites(Sprite redSprite, Sprite blueSprite)
    {
        Debug.Log("Func init");
        
        // Обрабатываем красные объекты
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

        // Обрабатываем синие объекты
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

        // Находим все объекты (включая неактивные) на сцене
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Проверяем, что объект имеет тег и он находится в сцене (не префаб)
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

        Debug.Log($"Found {_redBut.Length} RedBut objects (including inactive)");
        Debug.Log($"Found {_blueBut.Length} BlueBut objects (including inactive)");
    }

    public void SetNeon()
    {
        srBG.sprite = NeonBG;
        _redQueue.sprite = CrossNeonRed;
        _blueQueue.sprite = CrossNeonBlue;
        ChangeAllSprites(CrossNeonRed, CrossNeonBlue);
    }
    public void SetCold()
    {
        srBG.sprite = ColdBG;
        _redQueue.sprite = CrossColdRed;
        _blueQueue.sprite = CrossColdBlue;
        ChangeAllSprites(CrossColdRed, CrossColdBlue);
    }

    public void SetFootbal()
    {
        srBG.sprite = FootbalBG;
        _redQueue.sprite = CrossFootbalRed;
        _blueQueue.sprite = CrossFootbalBlue;
        ChangeAllSprites(CrossFootbalRed, CrossFootbalBlue);
    }

    
}
