using Desafio_DBServer.Model.System;
using Xamarin.Forms;

namespace Desafio_DBServer.Model
{
    public class TypeModel : BaseObjectModel
    {
        public TypeModel(Color _bgColor1, Color _bgColor2, Color _txColor, string _text)
        {
            BackgroundColor1 = _bgColor1;
            BackgroundColor2 = _bgColor2;
            TextColor = _txColor;
            Text = _text;
            Visible = true;
        }

        public TypeModel(bool _visible)
        {
            Visible = _visible;
        }

        private Color backgroundColor1;
        public Color BackgroundColor1
        {
            get { return backgroundColor1; }
            set { backgroundColor1 = value; OnPropertyChanged(); }
        }

        private Color backgroundColor2;
        public Color BackgroundColor2
        {
            get { return backgroundColor2; }
            set { backgroundColor2 = value; OnPropertyChanged(); }
        }

        private Color textColor;
        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; OnPropertyChanged(); }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged(); }
        }

        private bool visible;
        public bool Visible
        {
            get { return visible; }
            set { visible = value; OnPropertyChanged(); }
        }
    }
}
