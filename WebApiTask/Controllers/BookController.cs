using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTask.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase {
		[HttpPost]
		[Route("New")]
		public async Task<string> New([FromBody] NewBookVM model) {

			
			//https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/


			try {
				AuthLoginResponse authLoginResponse = await serviceTranscription.AuthLoginDigitroAsync();
				AuthChallengeRequest authChallengeRequest = new AuthChallengeRequest(authLoginResponse.User, authLoginResponse.challenge, serviceTranscription.GetChallengeResponseHash(authLoginResponse.challenge, authLoginResponse.salt));
				AuthChallengeResponse authChallengeResponse = await serviceTranscription.AuthChallengeDigitroAsync(authChallengeRequest);

				TranscriptionRequest transcriptionRequest = new TranscriptionRequest(authLoginResponse.User, model.AudioDuration);
				TranscriptionResponse transcriptResponse = await serviceTranscription.SendTranscriptionDigitroAsync(transcriptionRequest, authChallengeResponse.token);

				return JsonConvert.SerializeObject(transcriptResponse);

			} catch (Exception e) {
				model.ErrorText = e.Message;
			}

			return null;
		}
	}

	public class NewBookVM {
        public int Id { get; set; }
    }

}
