using ANTapPayment.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ANTapPayment.Payments
{
    public interface IPaymentRequest
    {
        public Task<GenericResponse<ChargeReponse, TapErrorResponse>> Create(PaymentRequestModel requestModel);
        public Task<GenericResponse<ChargeReponse, TapErrorResponse>> GetCharge(string ChargeId);
        public Task<GenericResponse<TapApiChargesListResponse, TapErrorResponse>> GetChargeList(DateTime fromDate, DateTime toDate, int limit = 25,
           string startingAfter = "", TapChargeStatus? status = null, List<string> sources = null, List<string> customers = null,
           List<string> chargesIds = null, List<string> PaymentMethods = null, ChargeDateType chargeDateType = ChargeDateType.Transaction_Date);

    }
}
