using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SearchTypes", menuName = "ScriptableObjects/SearchTypes")]
public class SearchTypes : ScriptableObject
{
    public List<string> searchTypes;
}