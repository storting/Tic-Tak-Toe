using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //0 - Red
    //1 - Blue
    public int _numPlayer = 0;

    public GameObject WinnerBg;
    public GameObject RedWinnerText;
    public GameObject BlueWinnerText;

    private GameObject redQueue;
    private GameObject blueQueue;

    public AudioSource AudioSourceWin;
    public AudioSource AudioSourceWinLine;

    [SerializeField] public Button[] buttons;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Button restartButton;

    private string[] board = new string[9];
    private bool gameActive = true;

    public int PlayerWinCountRed = 0;
    public int PlayerWinCountBlue = 0;

    private WinCount _Wcount;
    private void Start()
    {
        redQueue = GameObject.FindGameObjectWithTag("RedQueue");
        blueQueue = GameObject.FindGameObjectWithTag("qwe");
        FindWinCountObject("WinCountScrt");
        InitializeBoard();
        UpdateQueueUI(); 
    }

    void FindWinCountObject(string tag)
    {
        _Wcount = GameObject.FindGameObjectWithTag(tag)?.GetComponent<WinCount>();
        if (_Wcount == null)
        {
            WinCount[] allWinCounts = Resources.FindObjectsOfTypeAll<WinCount>();
            foreach (WinCount wc in allWinCounts)
            {
                if (wc.CompareTag(tag))
                {
                    _Wcount = wc;
                    break;
                }
            }
        }

        if (_Wcount == null)
        {
            Debug.LogError("Не удалось найти объект WinCount с тегом 'WinCountScrt'");
        }
    }

    void InitializeBoard()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => MakeMove(index));
            board[i] = "";
        }

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGameEnd);
    }

    public void MakeMove(int position)
    {
        if (!gameActive || board[position] != "") return;

        int currentPlayerBeforeMove = _numPlayer;
        string playerSymbol = (_numPlayer == 0) ? "X" : "O";

        board[position] = playerSymbol;
        buttons[position].interactable = false;

        if (CheckWinner(playerSymbol))
        {
            string winnerName = (currentPlayerBeforeMove == 0) ? "Red" : "Blue";
            statusText.text = $"{winnerName}";
            UpdateQueueUI();

            if (currentPlayerBeforeMove == 0) PlayerWinCountRed++;
            else PlayerWinCountBlue++;
            gameActive = false;
            if (PlayerWinCountRed >= _Wcount.CountToWin || PlayerWinCountBlue >= _Wcount.CountToWin)
            {
                string score = $"<color=red>{PlayerWinCountRed}</color> - <color=blue>{PlayerWinCountBlue}</color>";
                statusText.text = $"{score}";
                WinnerBg.SetActive(true);
                Invoke("PlaySound", 1f);
                if (PlayerWinCountRed > PlayerWinCountBlue)
                {
                    RedWinnerText.SetActive(true);
                }
                else if (PlayerWinCountRed < PlayerWinCountBlue)
                {
                    BlueWinnerText.SetActive(true);
                }
            }
            else
            {
                Invoke("RestartGame", 1.5f);
            }            
        }
        else if (IsBoardFull())
        {
            statusText.text = "ALL";
            gameActive = false;
            Invoke("RestartGame", 1.5f);
        }
        else
        {
            _numPlayer = (_numPlayer == 0) ? 1 : 0;
            UpdateQueueUI();
            UpdateStatusText();
        }
    }

    bool CheckWinner(string player)
    {
        int[,] winPatterns = {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
            {0, 4, 8}, {2, 4, 6}
        };

        for (int i = 0; i < winPatterns.GetLength(0); i++)
        {
            int a = winPatterns[i, 0];
            int b = winPatterns[i, 1];
            int c = winPatterns[i, 2];

            if (!string.IsNullOrEmpty(board[a]) &&
                !string.IsNullOrEmpty(board[b]) &&
                !string.IsNullOrEmpty(board[c]) &&
                board[a] == player &&
                board[b] == player &&
                board[c] == player)
            {
                SetButtonAlpha(1f, a);
                SetButtonAlpha(1f, b);
                SetButtonAlpha(1f, c);
                AudioSourceWinLine.Play();
                return true;
            }
        }
        return false;
    }

    public void SetButtonAlpha(float alpha, int index)
    {
        alpha = Mathf.Clamp01(alpha);

        Image mainImage = buttons[index].GetComponent<Image>();
        if (mainImage != null)
        {
            Color color = mainImage.color;
            color.a = alpha;
            mainImage.color = color;
        }
    }

    bool IsBoardFull()
    {
        foreach (string cell in board)
        {
            if (string.IsNullOrEmpty(cell)) return false;
        }
        return true;
    }

    void UpdateStatusText()
    {
        string score = $"<color=red>{PlayerWinCountRed}</color> - <color=blue>{PlayerWinCountBlue}</color>";
        statusText.text = $"{score}";

    }

    void UpdateQueueUI()
    {
        if (_numPlayer == 0) { redQueue.SetActive(true); blueQueue.SetActive(false); }
        else if (_numPlayer == 1) { blueQueue.SetActive(true); redQueue.SetActive(false); }

        UpdateStatusText();
    }

    GameObject FindChildByTag(GameObject parent, string tag)
    {
        return parent.transform.Cast<Transform>()
                       .FirstOrDefault(child => child.CompareTag(tag))?
                       .gameObject;
    }

    public void RestartGame()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            board[i] = "";
            buttons[i].interactable = true;

            GameObject redBut = FindChildByTag(buttons[i].gameObject, "RedBut");
            GameObject blueBut = FindChildByTag(buttons[i].gameObject, "BlueBut");
            if (redBut != null) redBut.SetActive(false);
            if (blueBut != null) blueBut.SetActive(false);
            SetButtonAlpha(0f, i);
        }

        _numPlayer = 0;
        gameActive = true;
        UpdateQueueUI();
    }
    public void RestartGameEnd()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            board[i] = "";
            buttons[i].interactable = true;

            GameObject redBut = FindChildByTag(buttons[i].gameObject, "RedBut");
            GameObject blueBut = FindChildByTag(buttons[i].gameObject, "BlueBut");
            if (redBut != null) redBut.SetActive(false);
            if (blueBut != null) blueBut.SetActive(false);
            SetButtonAlpha(0f, i);
        }
        WinnerBg.SetActive(false);
        RedWinnerText.SetActive(false);
        BlueWinnerText.SetActive(false);
        PlayerWinCountBlue = 0;
        PlayerWinCountRed = 0;
        _numPlayer = 0;
        gameActive = true;
        UpdateQueueUI();
    }

    public void PlaySound()
    {
        AudioSourceWin.Play();
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
