using SearchService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SearchService.RequestHelper
{
    public class AuctionServiceHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;


        public AuctionServiceHttpClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _config = configuration;
        }
        public async Task<List<ItemSearch>> GetAuctionForSearchDB()
        {
            var auctionUrl = _config["AuctionServiceUrl"];
            var endpoint = auctionUrl + "/api/auctions";
            var items =  await _httpClient.GetFromJsonAsync<List<ItemSearch>>(endpoint);
            return items?? new List<ItemSearch>();
        }
    }
}
