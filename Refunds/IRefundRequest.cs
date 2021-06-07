using ANTapPayment.Models;
using ANTapPayment.Models.Refund;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ANTapPayment.Refunds
{
    public interface IRefundRequest
    {
        public Task<GenericResponse<RefundResponse, TapErrorResponse>> Create(RefundRequestModel requestModel);
        public Task<GenericResponse<RefundResponse, TapErrorResponse>> GetRefund(string refundId);
        public Task<GenericResponse<TapApiRefundsListResponse, TapErrorResponse>> GetChargeList(DateTime fromDate, DateTime toDate, int limit = 25,
       string startingAfter = "", List<string> chargesIds = null, List<string> refundsIds = null);
    }
}
