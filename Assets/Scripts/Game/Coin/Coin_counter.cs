using UnityEngine;
using UnityEngine.UI; 

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private Text _coinText; // Посилання на текст
    // Якщо використовуєте TextMeshPro: private TMP_Text _coinText;
    private int _coinCount = 0; // Лічильник монет

    // Метод для додавання монет
    public void AddCoins(int amount)
    {
        _coinCount += amount; // Збільшуємо кількість монет
        UpdateCoinText(); // Оновлюємо текст
    }

    // Метод для оновлення тексту
    private void UpdateCoinText()
    {
        _coinText.text = "Coins: " + _coinCount.ToString();
    }
}
