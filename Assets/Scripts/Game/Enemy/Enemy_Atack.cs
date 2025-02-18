using UnityEngine;
using System.Collections;

public class Enemy_Atack : MonoBehaviour
{
    public float attackCooldown = 2f; // ��� �� �������
    private float lastAttackTime;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ����������, �� � �������� � ��'�����, �� �� ��� "Player"
        if (collision.collider.CompareTag("Player"))
        {
            // �������� ��������� PlayerHealth �� ���������
            PlayerHealth pla = collision.collider.GetComponent<PlayerHealth>();
                // ��������� ����� � ���������
                StartCoroutine(AttackWithDelay(pla));
           
        }
    }


    // �������� ��� ����� � ���������
    private IEnumerator AttackWithDelay(PlayerHealth pla)
    {
        // ����������, �� ������� ��� �� �������
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time; // ��������� ��� �������� �����


            // ��������� ������� �����
            animator.SetTrigger("Attack");

            // �������� ����� ���������� �����
            yield return new WaitForSeconds(1f);  // ��� ��� �������� (����� �����������)

            // �������� �����
            pla.TakeDamage();
        }
    }
}
