using ANTapPayment.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ANTapPayment.Models
{

    public partial class ChargeReponse
    {
        public ChargeReponse()
        {

        }

        public ChargeReponse(TapApiChargeReponse response)
        {
            Status = response.Status.StringToEnum<TapChargeStatus>();
            StatusString = Status.ToString();
            ThreeDSecure = response.ThreeDSecure;
            RedirectUrl = response.Transaction != null && response.Transaction.Url != null ? response.Transaction.Url.ToString() : "";
            Message = response.Response != null ? response.Response.Message : "";
            Code = response.Response != null ? response.Response.Code.ToString() : "";
            Response = response;
            CustomerId = response.Customer != null ? response.Customer.Id : "";
            ChargeId = response.Id;
        }

        public TapChargeStatus Status { get; set; }
        public string StatusString { get; set; }
        public bool ThreeDSecure { get; set; }
        public string RedirectUrl { get; set; }
        public string ChargeId { get; set; }
        public string Message { get; set; }
        public string CustomerId { get; set; }
        public string Code { get; set; }
        public TapApiChargeReponse Response { get; set; }
    }

    public partial class TapApiChargesListResponse
    {

        [JsonProperty("object_type")]
        public string ObjectType { get; set; }

        [JsonProperty("live_mode")]
        public bool LiveMode { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("has_more")]
        public int HasMore { get; set; }

        [JsonProperty("api_version")]
        public string ApiVersion { get; set; }

        [JsonProperty("charges")]
        public List<TapApiChargeReponse> Charges { get; set; }
    }


    public partial class TapApiChargeReponse
    {
        public bool Success { get; set; } = false;

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("live_mode")]
        public bool LiveMode { get; set; }

        [JsonProperty("api_version")]
        public string ApiVersion { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("threeDSecure")]
        public bool ThreeDSecure { get; set; }

        [JsonProperty("save_card")]
        public bool SaveCard { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("statement_descriptor")]
        public string StatementDescriptor { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("transaction")]
        public Transaction Transaction { get; set; }

        [JsonProperty("reference")]
        public Reference Reference { get; set; }

        [JsonProperty("response")]
        public Response Response { get; set; }

        [JsonProperty("receipt")]
        public Receipt Receipt { get; set; }

        [JsonProperty("customer")]
        public Customer Customer { get; set; } = new Customer();

        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("post")]
        public Post Post { get; set; }

        [JsonProperty("redirect")]
        public Post Redirect { get; set; }
    }

    public partial class Customer
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; } = "";

        [JsonProperty("middle_name")]
        public string MiddleName { get; set; } = "";

        [JsonProperty("last_name")]
        public string LastName { get; set; } = "";

        [JsonProperty("email")]
        public string Email { get; set; } = "";

        [JsonProperty("phone")]
        public Phone Phone { get; set; } = new Phone();
        [JsonProperty("id")]
        public string Id { get; set; } = "";
    }

    public partial class Phone
    {
        [JsonProperty("country_code")]
        public string CountryCode { get; set; } = "";

        [JsonProperty("number")]
        public string Number { get; set; } = "";
    }

    public partial class Metadata
    {
        [JsonProperty("udf1")]
        public string Udf1 { get; set; }
    }

    public partial class Post
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class Receipt
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public bool Email { get; set; }

        [JsonProperty("sms")]
        public bool Sms { get; set; }
    }

    public partial class Reference
    {
        [JsonProperty("track")]
        public string Track { get; set; }

        [JsonProperty("payment")]
        public string Payment { get; set; }

        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        [JsonProperty("acquirer")]
        public string Acquirer { get; set; }

        [JsonProperty("transaction")]
        public string Transaction { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public partial class Source
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class Transaction
    {
        [JsonProperty("authorization_id")]
        public string AuthorizationId { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public enum TapChargeStatus
    {
        INSERTED = 0,
        INITIATED = 1,
        ABANDONED = 2,
        CANCELLED = 3,
        FAILED = 4,
        DECLINED = 5,
        RESTRICTED = 6,
        CAPTURED = 7,
        VOID = 8,
        TIMEDOUT = 9,
        UNKNOWN = 10,
        Approved = 11,
        Pending = 12,
        Authorized = 13,
        FAILED_SUCCESS = 14,
        FailedValidated = 15,
        InvalidResponse = 16,
        InsufficientFunds = 17
    }

    public enum ChargeDateType
    {
        Transaction_Date = 1,
        Post_Date = 2,
        Balance_Available_Date = 3,
        Payout_Based_on_period_Type = 4
    }
}
