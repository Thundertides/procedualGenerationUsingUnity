using Random=UnityEngine.Random;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



public class MaterialObject : MonoBehaviour
{
    public int tier;
    public int num;
    public int seed;
    public int hardness;
    public int sharpness;
    public int magicConductivity;
    public int value;
    public float redValue;
    public float blueValue;
    public float greenValue;
    public int loadThis;
    public string materialName;
    public string description;
    public string type;
    public GameObject prefab;
    public Material material;
    public List<Material> materialCrystalList = new List<Material>(); 
    public List<Material> materialOreList = new List<Material>(); 
    public List<GameObject> crystalList = new List<GameObject>();
    public List<GameObject> oreList = new List<GameObject>();

    private string[] prefix = {"bu", "un", "in", "ur", "a", "ab", "ad", "af", "ap", "ante", "anti", "be", "bi", "co", "de", "dia", "dis", "dif", "en", "ex", "fore", "hyper", "hypo", "mal", "mid", "mis", "ob", "non" ,"trans", "ultra", "up"};
    private string[] suffix = {"ium", "otum", "oum", "ity", "or", "ee", "acy", "en", "al", "ate", "ite", "in", "inium", "us"};
    private string[] names = {"ta", "ng", "ra", "an", "ba", "ka", "da", "la", "fe", "be", "te", "ne", "ge"};


    private string[] descriptionHardnessBad = {"is considered to be very soft"};
    private string[] descriptionHardnessOkay = {"is soft"};
    private string[] descriptionHardnessGood = {"is known to be tough"};
    private string[] descriptionHardnessPerfect = {"is considered one of the toughest"};

    private string[] descriptionSharpnessBad = {"is bad for knives"};
    private string[] descriptionSharpnessOkay = {"is known to be brittle"}; 
    private string[] descriptionSharpnessGood = {"can be used to create some great swords"};
    private string[] descriptionSharpnessPerfect = {"rarely if ever shatters"};

    private string[] descriptionMagicConductivityBad = {"is generally disregarded by mages"};
    private string[] descriptionMagicConductivityOkay = {"is commonly used for beginner wands"};
    private string[] descriptionMagicConductivityGood = {"is seeked commonly as magic instructors, as they commonly run out while creating graduation wands"};
    private string[] descriptionMagicConductivityPerfect = {"some of the best mages love using it"};

    private string[] descriptionValueBad = {"saddens people who find it expecting gold"};
    private string[] descriptionValueOkay = {"is commonly found in coal mines"};
    private string[] descriptionValueGood = {"is one of the more common materials used in trade"};
    private string[] descriptionValuePerfect = {"is seeked by people in hopes to become rich"};

    private string[] locations = {"in a cave", "during a lab test", "in a field of flowers", "atop a rare tree", "in the heavens"};

    private string[] people = {"a scientist", "a miner", "a lumberjack", "the prince", "an angel"};

    private string[] sentenceAdditives ={" and also ", " and ", ". This material ", ". This ", "", ""};


    public static List<string> real = new List<string>();
   
