using TMPro;
using UnityEngine;

public class WinCount : MonoBehaviour
{
    [SerializeField] public int CountToWin = 5;
    private TextMeshProUGUI _textCount;

    private void Start()
    {
        _textCount = gameObject.GetComponent<TextMeshProUGUI>();
        _textCount.text = CountToWin.ToString();
    }

    public void UpCount()
    {
        if (CountToWin < 99)
        {
            CountToWin++;
            _textCount.text = CountToWin.ToString();
        }
    }
    
    public void DownCount()
    {
        if(CountToWin > 0) 
        { 
            CountToWin--;
            _textCount.text = CountToWin.ToString();
        }
    }
}
