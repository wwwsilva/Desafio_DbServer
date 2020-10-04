using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Desafio_DBServer.Model.System
{
    public class BaseObjectModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler eventHandler = this.PropertyChanged;

            if (eventHandler == null)
                return;

            if (PropertyChanged == null)
                return;

            eventHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshAllProperties()
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
