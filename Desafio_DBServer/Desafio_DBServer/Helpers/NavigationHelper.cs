using Desafio_DBServer.ViewModels;
using System;

namespace Desafio_DBServer.Helpers
{
    public class NavigationHelper
    {
        public static event EventHandler AccessPageEvent;

        public static void CallAccessPage(BaseViewModel viewModel)
        {
            AccessPageEvent?.Invoke(viewModel, new EventArgs());
        }
    }
}
