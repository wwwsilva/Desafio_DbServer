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
