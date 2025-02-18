using UnityEngine;
using UnityEngine.UI;

public class OrcInteraction : MonoBehaviour
{
    public GameObject dialogUI; // ³��� ������
    public Text dialogText; // �������� ���� ��� �����
    public string[] dialogLines; // ������ ������
    public int costToHire = 5; // ������� ����� ����

    private PlayerMovement player; // ��������� �� PlayerMovement
    private int currentLine = 0; // ����� ������� ������
    private bool isFollowing = false; // �� ��� ��� ���� �� �����

    [Header("Choice Buttons")]
    public GameObject hireButton; // ������ ��� ����� ����
    public GameObject dismissButton; // ������ ��� ��������� ����

    [Header("Interaction Settings")]
    public float interactionDistance = 5f; // ³������, �� ��� ���������� ������� � �����
    public Transform playerTransform; // ��������� �� ��'��� ������

    private bool isPlayerInRange = false; // �� ����������� ����� � ��� �����䳿

    private void Start()
    {
        hireButton.SetActive(false); // �������� ������ ��������
        dismissButton.SetActive(false);

        // ��������� PlayerMovement
        player = playerTransform.GetComponent<PlayerMovement>();

        // ������ ������� ��� ������ �����
        Button hireBtn = hireButton.GetComponent<Button>();
        hireBtn.onClick.AddListener(HireOrc); // ������� ����� HireOrc ��� ���������
    }

    private void Update()
    {
        // �������� �� ������� �� �����
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (distanceToPlayer <= interactionDistance && !isFollowing && !isPlayerInRange)
        {
            isPlayerInRange = true; // ����� ����������� � ��� �����䳿
            StartDialog();
        }
        else if (distanceToPlayer > interactionDistance && isPlayerInRange)
        {
            isPlayerInRange = false; // ����� ������� ���� �����䳿
            EndDialog();
        }

        // �������� ���������� ��� ��� ����������� �����
        if (isPlayerInRange && Input.GetMouseButtonDown(0)) // ��� ��� ����������� ������
        {
            ShowNextDialogLine(); // ������ �������� ������
        }
    }

    private void StartDialog()
    {
        currentLine = 0; // �������� ����� � ����� ������
        ShowNextDialogLine(); // ³��������� ����� ������
        dialogUI.SetActive(true); // ³��������� �������� ����
    }

    private void ShowNextDialogLine()
    {
        if (currentLine < dialogLines.Length)
        {
            // ³��������� ������� ������
            dialogText.text = dialogLines[currentLine];
            currentLine++;
        }
        else
        {
            // ���� �� ������ ��������, �������� ������ ��� ������
            ShowChoiceButtons();
        }
    }

    private void ShowChoiceButtons()
    {
        hireButton.SetActive(true); // �������� ������
        dismissButton.SetActive(true);
    }

    private void EndDialog()
    {
        dialogUI.SetActive(false); // ������ ��������� ������
        currentLine = 0; // ������� �����
    }

    // �����������, ���� ���������� ������ "�������"
    public void HireOrc()
    {
        int playerCoins = player.GetCoins(); // �������� ������� ����� � ������

        if (playerCoins >= costToHire)
        {
            player.AddCoins(-costToHire); // ������ ������ � ������
            GetComponent<OrcFollower>().StartFollowing(); // ������ �������� �� �����
            isFollowing = true; // ������������ ������ ���������
            EndDialog(); // ��������� �����
        }
        else
        {
            Debug.Log("����������� �����!");
        }
    }

    // �����������, ���� ���������� ������ "³�������"
    public void DismissOrc()
    {
        EndDialog(); // ��������� �����
    }
}
