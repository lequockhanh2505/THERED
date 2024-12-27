using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField]
    private GameObject _prefabArrow;
    [SerializeField]
    private Transform _spawnPoint;
    
    Queue<GameObject> objectPoolingArrow = new Queue<GameObject>();
    [SerializeField] private int countPool = 10;
    [SerializeField] GameObject poolParent;

    private void Start()
    {
        _player = GetComponent<Player>();
        for (int i = 0; i < countPool; i++)
        {
            GameObject arrow = Instantiate(_prefabArrow, _spawnPoint.transform.position, Quaternion.identity, poolParent.transform);
            objectPoolingArrow.Enqueue(arrow);
            arrow.SetActive(false);
        }
    }

    public bool GetIsAttackingWithArrow(bool isAttackingWithArrow)
    {
        return isAttackingWithArrow;
    }

    public bool GetIsAttackingNormal(bool isAttackingNormal)
    {
        return isAttackingNormal;
    }

    public void SpawnArrow()
    {
        if (objectPoolingArrow.Count == 0)
        {
            Debug.LogWarning("Hết đối tượng trong pool!");
            return; // Hoặc thêm logic tạo mới nếu cần
        }

        if (objectPoolingArrow.Count > 0)
        {
            GameObject arrow = objectPoolingArrow.Dequeue();

            if (!arrow.activeInHierarchy)
            {
                arrow.SetActive(true);
                arrow.transform.position = _spawnPoint.position;
                arrow.transform.localScale = new Vector3(Mathf.Abs(arrow.transform.localScale.x) * -Mathf.Sign(_player.gameObject.transform.localScale.x), arrow.transform.localScale.y, arrow.transform.localScale.z);

                Vector2 direction = (_spawnPoint.position - transform.position).normalized;
                arrow.GetComponent<Rigidbody2D>().AddForce(direction * 750f, ForceMode2D.Force);

                StartCoroutine(CompleteArrow(arrow));
            }

            objectPoolingArrow.Enqueue(arrow);
        }
    }

    IEnumerator CompleteArrow(GameObject arrow)
    {
        yield return new WaitForSeconds(2f);
        arrow.SetActive(false);
    }


}
