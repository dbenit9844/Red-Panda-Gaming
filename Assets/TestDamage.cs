using UnityEngine;

public class TestDamage : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GetComponent<PlayerHealth>().TakeDamage(10);
        }
    }
}