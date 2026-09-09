using UnityEngine;
using System.Collections;


public class HealthDmg_Nico : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] int dmg = 25;

    [SerializeField] float coolDown = 1f;

    [SerializeField] RectTransform healthFill;
    [SerializeField] Transform healthBar;

    private float maxHealth = 100f;
    private float maxBarWidth;

    private bool canTakeDamage = true;

    private void Start()
    {
        maxHealth = health;
        maxBarWidth = healthFill.sizeDelta.x;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Max(health, 0);
        Debug.Log("Health: " + health);
        Debug.Log("Player took damage. Health: " + health);

        UpdateHealthBar();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        float healthPercent = health / maxHealth;

        healthFill.sizeDelta = new Vector2(
            maxBarWidth * healthPercent,
            healthFill.sizeDelta.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canTakeDamage)
        {
            TakeDamage(dmg);
            StartCoroutine(DamageCooldown());
        }
    }

    private void LateUpdate()
    {
        healthBar.rotation = Quaternion.identity;
    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(coolDown);
        canTakeDamage = true;
    }

}
