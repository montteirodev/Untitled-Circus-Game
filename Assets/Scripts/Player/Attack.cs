using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;
    [Header("Attack Settings")]
    private bool isAttacking;
    private float nextAttackTime;
    public float attackDelay = 0.5f;
    [Header("Attack Area")]
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public int damage = 1;

    public GameObject Spatula;

    private void Start()
    {
        Spatula.SetActive(false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking && Time.time >= nextAttackTime)
        {
            AttackInput();
        }
    }
    void AttackInput()
    {
        isAttacking = true;
        nextAttackTime = Time.time + attackDelay;
        Spatula.SetActive(true);
        anim.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
        }
        Invoke("HideSpatula", 1f);
    }
    public void HideSpatula()
    {
        Spatula.SetActive(false);
        isAttacking = false;
    }
}
