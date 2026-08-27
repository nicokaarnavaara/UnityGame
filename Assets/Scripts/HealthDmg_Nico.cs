using UnityEngine;
using System.Collections;


public class HealthDmg_Nico : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] int dmg = 25;

    [SerializeField] float coolDown = 1f;

    private bool canTakeDamage = true;
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);
        Debug.Log("Player took damage. Health: " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canTakeDamage)
        {
            TakeDamage(dmg);
            StartCoroutine(DamageCooldown());
        }
    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(coolDown);
        canTakeDamage = true;
    }

}
