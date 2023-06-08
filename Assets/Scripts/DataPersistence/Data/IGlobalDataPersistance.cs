using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGlobalDataPersistance
{
    void LoadData(AttributesData data);

    void SaveData(AttributesData data);
}
