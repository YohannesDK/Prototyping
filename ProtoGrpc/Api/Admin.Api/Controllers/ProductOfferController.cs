using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Admin.Api.Entities;
using Protos.Service1;

namespace Admin.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductOfferController : Controller
    {
        private readonly GrpcChannel _channel;
        private readonly ProductOfferService.ProductOfferServiceClient _client;
        private readonly IConfiguration _configuration;

        public ProductOfferController(IConfiguration confguration)
        {
            _configuration = confguration;
            _channel = GrpcChannel.ForAddress(_configuration.GetValue<string>("GrpcSettings:Service1"));
            _client = new ProductOfferService.ProductOfferServiceClient(_channel);
        }

        [HttpGet("getofferlist")]
        public async Task<Offers> GetOfferListAsync()
        {
            try {
                var response = await _client.GetOfferListAsync(new Empty { });
                return response; 
            }
            catch (Exception) { throw;}
        }

        [HttpGet("getofferbyid")]
        public async Task<OfferDetail> GetOfferByIdAsync(int id)
        {
            try
            {
                return await _client.GetOfferAsync(new GetOfferDetailRequest { ProductId = id });
            }
            catch (Exception) { throw; }
        }

        [HttpPost("addoffer")]
        public async Task<OfferDetail> AddOfferAsync(Offer offer)
        {
            try
            {
                return await _client.CreateOfferAsync(new CreateOfferDetailRequest
                {
                    Offer = new OfferDetail { Id = offer.Id, ProductName = offer.ProductName, OfferDescription = offer.OfferDescription },
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("addMegaOffer")]
        public async Task<OfferDetail> AddMegaOfferAsync(MegaExtendedOfferDetail megaExtendedOfferDetail)
        {
            try
            {
                return await _client.CreateOfferFromMegaOfferAsync(new CreateOfferFromMegaRequest() { Offer = megaExtendedOfferDetail });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("updateoffer")]
        public async Task<OfferDetail> UpdateOfferAsync(Offer offer)
        {
            try
            {
                return await _client.UpdateOfferAsync(new UpdateOfferDetailRequest()
                {
                    Offer = new OfferDetail { Id = offer.Id, ProductName = offer.ProductName, OfferDescription = offer.OfferDescription }

                });
            }
            catch
            {

            }
            return null;
        }

        [HttpDelete("deleteoffer")]
        public async Task<DeleteOfferDetailResponse> DeleteOfferAsync(int Id)
        {
            try
            {
                return await _client.DeleteOfferAsync(new DeleteOfferDetailRequest()
                {
                    ProductId = Id
                });
            }
            catch
            {

            }
            return null;
        }

        [HttpGet("ping")]
        public async Task<PingReply> Ping()
        {
            try
            {
                return await _client.PingMessageAsync(new Empty { });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("pong")]
        public async Task<PingReply> pong()
        {
            try
            {
                return await _client.PongMessageAsync(new Empty { });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
