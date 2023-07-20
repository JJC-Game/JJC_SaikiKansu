using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCell : MonoBehaviour
{
    public int cellNumber = 0;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.Find("Text").GetComponent<Text>();
        int.TryParse(text.text, out cellNumber );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
