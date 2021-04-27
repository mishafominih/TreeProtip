using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Task : MonoBehaviourSave
{
    public int Lavel;
    public GameObject timeMassage;
    private List<int> tasks;
    private bool first = true;
    private GameObject canvas;
    private const string SAVE_LAVEL = "Lavel";
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        GetTask();
    }

    public override string SaveData()
    {
        return tasks[0] + "." + tasks[1] + "." + tasks[2] + "." + Lavel;
    }

    public override void LoadData(string data)
    {
        var info = data
            .Split('.')
            .Select(x => int.Parse(x))
            .ToArray();
        tasks[0] = info[0];
        tasks[1] = info[1];
        tasks[2] = info[2];
        Lavel = info[3];
        SetTask();
    }

    private void GetTask()
    {
        tasks = GetComponent<Text>().text
            .Split('\n')
            .Skip(1)
            .Select(x =>
            {
                if (x == "") return 0;
                return int.Parse(x);
            })
            .ToList();
    }

    private void SetTask()
    {
        var head = GetComponent<Text>().text
            .Split('\n')
            .First();
        var res = tasks.Select(x => x == 0 ? "" : x.ToString()).ToList();
        GetComponent<Text>().text = head + '\n' + res[0] + '\n' + res[1] + '\n' + res[2];
    }

    // Update is called once per frame
    void Update()
    {
        var treeLavel = TreeSkript.Instance.GetComponent<LavelInfo>().Lavel;
        var sugar = GameInfo.Instance.sugar.GetValue();
        var water = GameInfo.Instance.water.GetValue();
        if (tasks[0] <= treeLavel && tasks[1] <= sugar && tasks[2] <= water && first)
        {
            Instantiate(timeMassage, canvas.transform);
            first = false;
            var realLavel = PlayerPrefs.GetInt(SAVE_LAVEL);
            if (Lavel >= realLavel) PlayerPrefs.SetInt(SAVE_LAVEL, ++realLavel);
            StartCoroutine(enumerator());
        }
        CheckTaskOf(0, treeLavel);
        CheckTaskOf(1, sugar);
        CheckTaskOf(2, water);
    }

    private void CheckTaskOf(int index, float value)
    {
        if (tasks[index] != 0 && tasks[index] <= value)
        {
            tasks[index] = 0;
            SetTask();
        }
    }

    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }

}
