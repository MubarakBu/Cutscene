using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Police : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float rn_speed, ro_speed;
    private float mouseX, mouseY;
    public bool walking;
    public Transform playerTrans;
    public PlayableDirector playableDirector;
    public Transform cameraTransform;
    public Hide hide;
    

    // gun var
    public Camera fbsCam;
    public float damage = 10;
    public float shotRange = 100f;
    public AudioSource gunshot;
    public float fireRate = 0.5f;
    public int clipSize = 30;
    public int reservedAmmoCapacity = 270;
    bool _canShoot;
    int _currentAmmoInClip;
    int _ammoInReserve;
    


    void Start()
    {
        _currentAmmoInClip = clipSize;
        _ammoInReserve = reservedAmmoCapacity;
        _canShoot = true;
    }

    void Fire()
    {
        gunshot.Play();
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

    void Update()
    {


        if(playableDirector.state == PlayState.Paused)
        {
            

            mouseX += Input.GetAxis("Mouse X") * ro_speed * Time.deltaTime;
            mouseY -= Input.GetAxis("Mouse Y") * ro_speed * Time.deltaTime;

            mouseY = Mathf.Clamp(mouseY, -10f, 2f);

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
            
            
            if(Input.GetKey(KeyCode.Space) && _canShoot && _currentAmmoInClip > 0 && hide.isDown == false)
            {
                walking = false;
                playerAnim.SetTrigger("shoot");
                playerAnim.ResetTrigger("idle");
                _canShoot = false;
                _currentAmmoInClip--;
                Fire();
                StartCoroutine(ShootGun());
            }
            if(Input.GetKeyUp(KeyCode.Space))
            {
                playerAnim.ResetTrigger("shoot");
                playerAnim.SetTrigger("idle");
            }
        }
        
    }


    IEnumerator ShootGun()
    {
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
        RayCastForEnemy();
    }

    void RayCastForEnemy()
    {
        RaycastHit hit;
        if(Physics.Raycast(fbsCam.transform.position, fbsCam.transform.forward, out hit, shotRange))
        {
            Debug.Log(hit.transform.name);

            Rhit target = hit.transform.GetComponent<Rhit>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

}
