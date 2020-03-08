using UnityEngine.UI;

namespace UIBinder
{
    public class InputFieldDataNode : DataNode<string>
    {
        public InputFieldDataNode(InputField inputField, string data) : base(data)
        {
            Set = (value) =>
            {
                inputField.text = value;
            };

            Get = () =>
            {
                return inputField.text;
            };

            inputField.onValueChanged.AddListener((changeValue) =>
            {
                Data = changeValue;
            });
        }
    }
}
