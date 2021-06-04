using System;
using System.Collections.Generic;
using System.Text;

namespace ANTapPayment.Models
{
    public class RefundRequestModel
    {
        /// <summary>
        /// amount you want to refund could be paid amount or any less amount
        /// </summary>
        public double AmountToRefund { get; set; }

        /// <summary>
        /// currency in ISO format 
        /// </summary>
        public string CurrencyISO { get; set; }
        /// <summary>
        /// tap charge Id
        /// </summary>
        public string ChargeId { get; set; }
        /// <summary>
        /// transaction description will appear for customer
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// refund reasons 
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// payment merchent will be provided from tap for each country
        /// </summary>
        public string PaymentMerchent { get; set; }
        /// <summary>
        /// After refund completed, Tap will post the refund response to the this url
        /// base url will be appended from configration based on mode
        /// </summary>
        public string PostUrl { get; set; }
    }
}
