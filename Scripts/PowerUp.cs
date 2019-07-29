using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int powerUpID;//0 = tripleshot, 1 = speedboost, 2 = shields
    [SerializeField]
    private AudioClip _clip;
    

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_clip, transform.position,1f);
                
                //if powerUpID == 0 
                if (powerUpID == 0)
                {
                   // player.canTripleShot = true;
                   //player.StartCoroutine(player.TripleShotPowerDownRoutine());
                    player.TripleShotPowerupOn();
                    
                }
                else if (powerUpID == 1)
                {
                    player.SpeedPowerUpOn();
                   

                }
                else if (powerUpID == 2)
                {
                    player.ShieldPowerupOn();
                    
                }
                //enable speed boost here

                //enableShields
            }
            Destroy(this.gameObject);
        }
    }
}
