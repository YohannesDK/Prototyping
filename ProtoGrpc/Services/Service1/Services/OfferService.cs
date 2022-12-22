using AutoMapper;
using Grpc.Core;
using Service1.Repositories;
using Protos.Service1;
using ProductOfferService = Protos.Service1.ProductOfferService;
using Service1.Entities;

namespace Service1.Services
{
    public class OfferService : ProductOfferService.ProductOfferServiceBase
    {
        private readonly IProductOfferService _productOfferService;
        private readonly IMapper _mapper;

        public OfferService(IProductOfferService productOfferService, IMapper mapper)
        {
            _productOfferService = productOfferService;
            _mapper = mapper;
        }

		public async override Task<OfferDetail> CreateOffer(CreateOfferDetailRequest request, ServerCallContext context)
		{
            await _productOfferService.AddOfferAsync(_mapper.Map<Offer>(request.Offer));
            return _mapper.Map<OfferDetail>(request.Offer);
		}

		public async override Task<DeleteOfferDetailResponse> DeleteOffer(DeleteOfferDetailRequest request, ServerCallContext context)
		{
            return new DeleteOfferDetailResponse { IsDelete = await _productOfferService.DeleteOfferAsync(request.ProductId) };
		}

		public async override Task<OfferDetail> GetOffer(GetOfferDetailRequest request, ServerCallContext context)
		{
            return _mapper.Map<OfferDetail>(await _productOfferService.GetOfferByIdAsync(request.ProductId));
		}

		public async override Task<Offers> GetOfferList(Empty request, ServerCallContext context)
		{
            var offersData = await _productOfferService.GetOfferListAsync();
            Offers response = new Offers();
            offersData.ForEach(x => response.Items.Add(_mapper.Map<OfferDetail>(x)));
			return response;
		}

		public async override Task<OfferDetail> UpdateOffer(UpdateOfferDetailRequest request, ServerCallContext context)
		{
            return _mapper.Map<OfferDetail>(await _productOfferService.UpdateOfferAsync(_mapper.Map<Offer>(request.Offer)));
		}

        public override async Task<PingReply> PingMessage(Empty request, ServerCallContext context)
        {
            return await Task.FromResult(new PingReply { Msg = "Pong" });
        }

        public override async Task<OfferDetail> CreateOfferFromMegaOffer(CreateOfferFromMegaRequest request, ServerCallContext context)
        {
            await _productOfferService.AddOfferAsync(_mapper.Map<Offer>(request.Offer));
            return _mapper.Map<OfferDetail>(request.Offer.MegaOfferDetail.OfferDetail);
        }

        public async override Task<PingReply> PongMessage(Empty request, ServerCallContext context)
        {
            return await Task.FromResult(new PingReply { Msg = "Pong" });
        }
    }
}
