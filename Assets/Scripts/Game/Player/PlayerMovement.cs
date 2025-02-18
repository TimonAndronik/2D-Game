using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Text _coinText;

    private int _coinCount = 0; // Лічильник монет
    private Vector2 movement;

    public int GetCoins()
    {
        return _coinCount; // Повертає кількість монет
    }

    public void AddCoins(int amount)
    {
        _coinCount += amount; // Додаємо монети
        UpdateCoinText(); // Оновлюємо текст
    }

    private void UpdateCoinText()
    {
        if (_coinText != null)
        {
            _coinText.text = "Coins: " + _coinCount.ToString();
        }
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        // Оновлення анімацій
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        // Рух персонажа
        _rigidbody.MovePosition(_rigidbody.position + movement * _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin")) // Якщо гравець зібрав монету
        {
            AddCoins(1); // Додаємо 1 монету
            Destroy(collision.gameObject); // Знищуємо монету
        }
    }
}
