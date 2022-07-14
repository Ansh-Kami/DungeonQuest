using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    public bool dead;
    private CapsuleCollider2D capsule;
    

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider2D>();
        
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if(!dead)
            {
                anim.SetTrigger("die");
                

                //player
                if(GetComponent<Movement2D>() != null)
                {
                    GetComponent<Movement2D>().enabled = false;

                }
                
                dead = true;

                //enemy
                if(GetComponentInParent<EnemyPatrol>() != null)
                {
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                }

                if(GetComponent<meleeEnemy>() != null)
                {
                    GetComponent<meleeEnemy>().enabled = false;
                }

                //boss
                

                if(GetComponent<BossAttack>() != null)
                {

                    GetComponent<BossAttack>().enabled = false;
                    
                }

                if(GetComponent<Boss>() != null)
                {
                    GetComponent<Boss>().enabled = false;
                }

                
                
            }
            
        }
    }

    private void Update()
    {

    }

    private void dropDead()
    {
        capsule.size = capsule.size * 0.3f;
    }


    
}
