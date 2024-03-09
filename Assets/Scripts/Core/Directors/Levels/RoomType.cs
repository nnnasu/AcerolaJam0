using Core.Directors;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomType", menuName = "RoomType", order = 0)]
public class RoomType : ScriptableObject {

    [Header("Scaling Formulas")]
    //TODO change these to formula values.
    public float budget;
    public float numberOfSpawns;
    public EnemyWeights SpawnList;


}