using ANTapPayment.Helpers;
using ANTapPayment.Models;
using ANTapPayment.Models.Refund;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ANTapPayment.Refunds
{

    public interface IRefundRequest
    {
        public Task<RefundResponse> Create(RefundRequestModel requestModel);
        public Task<RefundResponse> GetRefund(string refundId);
        public Task<TapApiRefundsListResponse> GetChargeList(DateTime fromDate, DateTime toDate, int limit = 25,
       string startingAfter = "", List<string> chargesIds = null, List<string> refundsIds = null);
    }
    public class RefundRequest
    {

        private readonly ApiConfiguration _apiConfiguration;
        private readonly HttpClientFactory _httpClientFactory;
        public RefundRequest(ApiConfiguration configuration)
        {
            _apiConfiguration = configuration;
            _httpClientFactory = new HttpClientFactory(configuration.TapSecretKey);
        }


        public async Task<RefundResponse> Create(RefundRequestModel requestModel)
        {
            try
            {
                var reuest = GenerateRefundRequest(requestModel);
                var apiResponse = await _httpClientFactory.PostAsync<TapApiRefundResponse>("refunds", reuest);
                var refundResponse = new RefundResponse(apiResponse);
                return refundResponse;
            }
            catch
            {

                throw new Exception("Error creating payment request");
            }
        }


        public async Task<RefundResponse> GetRefund(string refundId)
        {
            try
            {
                var apiResponse = await _httpClientFactory.GetAsync<TapApiRefundResponse>($"refunds/{refundId}");
                var refundResponse = new RefundResponse(apiResponse);
                return refundResponse;
            }
            catch
            {
                throw new Exception("Error geting refund request");
            }
        }


        public async Task<TapApiRefundsListResponse> GetChargeList(DateTime fromDate, DateTime toDate, int limit = 25,
        string startingAfter = "", List<string> chargesIds = null, List<string> refundsIds = null)
        {
            try
            {
                var requestBody = new
                {
                    period = new
                    {
                        date = new
                        {
                            from = fromDate.ToTickFormat(),
                            to = toDate.ToTickFormat()
                        },
                    },
                    starting_after = startingAfter,
                    limit = limit,
                    charges = chargesIds,
                    refunds = refundsIds
                };

                var apiResponse = await _httpClientFactory.PostAsync<TapApiRefundsListResponse>($"refunds/list", requestBody);
                return apiResponse;
            }
            catch
            {
                throw new Exception("Error geting payment request");
            }
        }



        private dynamic GenerateRefundRequest(RefundRequestModel requestModel)
        {
            return new
            {
                charge_id = requestModel.ChargeId,
                amount = requestModel.AmountToRefund,
                currency = requestModel.CurrencyISO,
                description = requestModel.Description,
                reason = requestModel.Reason,
                reference = new
                {
                    merchant = requestModel.PaymentMerchent
                },
                post = new
                {
                    url = _apiConfiguration.BaseUrl + requestModel.PostUrl
                }
            };


        }
    }
}
