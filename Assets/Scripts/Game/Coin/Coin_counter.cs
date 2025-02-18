using UnityEngine;
using UnityEngine.UI; 

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private Text _coinText; // ��������� �� �����
    // ���� ������������� TextMeshPro: private TMP_Text _coinText;
    private int _coinCount = 0; // ˳������� �����

    // ����� ��� ��������� �����
    public void AddCoins(int amount)
    {
        _coinCount += amount; // �������� ������� �����
        UpdateCoinText(); // ��������� �����
    }

    // ����� ��� ��������� ������
    private void UpdateCoinText()
    {
        _coinText.text = "Coins: " + _coinCount.ToString();
    }
}
