using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimationController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Animator buttonAnimator; // ������� ��� ������

    private void Awake()
    {
        if (buttonAnimator == null)
            buttonAnimator = GetComponent<Animator>();
    }

    // ��������� ���� �� ������
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonAnimator.SetTrigger("Highlighted");
    }

    // ³�������� ���� � ������
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonAnimator.SetTrigger("Normal");
    }

    // ��� �� ������
    public void OnPointerClick(PointerEventData eventData)
    {
        buttonAnimator.SetTrigger("Pressed");
    }

    // ������ ������ (���������, ����� ��������� ��� ���������)
    public void OnSelect(BaseEventData eventData)
    {
        buttonAnimator.SetTrigger("Selected");
    }

    // ������ �������� ����
    public void OnDeselect(BaseEventData eventData)
    {
        buttonAnimator.SetTrigger("Normal");
    }

    // ����������� ������ (�����������)
    public void DisableButton()
    {
        buttonAnimator.SetTrigger("Disable");
    }

    // ��������� ������ (�����������)
    public void EnableButton()
    {
        buttonAnimator.SetTrigger("Normal");
    }
}
