﻿using System;
using System.Threading.Tasks;
using RestSharp;

namespace LIC.Malone.Core
{
	public class ApiClient
	{
		public async Task<SendResult> Send(Request request)
		{
			return await Send(request.ToMaloneRestRequest());
		}

		private async Task<SendResult> Send(MaloneRestRequest request)
		{
			var result = new SendResult();

			var client = new RestClient(request.BaseUrl)
			{
				FollowRedirects = false
			};

			result.SentAt = DateTimeOffset.UtcNow;
			var response = await client.ExecuteTaskAsync(request);
			result.ReceivedAt = DateTimeOffset.UtcNow;
			result.Response = response;
			
			return result;
		}
	}
}