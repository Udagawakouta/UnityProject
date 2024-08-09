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

        if (other.gameObject.tag == "COIN")
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
        transform.rotation = Quaternion.Euler(0, 90, 0);
        //moveSpeed = 10;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの下方向へレイを出す
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.8f, 0.0f);
        Ray ray = new Ray(rayPosition, Vector3.down);

        float distance = 0.9f;
        Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);

        isBlock = Physics.Raycast(ray, distance);

        if (GoalScript.isGameClear == false)
        {
            // ジャンプアニメーション切り替え
            if (isBlock == true)
            {
                animator.SetBool("Jump", false);
                Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
            }
            //else
            //{
            //    animator.SetBool("Jump", true);
            //    Debug.DrawRay(rayPosition, Vector3.down * distance, Color.yellow);
            //}


            // コントローラー操作
            Vector3 v = rb.velocity;
            if (Input.GetButton("Jump") || Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("Jump", true);
                v.y = moveSpeed.y;
            }

            // コントローラー右移動
            float stick = Input.GetAxis("Horizontal");
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow) || stick > 0)
            {
                v.x = moveSpeed.x;
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                v.x = 0;
            }

            // コントローラー左移動
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow) || stick < 0)
            {
                v.x = -moveSpeed.x;
                transform.rotation = Quaternion.Euler(0, -90, 0);
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
