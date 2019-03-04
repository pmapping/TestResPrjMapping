using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SliceDrawer : MonoBehaviour {

    private new Camera camera;

    public Material lineMaterial;
    public float lineWidth;
    public float depth = 5;
    public ConfigData configData;
    public SceneLoader sceneLoader;


    private Vector3? lineStartPoint = null;


	// Use this for initialization
	void Start ()
    {
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SaveCalibData();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Key Pressed T");
            sceneLoader.LoadTestfiveAScene();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GetCalibData();
        }


        if (Input.GetMouseButtonDown(0))
        {
            lineStartPoint = GetMouseCameraPoint();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            ParseLine("(-2.2, 1.0, -5.2),(1.3, 0.8, -4.9)");

        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!lineStartPoint.HasValue)
            {
                return;
            }

            var lineEndPoint = GetMouseCameraPoint();
            var gameObject = new GameObject();
            var lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;
            lineRenderer.SetPositions(new Vector3[] { lineStartPoint.Value, lineEndPoint });
            Vector3[] oust = new Vector3[2];
            oust[0] = new Vector3();
            oust[1] = new Vector3();
            
            lineRenderer.GetPositions(oust);
            Debug.Log("oust values" + oust[0]);

            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineStartPoint = null; 
        }	
	}

    private Vector3[] ParseLine(string splitterline)
    {
        //string splitter = "(-2.2, 1.0, -5.2),(1.3, 0.8, -4.9)";
        Vector3[] positions = new Vector3[2];
        string[] output = splitterline.Split(',');

       // Debug.Log(output.Length);

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = output[i].Trim(' ', '(', ')');
        }
    
        positions[0] = new Vector3((float)Convert.ToDouble(output[0]), (float)Convert.ToDouble(output[1]), (float)Convert.ToDouble(output[2]));
        positions[1] = new Vector3((float)Convert.ToDouble(output[3]), (float)Convert.ToDouble(output[4]), (float)Convert.ToDouble(output[5]));
        return new Vector3[2] { positions[0], positions[1] };

        //Debug.Log("2x Vectro3: " + positions[0] +" " + positions[1]);
    }

    private Vector3 GetMouseCameraPoint()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * depth;
    }

    private void SaveCalibData()
    {
        LineRenderer[] lines = GameObject.FindObjectsOfType<LineRenderer>();

        List<string> test = new List<string>();

        for (int i=0; i< lines.Length; i++)
        {
            test.Add(lines[i].GetPosition(0).ToString() + "," + lines[i].GetPosition(1).ToString());
        }

        configData.linesPositons = test.ToArray();
    }

    private void GetCalibData()
    {
        Debug.Log("GetData");
        Vector3[,] mytest = new Vector3[configData.linesPositons.Length, 2];
        
        for(int i = 0; i < configData.linesPositons.Length; i++)
        {
            Vector3[] tempArray = ParseLine(configData.linesPositons[i]);
            mytest[i, 0] = tempArray[0];
            mytest[i, 1] = tempArray[1];
            Debug.Log("fill mytext " + i);
        }

        Debug.Log(mytest.Length);

        for (int i = 0; i < configData.linesPositons.Length; i++)
        {
            var gameObject = new GameObject();
            var lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;
            lineRenderer.SetPositions(new Vector3[] { mytest[i,0], mytest[i,1] });
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            Debug.Log("fill lines " + i);
        }
    }
}
