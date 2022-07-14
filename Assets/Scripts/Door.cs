using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void OpenDoor()
    {
        gameObject.SetActive(false);
        Debug.Log("Dooropened");
    }

    public void CloseDoor()
    {
        Debug.Log("Doorclosed");
        gameObject.SetActive(true);
    }
}
