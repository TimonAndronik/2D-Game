using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimationController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Animator buttonAnimator; // Аніматор для кнопки

    private void Awake()
    {
        if (buttonAnimator == null)
            buttonAnimator = GetComponent<Animator>();
    }

    // Наведення миші на кнопку
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonAnimator.SetTrigger("Highlighted");
    }

    // Відведення миші з кнопки
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonAnimator.SetTrigger("Normal");
    }

    // Клік по кнопці
    public void OnPointerClick(PointerEventData eventData)
    {
        buttonAnimator.SetTrigger("Pressed");
    }

    // Кнопка обрана (наприклад, через клавіатуру або контролер)
    public void OnSelect(BaseEventData eventData)
    {
        buttonAnimator.SetTrigger("Selected");
    }

    // Кнопка втратила вибір
    public void OnDeselect(BaseEventData eventData)
    {
        buttonAnimator.SetTrigger("Normal");
    }

    // Деактивація кнопки (опціонально)
    public void DisableButton()
    {
        buttonAnimator.SetTrigger("Disable");
    }

    // Активація кнопки (опціонально)
    public void EnableButton()
    {
        buttonAnimator.SetTrigger("Normal");
    }
}
