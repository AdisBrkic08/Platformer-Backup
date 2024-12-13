using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{

    Collider2D body;
    Collider2D feet;
    private void Start()
    {
        body = GetComponent<CapsuleCollider2D>();
        feet = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (feet.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            Destroy(other.gameObject);

        }

        else if (body.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            FindFirstObjectByType<GameSession>().ProcessPlayerDamage();
         
        }
       
    }

}
