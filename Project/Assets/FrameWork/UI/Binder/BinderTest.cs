using UIBinder;
using UnityEngine;
using UnityEngine.UI;

public class BinderTest : MonoBehaviour
{
    public InputField InputField;
    public Text text;
    public Text text1;
    public Text text2;

    private InputFieldDataNode inputDataNode;
    private TextDataNode textDataNode;
    private TextDataNode text1DataNode;
    private TextDataNode text2DataNode;


    // Start is called before the first frame update
    void Start()
    {


    }

    void OnEnable()
    {
        inputDataNode = new InputFieldDataNode(InputField, "");
        textDataNode = new TextDataNode(text, "");
        text1DataNode = new TextDataNode(text1, "");
        text2DataNode = new TextDataNode(text2, "");

        inputDataNode.Bind(textDataNode, null);
        text1DataNode.Bind(textDataNode, null);
        text2DataNode.Bind(text1DataNode, null);

    }

    void OnDisable()
    {
        textDataNode.UnBind(textDataNode);
        text1DataNode.UnBind(textDataNode);
        text2DataNode.UnBind(text1DataNode);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            textDataNode.Data = "1";
        }
        else if (Input.GetMouseButtonDown(1))
        {
            textDataNode.Data = "2";
        }
    }
}
