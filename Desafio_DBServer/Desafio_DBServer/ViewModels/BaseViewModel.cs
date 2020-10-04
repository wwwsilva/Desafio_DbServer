using Desafio_DBServer.Enumerates;
using Desafio_DBServer.Helpers;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Desafio_DBServer.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshAllProperties()
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(null));
        }

        public virtual async Task Init() { }

        public virtual async Task Init(NavigationParameterHelper parameters) { }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

        private string messageWait;
        public string MessageWait
        {
            get { return messageWait; }
            set { messageWait = value; OnPropertyChanged(); }
        }

        private OrientationEnum orientation = OrientationEnum.All;
        public OrientationEnum Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        public virtual void OnLayoutChanged() { }

        public event EventHandler OnAppearingEvent;
        public void CallOnAppearing()
        {
            OnAppearingEvent?.Invoke(this, new EventArgs());
        }

        public event EventHandler OnDisappearingEvent;
        public void CallOnDisappearing()
        {
            OnDisappearingEvent?.Invoke(this, new EventArgs());
        }

        public event EventHandler OnLayoutChangedEvent;
        public void CallOnLayoutChanged()
        {
            OnLayoutChangedEvent?.Invoke(this, new EventArgs());
        }

    }
}
