using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundCon : MonoBehaviour
{
    [SerializeField] Vector2 vel;
    Material back;
    void Awake()
    {
        vel = new Vector2(0.1f, 0);
        back = GetComponent<Renderer>().material;
    }
    void Start()
    {
        
    }
    void Update()
    {

        back.mainTextureOffset += vel * Time.deltaTime;
    }
}
