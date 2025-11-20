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
        // Находим дочерние объекты
        GameObject playerRed = FindChildByTagInChildren("RedBut");
        GameObject playerBlue = FindChildByTagInChildren("BlueBut");

        // Проверяем, что обе картинки выключены (клетка пустая)
        if (!playerBlue.activeInHierarchy && !playerRed.activeInHierarchy)
        {
            // Получаем индекс этой кнопки
            int buttonIndex = GetButtonIndex();

            if (buttonIndex != -1)
            {
                // ЗАПОМИНАЕМ текущего игрока ДО хода
                int currentPlayerBeforeMove = _gameManager._numPlayer;

                // Вызываем MakeMove
                _gameManager.MakeMove(buttonIndex);

                // Включаем картинку того игрока, который делал ход
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
