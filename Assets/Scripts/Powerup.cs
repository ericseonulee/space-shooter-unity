using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
    enum PowerupType {
        Tripleshot,
        Speed,
        Shield
    };

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private PowerupType _powerupType;

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.5f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Player player = other.transform.GetComponent<Player>();
            if (player != null) {
                switch (_powerupType) {
                    case PowerupType.Tripleshot:
                        player.TripleShotActive();
                        break;
                    case PowerupType.Speed:
                        player.SpeedActive();
                        Debug.Log("speed");
                        break;
                    case PowerupType.Shield:
                        player.ShieldActive();
                        Debug.Log("shield");
                        break;
                    default:
                        Debug.Log("Default");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
