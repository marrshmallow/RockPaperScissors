using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcAi : MonoBehaviour
{
    public static EAction NpcAction;
    
    private const float CountDownStartTime = 7f;
    private float _timer;
    public static int Count;
    
    private bool _isCountingDown;
    private bool _canDecide;
    private bool _hasDecided;
    
    // 테스트용
    [SerializeField] private TextMeshProUGUI timerDebug;
    [SerializeField] private TextMeshProUGUI npcAiDebug;
    
    public static event Action DeclareAction;

    private static void OnActionDeclared()
    {
        DeclareAction?.Invoke();
    }

    private IEnumerator Start()
    {
        // 플레이와 동시에 카운트다운 시작
        yield return CountDown();
    }

    private void Update()
    {
        // 테스트용
        timerDebug.text = _timer.ToString("0.0");
        npcAiDebug.text = $"NPC의 선택: {NpcAction}";

        // 결정할 수 있는 타이밍에 아직 결정을 내리지 않았다면 결정을 한다.
        if (!_hasDecided && _canDecide)
        {
            Decide();
        }
    }

    private IEnumerator CountDown()
    {
        // 타이머의 시간을 맞춘 후
        _timer = CountDownStartTime;
        // 카운트다운 상태임을 선언
        _isCountingDown = true;
        
        // 카운트다운 중일 때
        while (_isCountingDown)
        {
            // 0일 때 타이머를 정지하고 NPC가 결정을 번복할 수 없게 한다.
            if (_timer < 1f)
            {
                Count = 0;
                _timer = 0f;
                _isCountingDown = false;
                _canDecide = false;
            }
            // 1일 때 NPC가 결정할 수 있는 상태가 된다.
            else if (_timer < 2f)
            {
                Count = 1;
                _canDecide = true;
            }
            // 2
            else if (_timer < 3f)
            {
                Count = 2;
            }
            // 3
            else if (_timer < 4f)
            {
                Count = 3;
            }
            // 4
            else if (_timer < 5f)
            {
                Count = 4;
            }
            // 5
            else if (_timer < 6f)
            {
                Count = 5;
            }
            
            // 타이머는 계속 실행
            _timer -= Time.deltaTime;
            yield return null;
        }
        
        // 다 끝났으면 결정했다고 외침 (이벤트 매니저에게)
        OnActionDeclared();
    }

    private void Decide()
    {
        // 임시로 값을 담을 지역 변수 생성
        var decision = EAction.None;
        
        // 행동을 결정할 수 있는 상태일 때
        // 0: 아무것도 내지 않음 / 1~3: 가위, 바위, 보
        // int일 경우 최소값은 포함되나 최대값은 포함되지 않으므로 1, 4로 설정
        var randomNumber = Random.Range(1, 4);
        decision = (EAction)randomNumber;
        
        // 최종 확정
        NpcAction = decision;
        // _canDecide가 어떤 이유로 참이 되더라도 Decide가 다시 호출되지 않도록 설정
        _hasDecided = true;
    }
}