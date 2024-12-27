using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButtetSkill : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag; // Tên loại đạn, ví dụ: "Bullet", "ExplosiveBullet"
        public GameObject prefab; // Prefab của loại đạn
        public int size; // Kích thước pool
    }

    public static SpawnButtetSkill Instance;
    public GameObject objectToPool;
    public int poolSize = 3;


    public List<GameObject> BulletSkills;

    void Awake()
    {
        Instance = this; // Khởi tạo singleton
    }
    private void Start()
    {
        BulletSkills = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectToPool, transform);
            obj.SetActive(false);
            BulletSkills.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject objInPool in BulletSkills)
        {
            if (!objInPool.activeInHierarchy)
            {
                return objInPool;
            }
        }

        GameObject objNew = Instantiate(objectToPool, transform);
        objNew.SetActive(false);
        BulletSkills.Add(objNew);
        return objNew;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            GameObject test = SpawnButtetSkill.Instance.GetPooledObject();
            test.SetActive(true);
        }
    }
}
