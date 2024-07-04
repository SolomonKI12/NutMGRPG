using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    public GameObject damageTextPrefab; // Prefab for the damage display text

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a projectile
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Get all MonoBehaviours attached to the collided projectile
            MonoBehaviour[] scripts = collision.gameObject.GetComponents<MonoBehaviour>();

            // Iterate through the scripts and check for the 'damage' variable
            foreach (MonoBehaviour script in scripts)
            {
                // Get the 'damage' field using reflection
                System.Reflection.FieldInfo damageField = script.GetType().GetField("damage");

                // Check if the 'damage' field is found and is of type int
                if (damageField != null && damageField.FieldType == typeof(int))
                {
                    // Get the damage value from the field
                    int damage = (int)damageField.GetValue(script);

                    // Instantiate the damage text at the same position as the goblin
                    Instantiate(damageTextPrefab, transform.position, Quaternion.identity)
                        .GetComponent<TextMesh>().text = damage.ToString();

                    break; // Exit the loop after finding the first 'damage' variable
                }
            }
        }
    }
}