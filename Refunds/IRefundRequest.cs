using ANTapPayment.Models;
using ANTapPayment.Models.Refund;
using System;
using System.Collections.Generic;
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
}
