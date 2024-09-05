using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // 가위 바위 보 게임의 결과: 무승부, 승리, 패배 (유저 기준)
    private EResult _result;
    // 결과란에 출력할 내용
    private readonly string[] _resultTexts = new string [3];
    public string finalResultText;
    
    // 습관적 매니저 생성 (...)
    private void Awake()
    {
        Initialize();
    }

    // 습관적 이벤트 구독 (...)
    private void Start()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
   
    // 본격적인 판정에 앞서서 경우의 수를 줄여준다(?)
    private void Judge()
    {
        // 유저가 아무 선택을 하지 않았을 경우 유저 패배 (안 내면 진다고 했으므로)
        if (UserInput.UserAction == EAction.None)
        {
            _result = EResult.Lose;
            finalResultText = _resultTexts[(int)_result];
            return;
        }

        // NPC와 유저가 같은 걸 냈을 때는
        if (NpcAi.NpcAction == UserInput.UserAction)
        {
            // 무승부,
            _result = EResult.Draw;
        }
        // 아니라면
        else
        {
            // 결판을 낸다.
            CompareActions();
        }

        finalResultText = _resultTexts[(int)_result];
    }

    private void CompareActions()
    {
        // 유저가 어떤 걸 냈느냐를 우선적인 기준으로 삼은 뒤
        // 결과값(무승부, 승리, 패배 중 1)을 정한다.
        switch (UserInput.UserAction)
        {
            // 유저가 보자기를 낸 경우
            case EAction.Paper:
                // 컴퓨터가 바위를 내면 승리
                if (NpcAi.NpcAction == EAction.Rock)
                {
                    _result = EResult.Win;
                }
                // 컴퓨터가 가위를 내면 패배
                else if (NpcAi.NpcAction == EAction.Scissors)
                {
                    _result = EResult.Lose;                    
                }
                break;
            // 이하 생략
            case EAction.Rock:
                if (NpcAi.NpcAction == EAction.Scissors)
                {
                    _result = EResult.Win;
                }
                else if (NpcAi.NpcAction == EAction.Paper)
                {
                    _result = EResult.Lose;
                }
                break;
            case EAction.Scissors:
                if (NpcAi.NpcAction == EAction.Paper)
                {
                    _result = EResult.Win;
                }
                else if (NpcAi.NpcAction == EAction.Rock)
                {
                    _result = EResult.Lose;
                }
                break;
        }
    }

    private void Initialize()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
        _result = EResult.Draw;
        
        // 표시할 문자열을 enum에 맞춰서 미리 저장
        _resultTexts[(int)EResult.Draw] = "무승부";
        _resultTexts[(int)EResult.Win] = "유저의 승리!";
        _resultTexts[(int)EResult.Lose] = "컴퓨터의 승리!";
    }
    
    private void Subscribe()
    {
        NpcAi.DeclareAction += Judge;
    }

    private void Unsubscribe()
    {
        NpcAi.DeclareAction -= Judge;
    }
}