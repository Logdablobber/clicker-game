using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class AutoclickerSpawnerScript : MonoBehaviour
{

    public GameObject autoclickerPrefab;
    public Vector2 ObjStartLocation;
    public float x_offset = 0.5f, y_offset = 0.5f;
    public int autoclickers_per_row = 10;

    GameObject[] autoclickerObjects = { };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateNewAutoclickerObject(int amount_autoclickers)
    {

        autoclickerObjects.Append(Instantiate(autoclickerPrefab, new Vector3(ObjStartLocation.x - (x_offset * (amount_autoclickers % autoclickers_per_row)), ObjStartLocation.y + (y_offset * math.floor(amount_autoclickers / autoclickers_per_row)), 0), Quaternion.identity));

    }
}
