using UnityEngine;
using System.Collections;

public class OrcFollower : MonoBehaviour
{
    [SerializeField]
    private float _followSpeed = 2f; // �������� ���������
    [SerializeField]
    private Rigidbody2D _rigidbody; // Rigidbody ��� ������
    [SerializeField]
    private Animator animator; // ������� ��� ��������� ���������
    [SerializeField]
    private Transform player; // ��������� �� ������
    [SerializeField]
    private float _stopDistance = 1.5f; // ³������, �� ��� ��� �����������
    [SerializeField]
    private float _attackDistance = 0.5f; // ³������ ��� ����� ������
    [SerializeField]
    private int _damage; // ���� ����� ����
    [SerializeField]
    private float _deathDelay = 2f; // �������� ����� ������ ������ ���� �����
    [SerializeField]
    private float _attackDelay = 1f; // �������� �� �������

    private bool _isFollowing = false; // �� ���� ��� �� �������
    private Transform _targetEnemy; // ������� ���� ������
    private Vector2 _movement; // �������� �������� ����
    private bool _canAttack = true; // �� ����� ���������

    private void Update()
    {
        if (_targetEnemy != null)
        {
            // ������� ������
            float distanceToEnemy = Vector2.Distance(transform.position, _targetEnemy.position);

            if (distanceToEnemy > _attackDistance)
            {
                // ��� �� ������
                Vector2 direction = (_targetEnemy.position - transform.position).normalized;
                _movement = direction;
                UpdateAnimation(_movement);
            }
            else
            {
                // ������� � �����, ���� ����� ���������
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
            // ������ �� �����
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
            // ��� ����
            _rigidbody.MovePosition(_rigidbody.position + _movement * _followSpeed * Time.fixedDeltaTime);
        }
    }

    private void UpdateAnimation(Vector2 direction)
    {
        // ��������� ���������� ���������
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    private IEnumerator AttackEnemy()
    {
        // ��������� ������� �����
        animator.SetTrigger("Attack");

        // ��������� �������� �����, ���� ����� ��������
        _canAttack = false;

        // ������ �� ���������� ������� �����
        yield return new WaitForSeconds(1.5f);  // ��������, ��� �������� ������� ����� �����������

        // ������� ������
        if (_targetEnemy != null)
        {
            Enemy enemy = _targetEnemy.GetComponent<Enemy>(); // �������������, �� ����� �� ������ Enemy
            if (enemy != null)
            {
                // �������� ����������� ������
                enemy.TakeDamage(_damage);
                if (enemy.IsDead())
                {
                    StartCoroutine(WaitBeforeDeath(enemy));
                }
            }
        }

        // ������ ������ �������� ����� ��������� ������
        yield return new WaitForSeconds(_attackDelay);

        // ���������� ��������� �����
        _canAttack = true;
    }

    // �������� ��� �������� ����� ��������� ������ ���� �����
    private IEnumerator WaitBeforeDeath(Enemy enemy)
    {
        // ������ ������ �������� ����� �������� ������ �����
        yield return new WaitForSeconds(_deathDelay);

        // ����������, �� ���� ����� ��
        if (enemy != null)
        {
            _targetEnemy = null; // ������� ��������� �� ������
            enemy.Die(); // ��������� ����� �����, ���� ����� �� ����
        }
        else
        {
            Debug.Log("����� ��� �������� �� ���������� ��������.");
        }
    }

    public void StartFollowing()
    {
        if (player != null)
        {
            // ����������� �� X �� Y ����� ����������
            _rigidbody.constraints = RigidbodyConstraints2D.None; // ³�������� ��������� �� ���

            _isFollowing = true; // ������� ���������
        }
    }

    public void StopFollowing()
    {
        // ���������� ����, ��� �� �� �������
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll; // ���������� �� �� (X, Y, Z)

        _isFollowing = false; // ������� ���������
        _movement = Vector2.zero; // �������� ����
        UpdateAnimation(_movement); // ��������� ������� ��� �������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // ��������� ������ �� �����
        {
            _targetEnemy = collision.transform; // �������� ������ �� ����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && _targetEnemy == collision.transform)
        {
            _targetEnemy = null; // ����� ������ �� ����
        }
    }
}
