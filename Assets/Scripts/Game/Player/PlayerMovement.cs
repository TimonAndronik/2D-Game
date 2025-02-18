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

    private int _coinCount = 0; // ˳������� �����
    private Vector2 movement;

    public int GetCoins()
    {
        return _coinCount; // ������� ������� �����
    }

    public void AddCoins(int amount)
    {
        _coinCount += amount; // ������ ������
        UpdateCoinText(); // ��������� �����
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

        // ��������� �������
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        // ��� ���������
        _rigidbody.MovePosition(_rigidbody.position + movement * _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin")) // ���� ������� ����� ������
        {
            AddCoins(1); // ������ 1 ������
            Destroy(collision.gameObject); // ������� ������
        }
    }
}
