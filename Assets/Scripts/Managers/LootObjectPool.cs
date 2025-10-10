using UnityEngine;
using System.Collections.Generic;

public class LootObjectPool : MonoBehaviour
{
    [SerializeField] int poolSize;

    [SerializeField] GameObject lootPrefab;

    [SerializeField] List<ILootableObject> objectPool = new List<ILootableObject>();

    public static LootObjectPool Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"Too many {name} in scene, deactivating");
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        FillPool();
    }

    private void FillPool()
    {
        for (int i = 0; i < poolSize; i++) 
        {
            GameObject newLoot = Instantiate(lootPrefab, this.transform);
            ILootableObject loot = newLoot.GetComponent<ILootableObject>();
            objectPool.Add(loot);
            loot.gameObject.SetActive(false);
        }
    }

    public void FetchLootObject(Vector3 _position, Transform _parent)
    {
        if (objectPool.Count > 0)
        {
            GameObject loot = objectPool[0].gameObject;

            loot.SetActive(true);
            loot.transform.position = _position;
            loot.transform.parent = _parent;
            objectPool.Remove(objectPool[0]);
        }
        else
        {
            GameObject newLoot = Instantiate(lootPrefab, _parent);
            newLoot.transform.position = _position;
            

        }
    }

    public void ReturnLootToPool(GameObject _loot)
    {
        if(_loot.TryGetComponent<ILootableObject>(out ILootableObject loot))
        {
            objectPool.Add(loot);
            loot.gameObject.SetActive(false);
        }
    }

}
