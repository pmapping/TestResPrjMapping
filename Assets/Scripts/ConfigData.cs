using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ConfigData")]
public class ConfigData : ScriptableObject {

    public int nbrLines = 0;
    public string[] linesPositons = new string[10];

}