    public void generate()
    {
      if(seed == -1)
      {
       seed = Random.Range(0,213123232);  
      }
       Random.InitState(seed);
       int randomNum;
       hardness = tier*Random.Range(1,20);
       sharpness = tier*Random.Range(1,5);
       value = tier*Random.Range(49,101);
       magicConductivity = tier*Random.Range(0,12);

       materialName = nameGenerator();
       sentenceAdditives[4] = $". {materialName} ";
       sentenceAdditives[5] = $", {materialName} ";
       description = descriptionGenerator();

       if(Random.Range(0,4)>1)
       {
              type = "crystal";
       }
       else
       {
              type = "metal";
       }

       if(type == "metal")
       {
            randomNum = Random.Range(1, oreList.Count+1);
            prefab = oreList[randomNum-1];
            randomNum = Random.Range(1, materialOreList.Count+1);
            material = materialOreList[randomNum-1];
       }
       else
       {
            randomNum = Random.Range(1, crystalList.Count+1);
            prefab = crystalList[randomNum-1];
            randomNum = Random.Range(1, materialCrystalList.Count+1);
            material = materialCrystalList[randomNum-1];
       }

       redValue = Random.Range(0.3f, 1f);
       Random.InitState(seed+1);
       blueValue = Random.Range(0.3f, 1f);
       Random.InitState(seed+2);
       greenValue = Random.Range(0.3f, 1f);
       Random.InitState(seed);
       loadThis = -2;
       
       save();

       load();

       
    }
    string descriptionGenerator()
    {
        bool hardnessTest = false;
        bool sharpnessTest = false;
        bool magicTest = false;
        bool locationTest = false;
        bool valueTest = false;
        bool unTest = false;

        int randomNum = 0;

        string descriptionTotal = "";
        descriptionTotal += materialName;
        descriptionTotal += " ";
        while(((!hardnessTest || !sharpnessTest) || (((!magicTest || !locationTest)) || ((!valueTest || !unTest)))))
        {

            randomNum = Random.Range(0, 7);

            if(randomNum == 1 && !hardnessTest)
            {
                if(hardness/tier > 17)
                {
                     randomNum = Random.Range(1, descriptionHardnessPerfect.Length);
                     descriptionTotal += descriptionHardnessPerfect[randomNum-1];
                }
                else if(hardness/tier > 12)
                {
                     randomNum = Random.Range(1, descriptionHardnessGood.Length);
                     descriptionTotal += descriptionHardnessGood[randomNum-1];
                }
                else if(hardness/tier > 6)
                {
                     randomNum = Random.Range(1, descriptionHardnessOkay.Length);
                     descriptionTotal += descriptionHardnessOkay[randomNum-1];
                }
                else
                {
                     randomNum = Random.Range(1, descriptionHardnessBad.Length);
                     descriptionTotal += descriptionHardnessBad[randomNum-1];
                }
                hardnessTest = true;

                if (!((hardnessTest && sharpnessTest) && ((magicTest && locationTest) && (valueTest && unTest))))
                {
                randomNum = Random.Range(1, sentenceAdditives.Length+1);

                descriptionTotal += sentenceAdditives[randomNum-1];
                }

            }
            else if(randomNum == 2 && !sharpnessTest)
            {
                if(sharpness/tier > 4)
                {
                     randomNum = Random.Range(1, descriptionSharpnessPerfect.Length);
                     descriptionTotal += descriptionSharpnessPerfect[randomNum-1];
                }
                else if(sharpness/tier > 3)
                {
                     randomNum = Random.Range(1, descriptionSharpnessGood.Length);
                     descriptionTotal += descriptionSharpnessGood[randomNum-1];
                }
                else if(sharpness/tier > 2)
                {
                     randomNum = Random.Range(1, descriptionSharpnessOkay.Length);
                     descriptionTotal += descriptionSharpnessOkay[randomNum-1];
                }
                else
                {
                     randomNum = Random.Range(1, descriptionSharpnessBad.Length);
                     descriptionTotal += descriptionSharpnessBad[randomNum-1];
                }

                sharpnessTest = true;

                if (!((hardnessTest && sharpnessTest) && ((magicTest && locationTest) && (valueTest && unTest))))
                {
                randomNum = Random.Range(1, sentenceAdditives.Length+1);
                descriptionTotal += sentenceAdditives[randomNum-1];
                }

            }
            else if(randomNum == 3 && !magicTest)
            {
                if(magicConductivity/tier > 10)
                {
                     randomNum = Random.Range(1, descriptionMagicConductivityPerfect.Length);
                     descriptionTotal += descriptionMagicConductivityPerfect[randomNum-1];
                }
                else if(magicConductivity/tier > 7)
                {
                     randomNum = Random.Range(1, descriptionMagicConductivityGood.Length);
                     descriptionTotal += descriptionMagicConductivityGood[randomNum-1];
                }
                else if(magicConductivity/tier > 4)
                {
                     randomNum = Random.Range(1, descriptionMagicConductivityOkay.Length);
                     descriptionTotal += descriptionMagicConductivityOkay[randomNum-1];
                }
                else
                {
                     randomNum = Random.Range(1, descriptionMagicConductivityBad.Length);
                     descriptionTotal += descriptionMagicConductivityBad[randomNum-1];
                }
                magicTest = true;
                if (!((hardnessTest && sharpnessTest) && ((magicTest && locationTest) && (valueTest && unTest))))
                {
                randomNum = Random.Range(1, sentenceAdditives.Length+1);
                descriptionTotal += sentenceAdditives[randomNum-1];
                }

            }
            else if(randomNum == 4 && !locationTest)
            {
                string location = "is rumored to be originally found ";
                
                randomNum = Random.Range(1, locations.Length);
                location += locations[randomNum-1];
                
                location += " by ";

                randomNum = Random.Range(1, people.Length);
                location += people[randomNum-1];

                descriptionTotal += location;
                locationTest = true;
                if (!((hardnessTest && sharpnessTest) && ((magicTest && locationTest) && (valueTest && unTest))))
                {
                randomNum = Random.Range(1, sentenceAdditives.Length+1);
                descriptionTotal += sentenceAdditives[randomNum-1];
                }
            }
            else if(randomNum == 5 && !valueTest)
            {
                if(value/tier > 95)
                {
                     randomNum = Random.Range(1, descriptionValuePerfect.Length);
                     descriptionTotal += descriptionValuePerfect[randomNum-1];
                }
                else if(value/tier > 75)
                {
                     randomNum = Random.Range(1, descriptionValueGood.Length);
                     descriptionTotal += descriptionValueGood[randomNum-1];
                }
                else if(value/tier > 65)
                {
                     randomNum = Random.Range(1, descriptionValueOkay.Length);
                     descriptionTotal += descriptionValueOkay[randomNum-1];
                }
                else
                {
                     randomNum = Random.Range(1, descriptionValueBad.Length);
                     descriptionTotal += descriptionValueBad[randomNum-1];
                }
                valueTest = true;
                if (!((hardnessTest && sharpnessTest) && ((magicTest && locationTest) && (valueTest && unTest))))
                {
                randomNum = Random.Range(1, sentenceAdditives.Length+1);
                descriptionTotal += sentenceAdditives[randomNum-1];
                }
            }
            else if(randomNum == 6 && !unTest)
            {
                descriptionTotal += "bungus";
                unTest = true;
                if (!((hardnessTest && sharpnessTest) && ((magicTest && locationTest) && (valueTest && unTest))))
                {
                randomNum = Random.Range(1, sentenceAdditives.Length+1);
                descriptionTotal += sentenceAdditives[randomNum-1];
                }
            }
        }
        return descriptionTotal;


    }
    // generates names based off 3 lists. 
    string nameGenerator()
    {
        string nameTotal = ""; 
        int randomNum = Random.Range(0, 11);
        if(randomNum > 1)
        {
            Random.InitState(seed+1);
            randomNum = Random.Range(1, prefix.Length+1);
            nameTotal += prefix[randomNum-1];
        }

        Random.InitState(seed+2);
        randomNum = Random.Range(1, names.Length+1);
        nameTotal += names[randomNum-1];

        randomNum = Random.Range(0, 20);
        Random.InitState(seed+2);


        if(randomNum > 1)
        {
            Random.InitState(seed+3);
            randomNum = Random.Range(1, suffix.Length+1);
            nameTotal += suffix[randomNum-1];     
        }

        Random.InitState(seed);
        return nameTotal;
    }
    //Saves material information into a Json File
    void save()
    {
     
     string tempStringSave = ""+tier+num+"";
     BinaryFormatter formatter = new BinaryFormatter();
     string path = Application.persistentDataPath + $"/{tempStringSave}.lu";
     FileStream stream = new FileStream(path, FileMode.Create);


     string saveTrue = JsonUtility.ToJson(this, true);

     formatter.Serialize(stream, saveTrue);
     stream.Close();

    }
    //Loads material information from the Json File
    void load()
    {
        string tempString = ""+tier+num+"";
        string path = Application.persistentDataPath + $"/{tempString}.lu";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            tempString = formatter.Deserialize(stream) as string;

            stream.Close();

            JsonUtility.FromJsonOverwrite(tempString, this);
            GameObject tempObject = Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity);

            tempObject.GetComponent<MeshRenderer>().material = material;

            tempObject.GetComponent<MeshRenderer>().material.color = new Color(redValue, blueValue, greenValue, Random.Range(0.6f,1f)); 
        }
        else
        {
            Debug.LogError("Save File not found at" + path);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
      // if there is a material to load it will load it
      // otherwise it will generate a new one. 
       if (loadThis == -1)
       {
              generate();
       }
       else 
       {
       load();
       }

       //Button btn = generateButton.GetComponent<Button>();
       //btn.onClick.AddListener(generate);
    }

    // Update is called once per frame
    void Update()
    {
    /*
      if(materialName == "bungus")
         {
              Debug.Log(seed);
      }
       else
       {
             generate();
      }
      */
    }
}
