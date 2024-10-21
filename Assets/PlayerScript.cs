using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject bombParticle;
    public Vector2 moveSpeed;
    public Animator animator;
    private bool isBlock = true;

    // 音楽
    private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        //爆発パーティクル発生
        Instantiate(bombParticle, transform.position, Quaternion.identity);

        if (other.CompareTag("COIN"))
        {
            other.gameObject.SetActive(false);
            audioSource.Play();
            GameManagerScript.score += 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Jump", false);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GoalScript.isGameClear)
        {
            // ジャンプアニメーション切り替え
            if (isBlock)
            {
                animator.SetBool("Jump", false);
            }

            // コントローラー操作
            Vector3 v = rb.velocity;

            if (isBlock && Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * moveSpeed.y, ForceMode.VelocityChange);
                animator.SetBool("Jump", true);
            }

            // コントローラー左右移動
            float stick = Input.GetAxis("Horizontal");
            if (stick > 0 || Input.GetKey(KeyCode.RightArrow))
            {
                v.x = moveSpeed.x;
                // 右向き
                if (!animator.GetBool("Jump"))
                {
                    Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500 * Time.deltaTime);        
                }
            }
            else if (stick < 0 || Input.GetKey(KeyCode.LeftArrow))
            {
                v.x = -moveSpeed.x;
                // 左向き
                if (!animator.GetBool("Jump"))
                {
                    Quaternion targetRotation = Quaternion.Euler(0, -90, 0);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500 * Time.deltaTime);
                }
            }
            else
            {
                v.x = 0;
            }

            rb.velocity = v;
        }
        else
        {
            Vector3 v = rb.velocity;
            v.x = 0;
            rb.velocity = v;
        }
    }
}
