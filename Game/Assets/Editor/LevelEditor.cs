using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelEditor : EditorWindow
{

    private VisualElement rightPane;
    private ListView leftPane;

    private const int brickSize = 50;
    private const int levelHeight = 10;
    private const int levelWidth = 17;

    public static LevelData_SO selectedLevel;
    public static SingleBrickData currentBrick;

    // Button[,] holderButton = new Button[levelWidth, levelHeight];
    [MenuItem("Tools/LevelEditor")]
    public static void ShowMyEditor()
    {
        EditorWindow wnd = GetWindow<LevelEditor>();
        wnd.titleContent = new GUIContent("�ؿ��༭��");

    }
    public void CreateGUI()
    {

        //���عؿ�����
        var allObjectGuids = AssetDatabase.FindAssets("t:LevelData_SO");
        var allObjects = new List<LevelData_SO>();
        foreach (var guid in allObjectGuids)
        {
            allObjects.Add(AssetDatabase.LoadAssetAtPath<LevelData_SO>(AssetDatabase.GUIDToAssetPath(guid)));
        }
        //��������
        var splitView1 = new TwoPaneSplitView(0, 150, TwoPaneSplitViewOrientation.Horizontal);

        rootVisualElement.Add(splitView1);

        leftPane = new ListView();
        splitView1.Add(leftPane);

        rightPane = new VisualElement();
        splitView1.Add(rightPane);

        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => { (item as Label).text = allObjects[index].name; };
        leftPane.itemsSource = allObjects;
        leftPane.onSelectionChange += OnLevelSelectionChange;
    }
    private void OnLevelSelectionChange(IEnumerable<object> _selectedLevels)
    {

        //����Ҳ�����
        rightPane.Clear();

        selectedLevel = _selectedLevels.First() as LevelData_SO;
        if (selectedLevel == null) return;

        //����������ť
        for (int i = 0; i < levelWidth; i++)
        {
            for (int j = 0; j < levelHeight; j++)
            {

                Button holderButton = new Button();
                holderButton.style.position = Position.Absolute;
                holderButton.text = "Holder";
                holderButton.style.height = brickSize;
                holderButton.style.width = brickSize;
                holderButton.style.left = i * brickSize;
                holderButton.style.top = j * brickSize;

                //holderButton.tooltip = i + "," + j;
                rightPane.Add(holderButton);
                //����ש��
                foreach (var brick in selectedLevel.bricks)
                {
                    if (brick.pos.x + (int)levelWidth / 2 == i && brick.pos.y - (int)levelHeight / 2 == -j)
                    {
                        var spriteImage = new Image();
                        spriteImage.scaleMode = ScaleMode.ScaleToFit;
                        spriteImage.sprite = brick.brick.gameObject.GetComponent<SpriteRenderer>().sprite;
                        spriteImage.tintColor = new Color(brick.data.brickColor.r, brick.data.brickColor.g, brick.data.brickColor.b, 1f);
                        holderButton.Add(spriteImage);


                        holderButton.clicked += () =>
                        {
                            
                            holderButton.userData = brick;
                            var window = new BrickEditorWindow(holderButton.userData);
                            window.ShowModal();
                        };
                    }
                }
            }
        }
    }
    //private void OnHolderClicked()
    //{
    //    var window = new BrickEditorWindow();
    //    window.ShowModal();
    //}

    public class BrickEditorWindow : EditorWindow
    {
        SingleBrickData currentBrick;
        public BrickEditorWindow(object _currentBrickData)
        {
            currentBrick = (SingleBrickData)_currentBrickData;
        }
        private void OnEnable()
        {
            //Color test = selectedLevel.

            //var gameObjectBox = new Box();
            //gameObjectBox.Add(new Label("ש��Ԥ����"));
            //gameObjectBox.Add(new ObjectField());
            //rootVisualElement.Add(gameObjectBox);
            //var colorBox = new Box();
            //colorBox.Add(new Label("ש����ɫ"));
            //colorBox.Add(new ColorField());
            //rootVisualElement.Add(colorBox);

            //var countBox = new Box();
            //countBox.Add(new Label("�ܻ�����"));
            //countBox.Add(new IntegerField());
            //rootVisualElement.Add(countBox);

            //var riftBox = new Box();
            //riftBox.Add(new Label("������"));
            //riftBox.Add(new IntegerField());
            //rootVisualElement.Add(riftBox);
        }
        private void CreateGUI()
        {
            rootVisualElement.Add(new Label("��ѡש��λ��"));
            rootVisualElement.Add(new Label(currentBrick.pos.x + " " + currentBrick.pos.y));

            ObjectField brickPrefab = new ObjectField("ש��Ԥ����");
            brickPrefab.value = currentBrick.brick;
            rootVisualElement.Add(brickPrefab);

            ColorField brickColor = new ColorField("ש����ɫ");
            brickColor.value = currentBrick.data.brickColor;
            rootVisualElement.Add(brickColor);
        }
    }
}
