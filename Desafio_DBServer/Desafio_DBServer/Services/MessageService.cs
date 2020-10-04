using Desafio_DBServer.Interfaces;
using Desafio_DBServer.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(MessageService))]
namespace Desafio_DBServer.Services
{
	public class MessageService : IMessageService
	{
		public async Task ShowMessage(string title, string message, string button)
		{
			try
			{
				await GetNavigationPage().DisplayAlert(title, message, button);
			}
			catch { }
		}

		public async Task<bool> ShowMessage(string title, string message, string accept, string cancel)
		{
			bool retorno = false;
			try
			{
				retorno = await GetNavigationPage().DisplayAlert(title, message, accept, cancel);
			}
			catch { }
			return retorno;
		}

		public async Task ShowMessageException(string message, Exception ex)
		{
			try
			{
				string exMessage = string.Empty;
				if (ex != null)
				{
					exMessage = ex.Message;
				}

				if (!string.IsNullOrWhiteSpace(exMessage))
					await GetNavigationPage().DisplayAlert("Problema", message + Environment.NewLine + Environment.NewLine + "Mensagem: " + Environment.NewLine + exMessage, "OK");
				else
					await GetNavigationPage().DisplayAlert("Problema", message, "OK");
			}
			catch { }
		}

		private Page GetNavigationPage()
		{
			if (Application.Current.MainPage is MasterDetailPage)
			{
				return (Application.Current.MainPage as MasterDetailPage).Detail;
			}
			else
			{
				return Application.Current.MainPage;
			}
		}
	}
}
