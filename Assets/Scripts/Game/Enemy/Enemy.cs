using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator animator; // ������� ������
    [SerializeField]
    private int health = 50; // ������'� ������

    public void TakeDamage(int damage)
    {
        health -= damage;

        // ��������� ������� ��������� �����
        animator.SetTrigger("Hit");

        if (IsDead())
        {
            Die(); // ���� ����� �������, ��������� ����� �����
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void Die()
    {
        // ��������� ������� �����
        animator.SetTrigger("Death");

        // ����� ������ ��������� ����� ��� �������� ��'���� ���� �������
        Destroy(gameObject, 1f); // ������� ������ ����� 1 ������� ���� �����
    }
}
