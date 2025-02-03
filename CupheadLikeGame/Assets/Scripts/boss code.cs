using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public int bossHealth = 20;
    public int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = bossHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
