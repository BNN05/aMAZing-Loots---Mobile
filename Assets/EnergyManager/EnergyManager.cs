using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{

    private int _energy = 0;

    [SerializeField]
    private TMP_Text _text;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = _energy.ToString();
    }

    public void ModifyEnergy(int energy)
    {
        _energy += energy;
    }
}
