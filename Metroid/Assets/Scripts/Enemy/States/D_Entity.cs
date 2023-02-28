using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float wallCheckDistance;
    public float ledgeCheckDistance;

    public float maxAggroDistance = 4f;
    public float minAggroDistance = 3f;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
