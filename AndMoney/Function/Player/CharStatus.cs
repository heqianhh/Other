using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndMoney {
    public class CharStatus {
        public float health;
        public float healthMax;
        public float healthRate {
            get {
                return health / healthMax;
            }

        }
        public float attack;
        public bool isFully {
            get {
                return health == healthMax;
            }
        }
        public bool isDeath {
            get {
                return health <= 0f;
            }
        }
        public CharStatus() {

        }
        public void SetData(float health, float attack) {
            healthMax = this.health = health;
            this.attack = attack;
        }
        public bool Hurt(float Damage) {
            if (health > Damage) {
                health -= Damage;
                return false;
            }
            else {
                health = 0f;
                return true;
            }
        }
    }
}

