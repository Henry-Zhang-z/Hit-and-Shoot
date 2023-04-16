using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelEditor : EditorWindow
{

    private static VisualElement rightPane;
    private static ListView leftPane;

    private const int brickSize = 40;
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
        var splitView1 = new TwoPaneSplitView(0, 100, TwoPaneSplitViewOrientation.Horizontal);
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
    public static void UpdateRightPane()
    {
        //����Ҳ�����
        rightPane.Clear();
        //������Ӱ�ť
        for (int i = 0; i < levelWidth; i++)
        {
            for (int j = 0; j < levelHeight; j++)
            {

                Button holderButton = new Button();
                holderButton.style.position = Position.Absolute;
                holderButton.text = "Add";
                holderButton.style.height = brickSize;
                holderButton.style.width = brickSize;
                holderButton.style.left = i * brickSize;
                holderButton.style.top = j * brickSize;


                rightPane.Add(holderButton);

                //����ש��ͼƬ
                foreach (var brick in selectedLevel.bricks)
                {
                    //��ǰλ����ש��
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

                //������Brick
                if (holderButton.childCount == 0)
                {
                    Vector2 newPos = new Vector2(i - (int)levelWidth / 2, -j + (int)levelHeight / 2);
                    holderButton.clicked += () =>
                    {
                        //��ʼ����Brick
                        SingleBrickData newBrick = new SingleBrickData();
                        newBrick.brick = Resources.Load<GameObject>("Prefabs/Brick");
                        newBrick.pos = newPos;

                        newBrick.data = new BrickData();
                        newBrick.data.count = 1;
                        newBrick.data.riftCount = 0;
                        newBrick.data.brickColor = Color.white;

                        selectedLevel.bricks.Add(newBrick);
                        //��������
                        holderButton.userData = newBrick;
                        var window = new BrickEditorWindow(holderButton.userData);
                        window.ShowModal();
                    };
                }
            }
        }
        ////���ƹؿ�������Ϣ
        //Box baseInfoBox = new Box();
        //baseInfoBox.style.position = Position.Absolute;
        //baseInfoBox.style.bottom = 10;
        //baseInfoBox.Add(new Label("�ؿ�������Ϣ"));
        //rightPane.Add(baseInfoBox);
    }
    private void OnLevelSelectionChange(IEnumerable<object> _selectedLevels)
    {
        selectedLevel = _selectedLevels.First() as LevelData_SO;
        if (selectedLevel == null) return;

        UpdateRightPane();

    }

    public class BrickEditorWindow : EditorWindow
    {
        SingleBrickData currentBrick;


        ObjectField brickPrefab = new ObjectField("ש��Ԥ����");
        ColorField brickColor = new ColorField("ש����ɫ");
        IntegerField brickCount = new IntegerField("�������");
        IntegerField brickRiftCount = new IntegerField("������");
        Button deleteButton = new Button();
        public BrickEditorWindow(object _currentBrickData)
        {
            currentBrick = (SingleBrickData)_currentBrickData;
        }

        private void CreateGUI()
        {

            rootVisualElement.Add(new Label("��ѡש��λ��"));
            rootVisualElement.Add(new Label(currentBrick.pos.x + " " + currentBrick.pos.y));

            //��ȡ����

            brickPrefab.value = currentBrick.brick;
            rootVisualElement.Add(brickPrefab);


            brickColor.value = currentBrick.data.brickColor;
            rootVisualElement.Add(brickColor);

            brickCount.value = currentBrick.data.count;
            rootVisualElement.Add(brickCount);

            brickRiftCount.value = currentBrick.data.riftCount;
            rootVisualElement.Add(brickRiftCount);


            deleteButton.text = "ɾ����ǰש��";
            deleteButton.clicked += () =>
            {
                foreach (var brick in selectedLevel.bricks)
                {
                    if (brick.pos == currentBrick.pos)
                    {
                        selectedLevel.bricks.Remove(brick);
                        LevelEditor.UpdateRightPane();
                        //�رմ���
                        Close();
                        break;
                    }
                }
            };
            rootVisualElement.Add(deleteButton);
        }

        private void OnGUI()
        {
            currentBrick.data.brickColor = brickColor.value;
            currentBrick.data.count = brickCount.value;
            currentBrick.data.riftCount = brickRiftCount.value;

            LevelEditor.UpdateRightPane();
        }
    }
}
