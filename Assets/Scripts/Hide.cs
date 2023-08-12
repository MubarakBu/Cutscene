using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class Hide : MonoBehaviour
{
    public GameObject player;
    public Animator playerAnim;
    public PlayableDirector playableDirector;
    public bool isDown = false;

    private void OnTriggerStay(Collider other) 
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("down");
            playerAnim.ResetTrigger("idle");
            isDown = true;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            playerAnim.SetTrigger("up");
            playerAnim.ResetTrigger("down");
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.ResetTrigger("up");
            playerAnim.SetTrigger("idle");
            isDown = false;

        }
    }
}
