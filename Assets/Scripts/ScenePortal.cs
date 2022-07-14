using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    [SerializeField] int SceneNumber;
    private bool can_tp;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && can_tp == true)
        {
            SceneManager.LoadScene(SceneNumber);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        can_tp = true;

    }
}
