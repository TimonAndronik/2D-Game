using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator animator; // Аніматор ворога
    [SerializeField]
    private int health = 50; // Здоров'я ворога

    public void TakeDamage(int damage)
    {
        health -= damage;

        // Викликаємо анімацію отримання шкоди
        animator.SetTrigger("Hit");

        if (IsDead())
        {
            Die(); // Якщо ворог мертвий, викликаємо метод смерті
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void Die()
    {
        // Викликаємо анімацію смерті
        animator.SetTrigger("Death");

        // Можна додати додаткову логіку для знищення об'єкта після анімації
        Destroy(gameObject, 1f); // Знищуємо ворога через 1 секунду після смерті
    }
}
