using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;

namespace masya3.twilio
{
    public static class twiliosample
    {
        [FunctionName("twiliosample")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger twiliosample function processed a request.");

             //電話番号を取得する
            //https://jp.twilio.com/docs/voice/twiml#request-parameters
            string from = req.Form["From"];   
            
            var response = new VoiceResponse();
            response.Say(from + "から着信がありました。", language: "ja-jp", voice: "alice");
            return new ContentResult{Content = response.ToString(), ContentType = "application/xml"};

            //入力させたい場合
            /*
            var nigitsResponse = new VoiceResponse();
            nigitsResponse.Gather(new Gather(numDigits: 入力桁数, timeout:入力のタイムアウト時間, action:"番号入力後の呼び出しURL")
                                .Say("ボタン入力を促すメッセージ"), language: "ja-jp", voice: "alice"));
            nigitsResponse.Say("タイムアウト後のメッセージ", language: "ja-jp", voice: "alice");

            return new ContentResult{Content = nigitsResponse.ToString(), ContentType = "application/xml"};
            */
        }
    }
}
