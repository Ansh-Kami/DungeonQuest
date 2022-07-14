using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackCooldown2;
    public Transform firepoint;
    public GameObject bulletPrefab;
    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;
    private float cooldownTimer2 = Mathf.Infinity;
    private Movement2D playerMovement;
    public bool stopAttack;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<Movement2D>();

    }

    void Update()
    {
        anim.SetBool("StopAttack", false);
        if(Input.GetButtonDown("Fire1") && playerMovement.canAttack() && cooldownTimer2 > attackCooldown2)
            PlayShoot();
            
        
        if(Input.GetKeyUp(KeyCode.K) && cooldownTimer > attackCooldown && playerMovement.canAttack() && anim.GetCurrentAnimatorStateInfo(0).IsName("BowUp"))
            Shoot();

        if(Input.GetKeyUp(KeyCode.K) && cooldownTimer < attackCooldown && playerMovement.canAttack())
        {
            anim.SetBool("StopAttack", true);
        }

        

    
            
        
        cooldownTimer += Time.deltaTime;
        cooldownTimer2 += Time.deltaTime;
        
        
    }

    void PlayShoot()
    {
        anim.SetTrigger("Attack");
        cooldownTimer2 = 0;
    }



    void Shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        cooldownTimer = 0;
        anim.SetBool("StopAttack", true);
    }


}
