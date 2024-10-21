using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public GameObject block;
    public GameObject block2;
    public GameObject goal;
    public GameObject coin;
    public TextMeshProUGUI scoreText;
    public static int score = 0;
    public GameObject goalParticle;

    public string nextSceneName;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, false);

        //èâä˙âª
        score = 0;

        Vector3 position = Vector3.zero;
        Instantiate(block, position, Quaternion.identity);
        int[,] map =
        {
           {1,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,1},
           {1,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,1},
           {1,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,1},
           {1,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,1},
           {1,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,1},
           {1,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,1},
           {1,0,0,0,0,3,0,0,0,0,  0,0,0,0,0,0,0,0,0,1},
           {1,0,0,0,1,1,1,0,0,0,  0,0,0,0,0,3,0,0,0,1},
           {1,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,2,0,1},
           {1,1,1,1,1,1,1,1,1,1,  1,1,1,1,1,1,1,1,1,1},
        };

        int lenY = map.GetLength(0);
        int lenX = map.GetLength(1);

        for (int x = 0; x < 20; x++)
        {
            position.x = x;
            Instantiate(block, position, Quaternion.identity);

            for (int y = 0; y < 10; y++)
            {
                // ÉuÉçÉbÉN
                position.y = -y + 5;
                if (map[y, x] == 1)
                {
                    Instantiate(block, position, Quaternion.identity);
                }
                // ÉSÅ[Éã
                if (map[y, x] == 2)
                {
                    goal.transform.position = position;
                    goalParticle.transform.position = position;
                }
                // ÉRÉCÉì
                if (map[y, x] == 3)
                {
                    Instantiate(coin, position, Quaternion.identity);
                }

            }
        }

        //îwåi
        for (int y = 0; y < lenY; y++)
        {
            for (int x = 0; x < lenX; x++)
            {
                position.x = x;
                position.y = -y + 5;
                position.z = 1;
                Instantiate(block2, position, Quaternion.identity);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GoalScript.isGameClear == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
        scoreText.text = "SCORE" + score;
    }
}
