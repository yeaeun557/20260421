using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public GameObject restart;
    public DustSpawner ds;

    [SerializeField]
    private float moveSpeed = 4.5f;
    public int number;
    public Rigidbody2D rigid;
    public int growCount;

    public System.Collections.Generic.List<Sprite> playerImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        number = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (growCount >= 8 && number <= 3)
        {
            number++;
            growCount = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = playerImage[number];
        }

        if (number == 4)
        {
            if (growCount >= 4)
            {
                SceneManager.LoadScene("ending");
            }
        }

        Vector3 moveVel = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVel = new Vector3(-1, moveVel.y);
        } else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVel = new Vector3(1, moveVel.y);
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            moveVel = new Vector3(moveVel.x, -1);
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            moveVel = new Vector3(moveVel.x, 1);
        }

        moveVel.Normalize();

        transform.position += moveSpeed * moveVel * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Dust>().number <= number)
        {
            Destroy(collision.gameObject);
            growCount += 1;
            ds.eatableDustCount -= 1;
        } else
        {
            Destroy(gameObject);
            Debug.Log("게임오버");
            restart.SetActive(true);
        }
    }
}
