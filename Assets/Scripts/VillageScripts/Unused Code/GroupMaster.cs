using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GroupMaster : MonoBehaviour
{
    public GameObject target;
    public GameObject pawn;
    public List<NavMeshAgent> pawns;
    public GameObject[] pawnholder;
    [SerializeField] private int pawnNumber;
    public Player player;
    public float openSpace;
    void Start()
    {

        Vector3 center = transform.position;
        for (int i = 0; i < pawnNumber; i++)
        {
            Vector3 pos = RandomCircle(center, 5.0f);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            GameObject p = Instantiate(pawn, pos, rot);
            pawns.Add(p.GetComponent<NavMeshAgent>());
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z+ radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }


    // Update is called once per frame
    void Update()
    {
        foreach(var pawn in pawns)
        {
            pawn.destination = player.transform.position;

        }
    }
}
