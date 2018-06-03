using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TectC.Contracts;

namespace TestC.Hubs
{
	public class DataXchangeHub : Hub
	{
		private static bool _isWorking;
		private readonly IDataProvider _dataProvider;
		private readonly IHubContext<DataXchangeHub> _hubcontext;

		private static CancellationTokenSource _cancellationTokenSourse = new CancellationTokenSource();

		public DataXchangeHub(IDataProvider dataProvider, IHubContext<DataXchangeHub> hubcontext)
		{
			_dataProvider = dataProvider;
			_hubcontext = hubcontext;
		}

		public async Task SendMessage(string name, string description)
		{
			await _hubcontext.Clients.All.SendAsync("ReceiveMessage", name, description);
		}

		public void StartStopSpamming()
		{
			if (_isWorking)
			{
				_cancellationTokenSourse.Cancel();
			}
			else
			{
				_cancellationTokenSourse = new CancellationTokenSource();
				ThreadPool.QueueUserWorkItem(async obj =>
				{
					var token = (CancellationToken)obj;

					while (!token.IsCancellationRequested)
					{
						string name = _dataProvider.GetName();
						string description = _dataProvider.GetDescription();

						await SendMessage(name, description);

						Thread.Sleep(1000);
					}

				}, _cancellationTokenSourse.Token);
			}

			_isWorking = !_isWorking;
		}
	}
}
