using UnityEngine;
using System.Collections;
using UnityEditor;
using Types;

public class EnemyDesignerWindow : EditorWindow
{
    Texture2D headerSectionTexture;
    Texture2D mageSectionTexture;
    Texture2D warriorSectionTexture;
    Texture2D rogueSectionTexture;

    Color headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);

    Rect headerSection;
    Rect mageSection;
    Rect warriorSection;
    Rect rogueSection;

    static MageData mageData;
    static WorrierData warriorData;
    static RogueData rogueData;

    public static MageData MageInfo { get { return mageData; } }
    public static WorrierData WarriorInfo { get { return warriorData; } }
    public static RogueData RogueInfo { get { return rogueData; } }

    public GUISkin skin;

    [MenuItem("Window/Enemy Designer")] //this line detrmine where can we access this window from unity
    static void OpenWindow()
    {
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }
    //Start function 
    private void OnEnable()
    {
        InitTextures();
        InitData();
    }
    public void InitData()
    {
        mageData = (MageData)ScriptableObject.CreateInstance(typeof(MageData));
        warriorData = (WorrierData)ScriptableObject.CreateInstance(typeof(WorrierData));
        rogueData = (RogueData)ScriptableObject.CreateInstance(typeof(RogueData));
        skin = Resources.Load<GUISkin>("Assets/Resources/GUIStyle/EnemyWindowSkin");

    }
    //intialize all textures
    void InitTextures()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();

        mageSectionTexture = Resources.Load<Texture2D>("Icons/red");
        warriorSectionTexture = Resources.Load<Texture2D>("Icons/green");
        rogueSectionTexture = Resources.Load<Texture2D>("Icons/blue");
    }
    //update function called once per frame,or more times per interaction 
    private void OnGUI()
    {
        DrawLayouts();
        DrawHeaders();
        DrawMageSettings();
        DrawRogueSettings();
        DrawWarriorSettings();
    }
    //define rect values and points textures based on rects
    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 50;

        mageSection.x = 0;
        mageSection.y = 50;
        mageSection.width = Screen.width/3f;
        mageSection.height = Screen.height - 50; ;

        warriorSection.x = Screen.width / 3f;
        warriorSection.y = 50;
        warriorSection.width = Screen.width / 3f;
        warriorSection.height = Screen.height - 50;

        rogueSection.x = (Screen.width / 3f)*2;
        rogueSection.y = 50;
        rogueSection.width = Screen.width / 3f;
        rogueSection.height = Screen.height - 50;
       
        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(mageSection, mageSectionTexture);
        GUI.DrawTexture(warriorSection, warriorSectionTexture);
        GUI.DrawTexture(rogueSection, rogueSectionTexture);
    }
    //draw header content 
    void DrawHeaders()
    {       
        GUILayout.BeginArea(headerSection);
        GUILayout.Label("Enemy Designer");
        GUILayout.EndArea();
    }
    //draw mage content
    void DrawMageSettings()
    {
        GUILayout.BeginArea(mageSection);
        GUILayout.Label("Mage Designer");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damge Type");
        mageData.dmgType = (MAgeDmgType)EditorGUILayout.EnumPopup(mageData.dmgType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon");
        mageData.wpnType = (MageWpnType)EditorGUILayout.EnumPopup(mageData.wpnType);
        EditorGUILayout.EndHorizontal();
        //return true if i click the button
        if (GUILayout.Button("Create", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.MAGE);
        }

        GUILayout.EndArea();
    }
    //draw warrior content
    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warriorSection);
        GUILayout.Label("Warrior Designer");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Class");
        warriorData.classType = (WarriorClassType)EditorGUILayout.EnumPopup(warriorData.classType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon");
        warriorData.wpnType = (WarriorWpnType)EditorGUILayout.EnumPopup(warriorData.wpnType);
        EditorGUILayout.EndHorizontal();

        //return true if i click the button
        if (GUILayout.Button("Create", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.WARRIOR);
        }

        GUILayout.EndArea();
    }
    //draw rogue content
    void DrawRogueSettings()
    {
        GUILayout.BeginArea(rogueSection);
        GUILayout.Label("Rogue Designer");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Strategy");
        rogueData.strategyType = (RogueStrategyType)EditorGUILayout.EnumPopup(rogueData.strategyType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon");
        rogueData.wpnType = (RogueWpnType)EditorGUILayout.EnumPopup(rogueData.wpnType);
        EditorGUILayout.EndHorizontal();

        //return true if i click the button
        if (GUILayout.Button("Create", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.ROGUE);
        }

        GUILayout.EndArea();
    }
}

