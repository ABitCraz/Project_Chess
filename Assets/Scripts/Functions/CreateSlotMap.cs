using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSlotMap : MonoBehaviour
{
    GameMap gameMap = new GameMap(5, 6);
    private void Awake()
    {
        GameObject emptyslotgo = Instantiate(Resources.Load("Prefabs/Slots/EmptySlot") as GameObject);
        CreateSlots(ref emptyslotgo);
    }

    private void CreateSlots(ref GameObject slotgo)
    {
        Vector3Int spawnplace = Vector3Int.zero;
        int gamemapsizex = gameMap.Size.x;
        int gamemapsizey = gameMap.Size.y;
        for (int i = 0; i < gamemapsizex; i++)
        {
            spawnplace.x = i;
            for (int j = 0; j < gamemapsizey; j++)
            {
                spawnplace.y = j;
                GameObject spawnedslot = Instantiate(slotgo);
                spawnedslot.transform.position = spawnplace;
            }
        }
        Destroy(slotgo);
    }
}
