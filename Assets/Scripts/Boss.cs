using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] public GameObject player;
    private Health playerHealth;

    public bool flip;
    [SerializeField] public float speed;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerHealth = player.GetComponent<Health>();

    }

    private void Update()
    {
        playerfollow();

        
    }

    private void playerfollow()
    {
        //anim
        anim.SetBool("moving", true);

        Vector3 scale = transform.localScale;

        if(player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1:1);
            transform.Translate(speed*Time.deltaTime,0,0);

        }else{
            scale.x = Mathf.Abs(scale.x) * (flip ? -1:1);
            transform.Translate(speed*Time.deltaTime*-1,0,0);
        }

        transform.localScale = scale;

    


    
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }
}
