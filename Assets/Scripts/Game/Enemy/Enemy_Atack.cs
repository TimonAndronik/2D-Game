using UnityEngine;
using System.Collections;

public class Enemy_Atack : MonoBehaviour
{
    public float attackCooldown = 2f; // Час між атаками
    private float lastAttackTime;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Перевіряємо, чи є зіткнення з об'єктом, що має тег "Player"
        if (collision.collider.CompareTag("Player"))
        {
            // Отримуємо компонент PlayerHealth із персонажа
            PlayerHealth pla = collision.collider.GetComponent<PlayerHealth>();
                // Викликаємо атаку з затримкою
                StartCoroutine(AttackWithDelay(pla));
           
        }
    }


    // Корутина для атаки з затримкою
    private IEnumerator AttackWithDelay(PlayerHealth pla)
    {
        // Перевіряємо, чи пройшов час між атаками
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time; // Оновлюємо час останньої атаки


            // Викликаємо анімацію атаки
            animator.SetTrigger("Attack");

            // Затримка перед нанесенням шкоди
            yield return new WaitForSeconds(1f);  // Час для затримки (можна налаштувати)

            // Наносимо шкоду
            pla.TakeDamage();
        }
    }
}
