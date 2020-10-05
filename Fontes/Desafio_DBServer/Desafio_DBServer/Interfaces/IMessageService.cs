using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Desafio_DBServer.Interfaces
{
    public interface IMessageService
    {
		Task ShowMessage(string title, string message, string button);
	}
}
