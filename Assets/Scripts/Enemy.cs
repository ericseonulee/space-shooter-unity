using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private float _speed = 3f;

    private Player _player;
    // Start is called before the first frame update
    void Start() {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.5f) {
            float randomX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Player player = other.transform.GetComponent<Player>();
            if (player != null) {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser") {
            Destroy(other.gameObject);
            
            if (_player != null) {
                _player.AddScore(10);
            }
            Destroy(this.gameObject);
        }
    }
}
