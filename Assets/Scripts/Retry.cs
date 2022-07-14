using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    private Health health;
    [SerializeField] int scene;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if (health.dead == true){
            SceneManager.LoadScene(scene);
        }
    }

}
