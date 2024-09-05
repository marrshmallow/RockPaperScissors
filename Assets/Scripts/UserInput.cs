using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// "가위, 바위, 보" 버튼의 OnClick 이벤트에 연결
/// </summary>
public class UserInput : MonoBehaviour
{
    public static EAction UserAction = EAction.None;
    [SerializeField] private Button[] actions;

    public void ConfirmAction(string action)
    {
        UserAction = (EAction)System.Enum.Parse(typeof(EAction), action);
    }
}
