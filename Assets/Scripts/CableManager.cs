using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CableManager : MonoBehaviour
{
    private GameObject point=  null;
    public GameObject cablePrefab;
    private List<GameObject> cables = new List<GameObject>();
    private List<(GameObject, int)> bornes = new List<(GameObject, int)>();
    public int nbGroupes = 0;
    
    public void AddPoint(GameObject obj)
    {
        if (point == null)
        {
            point = obj;
        }
        else
        {
            if (obj != point)
            {
                Vector3 startPosition = obj.transform.position, endPosition = point.transform.position;
                Vector3 cylinderPosition = (startPosition + endPosition) / 2f;
                Quaternion cylinderRotation = Quaternion.LookRotation(Vector3.Cross((endPosition - startPosition), Vector3.up), endPosition - startPosition);
                GameObject newCable = Instantiate(cablePrefab, cylinderPosition, cylinderRotation);
                float distance = Vector3.Distance(startPosition, endPosition);
                newCable.transform.localScale = new Vector3(0.1f, distance / 2f, 0.1f);
                cables.Add(newCable);
                bool todo = true;
                bool a, b;
                for (int i = 0; i < nbGroupes && todo; i++)
                {
                    a = bornes.Contains((obj,i));
                    b = bornes.Contains((point,i));
                    if (a || b)
                    {
                        todo = false;
                        if(!a)
                        {
                            bool cont = true;
                            for (int j = i+1;j< nbGroupes && cont; j++)
                            {
                                if (bornes.Contains((obj,j)))
                                {
                                    for (int k = 0; k < bornes.Count; k++) if (bornes[k].Item2 == j) bornes[k] = (bornes[k].Item1, i);
                                    cont = false;
                                }

                            }
                            if (cont) bornes.Add((obj,i));
                        }
                        if(!b)
                        {
                            bool cont = true;
                            for (int j = i + 1; j < nbGroupes && cont; j++)
                            {
                                if (bornes.Contains((point,j)))
                                {
                                    for (int k = 0; k < bornes.Count; k++) if (bornes[k].Item2 == j) bornes[k] = (bornes[k].Item1, i);
                                    cont = false;
                                }

                            }
                            if (cont) bornes.Add((point, i));
                        }
                    }
                }
                if (todo) 
                {
                    bornes.Add((obj, nbGroupes));
                    bornes.Add((point, nbGroupes));
                    nbGroupes++;
                }
            }
            point = null;

            // Activate or disactivate everything
            UpdateElectricThings();
        }
    }

    public void UpdateElectricThings()
    {
        foreach (var obj in bornes)
            obj.Item1.transform.parent.gameObject.GetComponent<CompoScript>().Deactivate();
        HashSet<GameObject> seen = new HashSet<GameObject>();

        Dictionary<int, List<(CompoScript, int)>> paths = new Dictionary<int, List<(CompoScript, int)>>();
        HashSet<(int, int, CompoScript)> power = new HashSet<(int, int, CompoScript)>();
        for (int i = 0; i < bornes.Count; i++)
        {
            GameObject a = bornes[i].Item1.transform.parent.gameObject;
            for (int j = i + 1; j < bornes.Count; j++)
                if (a.Equals(bornes[j].Item1.transform.parent.gameObject))
                {
                    CompoScript b = a.GetComponent<CompoScript>();
                    if (bornes[i].Item2 == bornes[j].Item2)
                    {
                        if (b.isPower) b.Explode();
                    }
                    else
                    {
                        int k = bornes[i].Item2, l = bornes[j].Item2;
                        if (bornes[i].Item1.name.Equals("BorneP")) { int t = k; k = l; l = t; }
                        if (b.isPower) power.Add((k, l, b));
                        else
                        {
                            if (!b.blockPtoM)
                            {
                                if (!paths.ContainsKey(k)) paths[k] = new List<(CompoScript, int)>();
                                paths[k].Add((b, l));
                            }
                            if (!b.blockMtoP)
                            {
                                if (!paths.ContainsKey(l)) paths[l] = new List<(CompoScript, int)>();
                                paths[l].Add((b, k));
                            }
                        }
                    }
                }
        }
        foreach (var a in power)
        {
            if (SearchPath(new List<int>(), a.Item1, a.Item2, paths)) a.Item3.Activate();
            else a.Item3.Deactivate();
        }
    }

    private bool SearchPath(List<int> prev, int start,int goal, Dictionary<int, List<(CompoScript,int)>> paths)
    {
        if (start == goal) return true;
        if (!paths.ContainsKey(start)) return false;
        prev.Add(start);
        bool ans = false;
        foreach (var a in paths[start])
        {
            if (!prev.Contains(a.Item2)) { 
            if (SearchPath(new List<int>(prev), a.Item2, goal, paths))
            {
                a.Item1.Activate();
                ans = true;
            } 
            }
        }
        return ans;
    }

    public void Remove() {
        for (int i = 0; i < cables.Count; i++)
        {
            Destroy(cables[i]);
        }
        cables.Clear();
        foreach (var obj in bornes)
             obj.Item1.transform.parent.gameObject.GetComponent<CompoScript>().Deactivate();
        bornes.Clear();
    }

    public void RemoveAll()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
