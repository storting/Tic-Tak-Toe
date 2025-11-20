using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Playing : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void OnClick()
    {
        GameObject playerRed = FindChildByTagInChildren("RedBut");
        GameObject playerBlue = FindChildByTagInChildren("BlueBut");

        if (!playerBlue.activeInHierarchy && !playerRed.activeInHierarchy)
        {
            int buttonIndex = GetButtonIndex();

            if (buttonIndex != -1)
            {
                int currentPlayerBeforeMove = _gameManager._numPlayer;

                _gameManager.MakeMove(buttonIndex);

                if (currentPlayerBeforeMove == 0)
                {
                    playerRed.SetActive(true);
                    playerBlue.SetActive(false);
                }
                else
                {
                    playerRed.SetActive(false);
                    playerBlue.SetActive(true);
                }
            }
        }
    }

    int GetButtonIndex()
    {
        for (int i = 0; i < _gameManager.buttons.Length; i++)
        {
            if (_gameManager.buttons[i].gameObject == this.gameObject)
                return i;
        }
        return -1;
    }

    public GameObject FindChildByTagInChildren(string tag)
    {
        return transform.Cast<Transform>()
                       .FirstOrDefault(child => child.CompareTag(tag))?
                       .gameObject;
    }
}
