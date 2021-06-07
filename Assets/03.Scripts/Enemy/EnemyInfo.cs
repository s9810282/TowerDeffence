using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    [System.Serializable]
    public class EnemyInfo
    {
        public string enemyName;
        public MonsterType enemyType;

        public float hp;
        public float maxHp;

        public float speed;
    }
}
