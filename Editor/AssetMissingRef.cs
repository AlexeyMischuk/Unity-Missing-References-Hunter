using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that holds reference to Asset with missing reference;
/// and corresponding Component name within which reference is missed and number of those references
/// </summary>
public class AssetMissingRef
{
    public Object Asset { get; set; }
    public Dictionary<string, int> Component { get; set; }

    public AssetMissingRef(Object asset, Dictionary<string, int> component)
    {
        Asset = asset;
        Component = component;
    }
    
}
