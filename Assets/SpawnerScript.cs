using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    Transform[] entrances;
    [SerializeField] GameObject guard,moskito,magician;

    public static int level;
    public static int points;

    public int round = 0;
    bool flag = false;

    float lvl0Cd = 4f, lvl0Time = 4f;

    float lvl1GuardCd = 3.5f, lvl1GuardTime = 5f;
    float lvl1MoskitoCd = 10f, lvl1MoskitoTime = 10f;

    float lvl2GuardCd = 3.5f, lvl2GuardTime = 5f;
    float lvl2MoskitoCd = 10f, lvl2MoskitoTime = 10f;
    float lvl2CatapultCd = 12f, lvl2CatapultTime = 5f;
    float lvl2EyeCd = 10f, lvl2EyeTime = 10f;

    float lvl3GuardCd = 3f, lvl3GuardTime = 5f;
    float lvl3MoskitoCd = 9f, lvl3MoskitoTime = 10f;
    float lvl3CatapultCd = 12f, lvl3CatapultTime = 5f;
    float lvl3EyeCd = 10f, lvl3EyeTime = 10f;

    int catapultsLeft = 3;
    // Start is called before the first frame update
    void Start()
    {
        entrances = GetComponentsInChildren<Transform>();
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (HudScript.dead) return;
        SetLevel();

        if (flag)
        {
            StartRound(round);
            round++;
        }

        if (level == 0)
        {
            if (lvl0Time > lvl0Cd)
            {
                lvl0Time = 0;
                for (int i = 0; i < Random.Range(1, 3); i++)
                    Instantiate(guard, entrances[Random.Range(1, entrances.Length)].position, Quaternion.identity);
                return;
            }
            lvl0Time += Time.deltaTime;
        }

        if (level == 1)
        {
            if (lvl1GuardTime > lvl1GuardCd)
            {
                lvl1GuardTime = 0;
                for (int i = 0; i < Random.Range(2, 4); i++)
                        Instantiate(guard, entrances[Random.Range(1, entrances.Length)].position, Quaternion.identity);
            }
            lvl1GuardTime += Time.deltaTime;

            if (lvl1MoskitoTime > lvl1MoskitoCd)
            {
                lvl1MoskitoTime = 0;
                for (int i = 0; i < Random.Range(1, 3); i++)
                    Instantiate(moskito, entrances[Random.Range(1, entrances.Length)].position, Quaternion.identity);
                
            }
            lvl1MoskitoTime += Time.deltaTime;
            return;
        }

        if (level == 2)
        {
            if (lvl2GuardTime > lvl2GuardCd)
            {
                lvl2GuardTime = 0;
                for (int i = 0; i < Random.Range(2, 4); i++)
                    Instantiate(guard, entrances[Random.Range(1, entrances.Length)].position, Quaternion.identity);
            }
            lvl2GuardTime += Time.deltaTime;

            if (lvl2MoskitoTime > lvl2MoskitoCd)
            {
                lvl2MoskitoTime = 0;
                for (int i = 0; i < Random.Range(1, 3); i++)
                    Instantiate(moskito, entrances[Random.Range(1, entrances.Length)].position, Quaternion.identity);

            }
            lvl2MoskitoTime += Time.deltaTime;

            if (lvl2EyeTime > lvl2EyeCd)
            {
                lvl2EyeTime = 0;
                var eyeNumber = Random.Range(1, 3);
                List<int> list = new List<int>();
                for (int i = 0; i < eyeNumber; i++)
                {
                    var flag = true;
                    while (flag)
                    {
                        var j = Random.Range(0, 13);
                        if (!list.Contains(j))
                        {
                            flag = false;
                            list.Add(j);
                        }
                    }
                }
                foreach(int number in list)
                {
                    EvilEyeActivationScript.current.Activate(number);
                }
                    
            }
            lvl2EyeTime += Time.deltaTime;

            if (lvl2CatapultTime > lvl2CatapultCd && catapultsLeft>0)
            {
                lvl2CatapultTime = 0;
                catapultsLeft--;
                    CatapultScript.current.Fire();
            }
            lvl2CatapultTime += Time.deltaTime;
        }

        if (level == 3)
        {

            magician.SetActive(true);
            if (lvl3GuardTime > lvl3GuardCd)
            {
                lvl3GuardTime = 0;
                for (int i = 0; i < Random.Range(2, 4); i++)
                    Instantiate(guard, entrances[Random.Range(1, entrances.Length)].position, Quaternion.identity);
            }
            lvl3GuardTime += Time.deltaTime;

            if (lvl3MoskitoTime > lvl3MoskitoCd)
            {
                lvl3MoskitoTime = 0;
                for (int i = 0; i < Random.Range(1, 3); i++)
                    Instantiate(moskito, entrances[Random.Range(1, entrances.Length)].position, Quaternion.identity);

            }
            lvl3MoskitoTime += Time.deltaTime;

            if (lvl3EyeTime > lvl3EyeCd)
            {
                lvl3EyeTime = 0;
                var eyeNumber = Random.Range(1, 3);
                List<int> list = new List<int>();
                for (int i = 0; i < eyeNumber; i++)
                {
                    var flag = true;
                    while (flag)
                    {
                        var j = Random.Range(0, 13);
                        if (!list.Contains(j))
                        {
                            flag = false;
                            list.Add(j);
                        }
                    }
                }
                foreach (int number in list)
                {
                    EvilEyeActivationScript.current.Activate(number);
                }

            }
            lvl3EyeTime += Time.deltaTime;

            if (lvl3CatapultTime > lvl3CatapultCd && catapultsLeft > 0)
            {
                lvl2CatapultTime = 0;
                catapultsLeft--;
                CatapultScript.current.Fire();
            }
            lvl3CatapultTime += Time.deltaTime;
        }

    }

    private void SetLevel()
    {
        if (points > 125)
        {
            level = 3;
            return;
        }
        if (points > 50)
        {
            level = 2;
            return;
        }
        if (points > 10)
        {
            level = 1;
            return;
        }
        else
        {
            level = 0;
            return;
        }
    }

    private void StartRound(int round)
    {
        switch (round)
        {
            case 0:
                for (int i = 0; i < 6; i++)
                {
                    Instantiate(guard, entrances[Random.Range(1, entrances.Length)].position, Quaternion.identity);
                }
                break;
        }
    }
}
