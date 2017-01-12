using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;

namespace Sansagol.ArithmeticMaster
{
    public sealed class OperationResultMessage
    {
        private string _Name = string.Empty;
        private Image _Pict = null;
        private Brush _Color = Brushes.Black;

        public string Message { get { return _Name; } set { _Name = value; } }
        public Brush Color { get { return _Color; } set { _Color = value; } }
        public Image Pict { get { return _Pict; } set { _Pict = value; } }

        public OperationResultMessage(string mess, Brush color)
        {
            _Name = mess;
            _Color = color;
        }

    }
}
