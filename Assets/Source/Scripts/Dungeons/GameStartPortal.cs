using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trigger for game start in lobby
public class GameStartPortal : MonoBehaviour
{
    private FadeUI _fade;
    private void Start()
    {
        _fade = FindAnyObjectByType<FadeUI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerEntity>())
        {
            GameStarter.StartGame();
            _fade.Fade();
        }
    }
}
