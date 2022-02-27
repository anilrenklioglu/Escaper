using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
   [SerializeField] private float moveSpeed;
   [SerializeField] private float rotationSpeed;
   [SerializeField] private Text score;

   private bool _gameOver = false;
   private Rigidbody2D rb;
   private Camera cam;
   public AudioClip AudioClip;
   public AudioSource AudioSource;
   private float cpt = 0;
   private int scr = 0;

   private void Start()
   {
      rb = GetComponent<Rigidbody2D>();
      cam = Camera.main;
   }

   private void Update()
   {
      //getting input
      
      if (!_gameOver)
      {
         if (Input.GetKey(KeyCode.RightArrow))
         {
            transform.Rotate(Vector3.forward * (-rotationSpeed) * Time.deltaTime);
         }
         
         if (Input.GetKey(KeyCode.LeftArrow))
         {
            transform.Rotate(Vector3.forward * (rotationSpeed) * Time.deltaTime);
         }
      }
      
      Score();
   }

   private void FixedUpdate()
   {
      //moving player

      if (!_gameOver)
      {
         rb.AddRelativeForce(new Vector3(moveSpeed * Time.fixedDeltaTime, 0f, 0f));
      }
   }

   private void LateUpdate()
   {
      if (!_gameOver)
      {
         cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
      }
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
      if (!_gameOver)
      {
         _gameOver = true;

         GetComponent<SpriteRenderer>().enabled = false;
         GetComponent<PolygonCollider2D>().enabled = false;
         GetComponentInChildren<ParticleSystem>().Play();
         AudioSource.PlayOneShot(AudioClip);
         
         Invoke("Restart", 2f);
      }
   }

   void Restart()
   {
      SceneManager.LoadScene(0);
   }

   void Score()
   {
      cpt += Time.deltaTime;
      if (cpt >= .5f)
      {
         cpt = 0f;
         scr++;
         score.text = scr.ToString("000");
      }
   }
}
