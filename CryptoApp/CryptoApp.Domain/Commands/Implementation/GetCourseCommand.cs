using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CryptoApp.DataAccess.Common.Db;
using CryptoApp.Domain.Commands.Interface;
using CryptoApp.Models.Models;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CryptoApp.Domain.Commands.Implementation
{
    public class GetCourseCommand : ITelegramCommand
    {
        public string Name => @"/getCourse";

        public string Description => "Получить курс валют";

        private string _apiUrl =>
            "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest?start=1&limit=14&convert=USD";

        private string _apiKey => "3471bf9f-7973-4708-8e17-e2535cc460a6";
        
        public IMainDbConnection Connection { get; set; }

        public async Task<Message> Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _apiKey);

            var response = await httpClient.GetAsync(_apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var courseResponse = JsonConvert.DeserializeObject<CourseResponse>(result) 
                                     ?? throw new Exception();

                var strBuilder = new StringBuilder();
                strBuilder.Append(@"\u07503300265 0334 [USD]\n");
                strBuilder.Append($"[{DateTime.Now.ToString(CultureInfo.InvariantCulture)}]\n");
                strBuilder.Append("\n");
                strBuilder.Append($"BTC   {Math.Round(courseResponse.data[0].quote.USD.price, 3)}\n");
                strBuilder.Append($"ETH   {Math.Round(courseResponse.data[1].quote.USD.price, 3)}\n");
                strBuilder.Append($"DOGE  {Math.Round(courseResponse.data[5].quote.USD.price, 3)}\n");
                strBuilder.Append($"ADA   {Math.Round(courseResponse.data[4].quote.USD.price, 3)}\n");
                strBuilder.Append($"LINK  {Math.Round(courseResponse.data[13].quote.USD.price, 3)}\n");
                
                return await client.SendTextMessageAsync(chatId,
                    strBuilder.ToString(), 
                    ParseMode.Markdown);
            }

            return await client.SendTextMessageAsync(chatId,
                "Произошла ошибка при получении курса.", 
                ParseMode.Markdown);
        }

        public bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
            {
                return false;
            }

            return message.Text != null && message.Text.Contains(Name);
        }
    }
}