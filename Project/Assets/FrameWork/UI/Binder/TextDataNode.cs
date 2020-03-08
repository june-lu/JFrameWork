using UnityEngine.UI;

namespace UIBinder
{
    public class TextDataNode : DataNode<string>
    {
        private Text text;
        public TextDataNode(Text text, string data) : base(data)
        {
            this.text = text;
            Set = (value) =>
            {
                text.text = value;
            };

            Get = () =>
            {
                return Data;
            };
        }

        protected override void OnValueChanged(string data)
        {
            this.text.text = data;
        }
    }
}
