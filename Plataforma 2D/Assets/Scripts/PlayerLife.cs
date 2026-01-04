using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public bool alive = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameController.gc.RefreshScreen();
    }
    public void LoseLife()
    {
        if (alive)
        {
            alive = false;
            gameObject.GetComponent<Animator>().SetTrigger("Dead");
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            gameObject.GetComponent<Player>().enabled = false;
            gameObject.GetComponent<Animator>().SetBool("Jump", false);
            GameController.gc.SetLives(-1);
            Invoke("LoadScene", 1f);
        }
    }
    void LoadScene()
    {
        SceneManager.LoadScene("Fase 1 1");
    }
}
