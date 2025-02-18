using UnityEngine;
using System.Collections;

public class OrcFollower : MonoBehaviour
{
    [SerializeField]
    private float _followSpeed = 2f; // Швидкість слідування
    [SerializeField]
    private Rigidbody2D _rigidbody; // Rigidbody для фізики
    [SerializeField]
    private Animator animator; // Аніматор для управління анімаціями
    [SerializeField]
    private Transform player; // Посилання на гравця
    [SerializeField]
    private float _stopDistance = 1.5f; // Відстань, на якій орк зупиняється
    [SerializeField]
    private float _attackDistance = 0.5f; // Відстань для атаки ворога
    [SerializeField]
    private int _damage; // Сила атаки орка
    [SerializeField]
    private float _deathDelay = 2f; // Затримка перед смертю ворога після атаки
    [SerializeField]
    private float _attackDelay = 1f; // Затримка між ударами

    private bool _isFollowing = false; // Чи слідує орк за гравцем
    private Transform _targetEnemy; // Поточна ціль ворога
    private Vector2 _movement; // Поточний напрямок руху
    private bool _canAttack = true; // Чи можна атакувати

    private void Update()
    {
        if (_targetEnemy != null)
        {
            // Атакуємо ворога
            float distanceToEnemy = Vector2.Distance(transform.position, _targetEnemy.position);

            if (distanceToEnemy > _attackDistance)
            {
                // Рух до ворога
                Vector2 direction = (_targetEnemy.position - transform.position).normalized;
                _movement = direction;
                UpdateAnimation(_movement);
            }
            else
            {
                // Зупинка і атака, якщо можна атакувати
                if (_canAttack)
                {
                    _movement = Vector2.zero;
                    UpdateAnimation(_movement);
                    StartCoroutine(AttackEnemy());
                }
            }
        }
        else if (_isFollowing && player != null)
        {
            // Слідуємо за героєм
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > _stopDistance)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                _movement = direction;
                UpdateAnimation(_movement);
            }
            else
            {
                _movement = Vector2.zero;
                UpdateAnimation(_movement);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_movement != Vector2.zero)
        {
            // Рух орка
            _rigidbody.MovePosition(_rigidbody.position + _movement * _followSpeed * Time.fixedDeltaTime);
        }
    }

    private void UpdateAnimation(Vector2 direction)
    {
        // Оновлення анімаційних параметрів
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    private IEnumerator AttackEnemy()
    {
        // Запускаємо анімацію атаки
        animator.SetTrigger("Attack");

        // Запобігаємо повторній атаці, поки триває затримка
        _canAttack = false;

        // Чекаємо на завершення анімації атаки
        yield return new WaitForSeconds(1.5f);  // Затримка, яка дозволяє анімації атаки завершитись

        // Атакуємо ворога
        if (_targetEnemy != null)
        {
            Enemy enemy = _targetEnemy.GetComponent<Enemy>(); // Передбачається, що ворог має скрипт Enemy
            if (enemy != null)
            {
                // Наносимо пошкодження ворогу
                enemy.TakeDamage(_damage);
                if (enemy.IsDead())
                {
                    StartCoroutine(WaitBeforeDeath(enemy));
                }
            }
        }

        // Чекаємо задану затримку перед наступною атакою
        yield return new WaitForSeconds(_attackDelay);

        // Дозволяємо атакувати знову
        _canAttack = true;
    }

    // Корутина для затримки перед знищенням ворога після смерті
    private IEnumerator WaitBeforeDeath(Enemy enemy)
    {
        // Чекаємо задану затримку перед викликом методу смерті
        yield return new WaitForSeconds(_deathDelay);

        // Перевіряємо, чи існує ворог ще
        if (enemy != null)
        {
            _targetEnemy = null; // Очищаємо посилання на ворога
            enemy.Die(); // Викликаємо метод смерті, якщо ворог ще існує
        }
        else
        {
            Debug.Log("Ворог був знищений до завершення затримки.");
        }
    }

    public void StartFollowing()
    {
        if (player != null)
        {
            // Розморожуємо осі X та Y перед слідуванням
            _rigidbody.constraints = RigidbodyConstraints2D.None; // Відключаємо обмеження на рух

            _isFollowing = true; // Початок слідування
        }
    }

    public void StopFollowing()
    {
        // Заморожуємо орка, щоб він не рухався
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll; // Заморожуємо всі осі (X, Y, Z)

        _isFollowing = false; // Зупинка слідування
        _movement = Vector2.zero; // Скидання руху
        UpdateAnimation(_movement); // Оновлення анімацій для зупинки
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Виявлення ворога за тегом
        {
            _targetEnemy = collision.transform; // Зберігаємо ворога як ціль
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && _targetEnemy == collision.transform)
        {
            _targetEnemy = null; // Ворог вийшов із зони
        }
    }
}
