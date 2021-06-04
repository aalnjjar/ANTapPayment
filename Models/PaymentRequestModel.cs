using System;
using System.Collections.Generic;
using System.Text;

namespace ANTapPayment.Models
{
    /// <summary>
    /// use this model to gather payment request information 
    /// </summary>
    public class PaymentRequestModel
    {
        /// <summary>
        /// amount that will be paid 
        /// </summary>
        public double AmountToPay { get; set; }
        /// <summary>
        /// payment merchent will be provided from tap for each country
        /// </summary>
        public string PaymentMerchent { get; set; }
        /// <summary>
        /// currency in ISO format 
        /// </summary>
        public string CurrencyISO { get; set; }
        /// <summary>
        /// description for the payment will be added for payment notes
        /// </summary>
        public string PaymentDescription { get; set; }
        /// <summary>
        /// Payment transaction id could be same as order id or unique id
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// your system order id 
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// use this if you want tap to save card information optional
        /// </summary>
        public bool SaveCardInformation { get; set; } = false;

        /// <summary>
        /// use this if you want to send email for the customer after payment optional
        /// </summary>
        public bool SendEmail { get; set; } = false;
        /// <summary>
        /// use this if you want tap to send sms to customer after payment optional
        /// </summary>
        public bool SendSms { get; set; } = false;

        /// <summary>
        /// customer id use it if you used tap save card information tap will provide you with customer id save it in yor database 
        /// and you can use it later for fast check out 
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// customer first name
        /// </summary>
        public string CustomerFirstName { get; set; }
        /// <summary>
        /// customer middle name optional
        /// </summary>
        public string CustomerMiddleName { get; set; } = "";
        /// <summary>
        /// customer last name
        /// </summary>
        public string CustomerLastName { get; set; }
        /// <summary>
        /// customer email
        /// </summary>
        public string CustomerEmail { get; set; }
        /// <summary>
        /// customer country code which should be start with + ex +965
        /// </summary>
        public string CustomerPhoneCountryCode { get; set; }
        /// <summary>
        /// customer phone number
        /// </summary>
        public string CustomerPhoneNumber { get; set; }
        /// <summary>
        /// payment source  
        /// credit card ==> send generated token 
        /// KNET ==> src_kw.knet 
        /// Mada ==> src_sa.mada
        /// Benifit ==> src_bh.benefit
        /// Fawray ==> src_eg.fawry
        /// Oman net ==> src_om.omannet
        /// </summary>
        public string PaymentSource { get; set; }


    }
}
