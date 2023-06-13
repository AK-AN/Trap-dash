using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int apples = 0;

    [SerializeField] private TextMeshProUGUI applestext;
    [SerializeField] private AudioSource CollectorSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apples"))
        {
            CollectorSoundEffect.Play();
            Destroy(collision.gameObject);
            apples++;
            Debug.Log("apples: " + apples);
            applestext.text = "Apples: " + apples;
        }
    }
}