public class GeneralSettings : EditorWindow
{
    public enum SettingsType
    {
        MAGE,
        WARRIOR,
        ROGUE
    }
    static SettingsType dataSettings;
    static GeneralSettings settingsWindow;
    public static void OpenWindow(SettingsType settings)
    {
        dataSettings = settings;
        settingsWindow = (GeneralSettings)GetWindow(typeof(GeneralSettings));
        settingsWindow.minSize = new Vector2(250, 200);
        settingsWindow.maxSize = new Vector2(250, 350);
        settingsWindow.Show();
    }
    private void OnGUI()
    {
        switch (dataSettings)
        {
            case SettingsType.MAGE:
                {
                    DrawSettings((CharachterData)EnemyDesignerWindow.MageInfo);
                    break;
                }
            case SettingsType.WARRIOR:
                {
                    DrawSettings((CharachterData)EnemyDesignerWindow.WarriorInfo);
                    break;
                }
            case SettingsType.ROGUE:
                {
                    DrawSettings((CharachterData)EnemyDesignerWindow.RogueInfo);
                    break;
                }
        }
    }

    void DrawSettings(CharachterData charachterData)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Prefab");
        charachterData.prefab =(GameObject) EditorGUILayout.ObjectField(charachterData.prefab,typeof(GameObject),false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Health");
        charachterData.maxHealth = EditorGUILayout.FloatField(charachterData.maxHealth);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Energy");
        charachterData.maxEnergy = EditorGUILayout.FloatField(charachterData.maxEnergy);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Power");
        charachterData.power = EditorGUILayout.Slider(charachterData.power, 1, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("% crit Chance");
        charachterData.critChange = EditorGUILayout.Slider(charachterData.critChange, 0, charachterData.power);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name");
        charachterData.name = EditorGUILayout.TextField(charachterData.name);
        EditorGUILayout.EndHorizontal();
        if (charachterData.prefab == null)
        {
            EditorGUILayout.HelpBox("This Enemy needs a [Prefab] befor creating it", MessageType.Warning);
        }
        else if(charachterData.name == null || charachterData.name.Length < 1)
        {
            EditorGUILayout.HelpBox("This Enemy needs a [Name] befor creating it", MessageType.Warning);
        }
        else if (GUILayout.Button("Finish and Save Data", GUILayout.Height(30))){
            SaveCharachterData();
            settingsWindow.Close();
        }

    }
    void SaveCharachterData()
    {
        string prefabPath;
        string newPrefabPath = "Assets/Prefabs/Charchters/";
        string dataPath      = "Assets/Resources/CharachterData/Data/";

        switch (dataSettings)
        {
            case SettingsType.MAGE:
                {
                    dataPath += "Mage/"+EnemyDesignerWindow.MageInfo.name + ".asset";
                    AssetDatabase.CreateAsset(EnemyDesignerWindow.MageInfo, dataPath);

                    newPrefabPath += "Mage/" + EnemyDesignerWindow.MageInfo.name + ".prefab";
                   prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.MageInfo.prefab);
                    AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    GameObject magePrefab = null;
                    magePrefab = PrefabUtility.LoadPrefabContents(newPrefabPath);
                    if (!magePrefab.GetComponent<Mage>())
                    {
                        magePrefab.AddComponent(typeof(Mage));
                    }
                    magePrefab.GetComponent<Mage>().mageData = EnemyDesignerWindow.MageInfo;
                    PrefabUtility.SaveAsPrefabAsset(magePrefab, newPrefabPath);
                    PrefabUtility.UnloadPrefabContents(magePrefab);
                    break;
                }

            case SettingsType.WARRIOR:
                {
                    dataPath += "Worrior/" + EnemyDesignerWindow.WarriorInfo.name + ".asset";
                    AssetDatabase.CreateAsset(EnemyDesignerWindow.WarriorInfo, dataPath);

                    newPrefabPath += "Worrior/" + EnemyDesignerWindow.WarriorInfo.name + ".prefab";
                    prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.WarriorInfo.prefab);
                    AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    GameObject warriorData = null;
                    warriorData = PrefabUtility.LoadPrefabContents(newPrefabPath);
                    if (!warriorData.GetComponent<Warrior>())
                    {
                        warriorData.AddComponent(typeof(Warrior));
                    }
                    warriorData.GetComponent<Warrior>().worrierData = EnemyDesignerWindow.WarriorInfo;
                    PrefabUtility.SaveAsPrefabAsset(warriorData, newPrefabPath);
                    PrefabUtility.UnloadPrefabContents(warriorData);
                    break;
                }
            case SettingsType.ROGUE:
                {
                    dataPath += "Rogue/" + EnemyDesignerWindow.RogueInfo.name + ".asset";
                    AssetDatabase.CreateAsset(EnemyDesignerWindow.RogueInfo, dataPath);

                    newPrefabPath += "Rogue/" + EnemyDesignerWindow.RogueInfo.name + ".prefab";
                    prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.RogueInfo.prefab);
                    AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    GameObject rogouInfo = null;
                    rogouInfo = PrefabUtility.LoadPrefabContents(newPrefabPath);
                    if (!rogouInfo.GetComponent<Rogue>())
                    {
                        rogouInfo.AddComponent(typeof(Rogue));
                    }
                    rogouInfo.GetComponent<Rogue>().rogueData = EnemyDesignerWindow.RogueInfo;
                    PrefabUtility.SaveAsPrefabAsset(rogouInfo, newPrefabPath);
                    PrefabUtility.UnloadPrefabContents(rogouInfo);
                    break;
                }
        }
    }
}
