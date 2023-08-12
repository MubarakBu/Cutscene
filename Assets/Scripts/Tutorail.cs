using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Tutorail : MonoBehaviour
{
    public GameObject _robber;
    public Animator animator;
    public PlayableDirector playableDirector;
    public GameObject gStart;
    public GameObject tutorialComplete;
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float rn_speed, ro_speed;
    private float mouseX, mouseY;
    public bool walking;
    public Transform playerTrans;
    private bool isInsideTrigger = false;
    public Camera fbsCam;
    public float damage = 10f;
    public float shotRange = 100f;
    private bool isDestroy = false;
    public GameObject tot1D;
    public GameObject _playerHB;
    public GameObject _enemyHB;
    public GameObject _playerHT;
    public GameObject _enemyHT;

    public GameObject[] popUps;
    private int popUpIndex;
    private MeshRenderer meshRenderer;


    

    private void OnTriggerEnter(Collider other) 
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        isInsideTrigger = true;
    }
    private void OnTriggerExit(Collider other) 
    {
        isInsideTrigger = false;
    }

    void FixedUpdate()
    {
        if (playableDirector.state == PlayState.Paused)
        {
            if(Input.GetKey(KeyCode.W) && walking == true)
            {
                playerRigid.velocity = transform.forward * rn_speed * Time.deltaTime;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(playableDirector.state == PlayState.Paused)
        {
            if(gameObject.activeSelf)
            {
                _robber.SetActive(false);
                if(tot1D != null)
                {
                    tot1D.SetActive(true);
                }
            }
            
            mouseX += Input.GetAxis("Mouse X") * ro_speed * Time.deltaTime;
            mouseY -= Input.GetAxis("Mouse Y") * ro_speed * Time.deltaTime;

            mouseY = Mathf.Clamp(mouseY, -8f, 2f);

            playerTrans.localRotation = Quaternion.Euler(mouseY, mouseX, 0f);

            
            if(Input.GetKeyDown(KeyCode.W))
            {
                playerAnim.SetTrigger("walk");
                playerAnim.ResetTrigger("idle");
            }
            if(Input.GetKeyUp(KeyCode.W))
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.SetTrigger("idle");
            }
            

            for(int i = 0; i < popUps.Length; i++)
            {
                if(i == popUpIndex)
                {
                    popUps[popUpIndex].SetActive(true);
                }
            }

            if(popUpIndex == 0)
            {
                if(Input.GetKeyUp(KeyCode.W))
                {
                    popUps[popUpIndex].SetActive(false);
                    popUpIndex++;
                }
            }
            else if(popUpIndex == 1)
            {
                if(isInsideTrigger == true)
                {
                    if(Input.GetKeyDown(KeyCode.S))
                    {
                        playerAnim.SetTrigger("down");
                        playerAnim.ResetTrigger("idle");
                        popUps[popUpIndex].SetActive(false);
                        popUpIndex++;
                    }
                }
            }
            else if(popUpIndex == 2)
            {
                if(Input.GetKeyDown(KeyCode.D))
                {
                    playerAnim.SetTrigger("up");
                    playerAnim.ResetTrigger("down"); 
                }
                if(Input.GetKeyUp(KeyCode.D))
                {
                    playerAnim.ResetTrigger("up");
                    playerAnim.SetTrigger("idle");
                    popUps[popUpIndex].SetActive(false);
                    popUpIndex++;
                }
            }
            else if(popUpIndex == 3)
            {
                if(Input.GetKey(KeyCode.Space))
                {
                    walking = false;
                    playerAnim.SetTrigger("shoot");
                    playerAnim.ResetTrigger("idle");
                    //Fire();
                }
                if(Input.GetKeyUp(KeyCode.Space))
                {
                    playerAnim.ResetTrigger("shoot");
                    playerAnim.SetTrigger("idle");
                    StartCoroutine(ShootGun());
                }
            }
            else if(popUpIndex == 4)
            {
                gStart.SetActive(true);
                _robber.SetActive(true);
                _playerHB.SetActive(true);
                _enemyHB.SetActive(true);
                _playerHT.SetActive(true);
                _enemyHT.SetActive(true);
                tutorialComplete.SetActive(false);
            }
        }
    }

    IEnumerator ShootGun()
    {
        RayCastForEnemy();
        if(isDestroy)
        {
            yield return new WaitForSeconds(1f);
            popUps[popUpIndex].SetActive(false);
            popUpIndex++;
        }
    }


    void RayCastForEnemy()
    {
        RaycastHit hit;
        if(Physics.Raycast(fbsCam.transform.position, fbsCam.transform.forward, out hit, shotRange))
        {
            Debug.Log(hit.transform.name);

            Tot1D target = hit.transform.GetComponent<Tot1D>();
            if(target != null)
            {
                target.TakeDamage(damage);
                isDestroy = true;
            }
        }
    }
}
