using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;

    public BulletManager bulletManager;

    [Header("Movement")]
    public float speed;
    public bool isGrounded;
    public Vector3 direction;

    public RigidBody3D body;
    public CubeBehaviour cube;
    public Camera playerCam;

    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _Fire();
        _Move();
    }

    private void _Move()
    {
        if (isGrounded)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                // move right
                body.velocity = playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                // move left
                body.velocity = -playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Vertical") > 0.0f)
            {
                // move forward
                body.velocity = playerCam.transform.forward * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Vertical") < 0.0f)
            {
                // move Back
                body.velocity = -playerCam.transform.forward * speed * Time.deltaTime;
            }

            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, 0.9f);
            body.velocity = new Vector3(body.velocity.x, 0.0f, body.velocity.z); // remove y


            if (Input.GetAxisRaw("Jump") > 0.0f)
            {
                body.velocity += transform.up * speed * 0.05f * Time.deltaTime;
            }
        }
        if (!isGrounded)  //allow direction changing in air
        {
            direction = body.velocity;
            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                // move right
                direction += playerCam.transform.right * speed *0.05f * Time.deltaTime;
            }
            if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                // move left
                direction += -playerCam.transform.right * speed * 0.05f * Time.deltaTime;
            }
            if (Input.GetAxisRaw("Vertical") > 0.0f)
            {
                // move forward
                direction += playerCam.transform.forward * speed * 0.05f * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Vertical") < 0.0f)
            {
                // move Back
                direction += -playerCam.transform.forward * speed * 0.05f * Time.deltaTime;
            }
            direction = Vector3.Lerp(direction, body.velocity, 0.99f);
            body.velocity = direction;
        }


        transform.position += body.velocity;
        

        if (Input.GetAxisRaw("Quit") > 0.0f)
            {
                SceneManager.LoadScene(0);
            }

        if (Input.GetAxisRaw("Cancel") > 0.0f)
        {
            Debug.Break();
        }
    }


    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {

                var tempBullet = bulletManager.GetBullet(bulletSpawn.position, bulletSpawn.forward);
                tempBullet.transform.SetParent(bulletManager.gameObject.transform);
            }
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        isGrounded = cube.isGrounded;
    }

}
