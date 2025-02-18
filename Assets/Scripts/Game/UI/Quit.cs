using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    // ћетод, €кий викликаЇтьс€ при натисканн≥ кнопки
    public void ExitGame()
    {
        Debug.Log("Game is exiting..."); // Ћог пов≥домленн€ в консол≥

        // якщо гра запущена в редактор≥, ми використовуЇмо EditorApplication дл€ зупинки гри
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else       
        Application.Quit(); // ƒл€ зб≥рок гри вих≥д з гри
#endif

    }
}
