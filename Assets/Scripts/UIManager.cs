using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI guideText;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        InitializeUI();
        Subscribe();
    }

    private void Update()
    {
        // NPC의 카운트다운 타이밍에 맞춰서 표시하는 문구 변경
        switch (NpcAi.Count)
        {
            case 5:
                SetTimerText($"{NpcAi.Count}");
                RemindUser($"안 내면");
                break;
            case 4:
                SetTimerText($"{NpcAi.Count}");
                RemindUser($"진다");
                break;
            case 3:
                SetTimerText($"{NpcAi.Count}");
                RemindUser("가위");
                break;
            case 2:
                SetTimerText($"{NpcAi.Count}");
                RemindUser("바위");
                break;
            case 1:
                SetTimerText($"{NpcAi.Count}");
                RemindUser("보!");
                break;
        }
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void InitializeUI()
    {
        guideText.text = " ";
        countdownText.text = " ";
        resultText.text = " ";
    }

    private void SetTimerText(string text)
    {
        countdownText.text = text;
    }

    private void RemindUser(string text)
    {
        guideText.text = text;
    }
    
    private void ShowResult()
    {
        resultText.text = $"NPC의 선택: {NpcAi.NpcAction}\n유저의 선택: {UserInput.UserAction}\n결과: {GameManager.Instance.finalResultText}";
    }
    
    private void Subscribe()
    {
        NpcAi.DeclareAction += ShowResult;
    }

    private void Unsubscribe()
    {
        NpcAi.DeclareAction -= ShowResult;
    }
}