using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAttack : MonoBehaviour
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
    private Boss boss;

    private Health playerHealth;

    private void Awake()
    {
        
        anim = GetComponent<Animator>();
        playerHealth = player.GetComponent<Health>();
        boss = GetComponentInParent<Boss>();
       
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

        if(boss != null)
        {
            boss.enabled = !PlayerInSight;
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

    void deadNext(){
        SceneManager.LoadScene(4);
    }
    

 


    

   

}
