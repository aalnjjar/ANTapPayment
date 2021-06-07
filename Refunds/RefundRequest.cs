using ANTapPayment.Helpers;
using ANTapPayment.Models;
using ANTapPayment.Models.Refund;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ANTapPayment.Refunds
{
    public class RefundRequest : IRefundRequest
    {

        private readonly ApiConfiguration _apiConfiguration;
        private readonly HttpClientFactory _httpClientFactory;
        public RefundRequest(ApiConfiguration configuration)
        {
            _apiConfiguration = configuration;
            _httpClientFactory = new HttpClientFactory(configuration.TapSecretKey);
        }


        public async Task<GenericResponse<RefundResponse, TapErrorResponse>> Create(RefundRequestModel requestModel)
        {
            try
            {
                var reuest = GenerateRefundRequest(requestModel);
                var apiResponse = await _httpClientFactory.PostAsync<TapApiRefundResponse, TapErrorResponse>("refunds", reuest);
                var chargeresponse = apiResponse.IsSuccess ? new RefundResponse(apiResponse.SucsessResponse) : null;
                return new GenericResponse<RefundResponse, TapErrorResponse>(apiResponse.JsonResponse, chargeresponse, apiResponse.FailureResponse);

            }
            catch
            {

                throw new Exception("Error creating payment request");
            }
        }


        public async Task<GenericResponse<RefundResponse, TapErrorResponse>> GetRefund(string refundId)
        {
            try
            {
                var apiResponse = await _httpClientFactory.GetAsync<TapApiRefundResponse, TapErrorResponse>($"refunds/{refundId}");
                var chargeresponse = apiResponse.IsSuccess ? new RefundResponse(apiResponse.SucsessResponse) : null;
                return new GenericResponse<RefundResponse, TapErrorResponse>(apiResponse.JsonResponse, chargeresponse, apiResponse.FailureResponse);
            }
            catch
            {
                throw new Exception("Error geting refund request");
            }
        }


        public async Task<GenericResponse<TapApiRefundsListResponse, TapErrorResponse>> GetChargeList(DateTime fromDate, DateTime toDate, int limit = 25,
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

                var apiResponse = await _httpClientFactory.PostAsync<TapApiRefundsListResponse, TapErrorResponse>($"refunds/list", requestBody);
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
