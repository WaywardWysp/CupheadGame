using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public int bossHealth = 20;
    public int currentHealth;
    public GameObject target;
    public GameObject trigger1;
    public GameObject trigger2;
    public float attackCooldown = 3f;

    public bool canAttack = true;

    public bool timerIsRunning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = bossHealth;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player") && canAttack)
        {
            //this is where your attacks will go

            if (collision.gameObject == trigger1)
            {
                timerIsRunning = true;
                if (attackCooldown > 0)
                {
                    attackCooldown -= Time.deltaTime;

                }
                else
                {
                    attackCooldown = 0;
                    timerIsRunning = false;
                    canAttack = true;

                }
            }

            if (collision.gameObject == trigger2)
            {
                timerIsRunning = true;
                if (attackCooldown > 0)
                {
                    attackCooldown -= Time.deltaTime;

                }
                else
                {
                    attackCooldown = 0;
                    timerIsRunning = false;
                    canAttack = true;
                }
            }


        }
    }
}