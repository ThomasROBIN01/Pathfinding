using UnityEngine;

public class ITreasureChest : MonoBehaviour, IInteract
{
    bool alreadyLooted = false;

    public void Activate()
    {
        if (!alreadyLooted) 
        {
            // spawn some loot 
            Vector3 spawnLocation = transform.position - transform.forward;
            LootObjectPool.Instance.FetchLootObject(spawnLocation, transform);

            alreadyLooted = true;
        }
        else
        {
            Debug.Log("Already looted :(");
        }
    }

}
