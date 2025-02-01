using System.Collections;
using UnityEngine;

public class QuestChecker : MonoBehaviour
{
    private NPC[] npcs;

    public QuestChecker()
    {
        npcs = FindObjectsOfType<NPC>();
    }

    public bool AreAllQuestsCompleted
    {
        get
        {
            foreach (var npc in npcs)
            {
                if (npc != null && !npc.QuestCompleted)
                {
                    return false;
                }
            }

            return true;
        }
    }
}