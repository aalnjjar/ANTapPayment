using ANTapPayment.Helpers;
using ANTapPayment.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ANTapPayment.Payments
{
    public class PaymentRequest : IPaymentRequest
    {

        private readonly ApiConfiguration _apiConfiguration;
        private readonly HttpClientFactory _httpClientFactory;
        public PaymentRequest(ApiConfiguration configuration)
        {
            _apiConfiguration = configuration;
            _httpClientFactory = new HttpClientFactory(configuration.TapSecretKey);
        }


        public async Task<GenericResponse<ChargeReponse, TapErrorResponse>> Create(PaymentRequestModel requestModel)
        {
            try
            {
                var reuest = GenerateRequest(requestModel);
                var apiResponse = await _httpClientFactory.PostAsync<TapApiChargeReponse, TapErrorResponse>("charges", reuest, requestModel.Language);
                var chargeresponse = apiResponse.IsSuccess ? new ChargeReponse(apiResponse.SucsessResponse) : null;
                return new GenericResponse<ChargeReponse, TapErrorResponse>(apiResponse.JsonResponse, chargeresponse, apiResponse.FailureResponse);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<GenericResponse<ChargeReponse, TapErrorResponse>> GetCharge(string chargeId)
        {
            try
            {
                var apiResponse = await _httpClientFactory.GetAsync<TapApiChargeReponse, TapErrorResponse>($"charges/{chargeId}");
                var chargeresponse = apiResponse.IsSuccess ? new ChargeReponse(apiResponse.SucsessResponse) : null;
                return new GenericResponse<ChargeReponse, TapErrorResponse>(apiResponse.JsonResponse, chargeresponse, apiResponse.FailureResponse);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<GenericResponse<TapApiChargesListResponse, TapErrorResponse>> GetChargeList(DateTime fromDate, DateTime toDate, int limit = 25,
           string startingAfter = "", TapChargeStatus? status = null, List<string> sources = null, List<string> customers = null,
           List<string> chargesIds = null, List<string> PaymentMethods = null, ChargeDateType chargeDateType = ChargeDateType.Transaction_Date)
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
                        type = chargeDateType.IntValue()
                    },
                    status = status.HasValue ? status.Value.ToString() : "",
                    starting_after = startingAfter,
                    limit = limit,
                    sources = sources,
                    customers = customers,
                    charges = chargesIds,
                    payment_methods = PaymentMethods
                };

                var apiResponse = await _httpClientFactory.PostAsync<TapApiChargesListResponse, TapErrorResponse>($"charges/list", requestBody);
                return apiResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private dynamic GenerateRequest(PaymentRequestModel requestModel)
        {
            return new
            {
                amount = requestModel.AmountToPay,
                currency = requestModel.CurrencyISO,
                save_card = requestModel.SaveCardInformation,
                threeDSecure = true,
                description = requestModel.PaymentDescription,
                statement_descriptor = _apiConfiguration.Company,
                merchant = new
                {
                    id = requestModel.PaymentMerchent
                },
                reference = new
                {
                    transaction = requestModel.TransactionId,
                    order = requestModel.OrderId
                },
                receipt = new
                {
                    email = requestModel.SendEmail,
                    sms = requestModel.SendSms
                },
                customer = new
                {
                    id = requestModel.CustomerId,
                    first_name = requestModel.CustomerFirstName,
                    middle_name = requestModel.CustomerMiddleName,
                    last_name = requestModel.CustomerLastName,
                    email = requestModel.CustomerEmail,
                    phone = new
                    {
                        country_code = requestModel.CustomerPhoneCountryCode,
                        number = requestModel.CustomerPhoneNumber
                    }
                },
                source = new
                {
                    id = requestModel.PaymentSource
                },
                post = new
                {
                    url = _apiConfiguration.BaseUrl + _apiConfiguration.PostEndPoint
                },
                redirect = new
                {
                    url = _apiConfiguration.BaseUrl + _apiConfiguration.RedirectEndPoint
                }

            };
        }
    }
}
