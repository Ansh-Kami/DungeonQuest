using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Door door;
    private bool doorclosed;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        doorclosed = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(doorclosed)
        {
            door.OpenDoor();
            anim.SetBool("On", doorclosed);
            
        }
        else
        {
            door.CloseDoor();
            anim.SetBool("On", doorclosed);

            
        }
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(doorclosed)
        {
            doorclosed = false;
            Debug.Log("open");
            
        }
        else
        {
            doorclosed = true;
            Debug.Log("closed");
            
        }
        

    }
}
