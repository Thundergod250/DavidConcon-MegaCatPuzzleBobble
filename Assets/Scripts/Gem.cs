using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private GemType gemType;

    public GemType GetGemType() => gemType;

}
