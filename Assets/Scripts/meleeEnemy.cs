using UnityEngine;

public class meleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject player;
    private bool PlayerInSight;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private EnemyPatrol enemyPatrol;

    private Health playerHealth;

    private void Awake()
    {
        
        anim = GetComponent<Animator>();
        playerHealth = player.GetComponent<Health>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
       
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight == true)
        {
            
            if (cooldownTimer >= attackCooldown)
            {

                cooldownTimer = 0;
                anim.SetTrigger("MeleeAttack");

            }
        
        }

        if (playerHealth.currentHealth <= 0)
        {
            PlayerInSight = false;
        }

        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight;
        }

        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerInSight = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerInSight = false;
        }
    }


    private void DamagePlayer()
    {
        if(PlayerInSight == true)
        {
            playerHealth.TakeDamage(damage);

        }
    }

 


    private void DisablEnemy()
    {
        gameObject.SetActive(false);
    }

   

}
