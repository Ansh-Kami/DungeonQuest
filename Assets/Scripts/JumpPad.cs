using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float bounce = 42f;
    private Animator _anim;
    private bool JPD = false;
    

    void Start()
    {
        _anim = GetComponent<Animator>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            JPD = true;
            _anim.SetBool("JPD", JPD);
            

        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        JPD = false;
        _anim.SetBool("JPD", JPD);
    }

    
}
