using ANTapPayment.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ANTapPayment.Models.Refund
{
    public partial class RefundResponse
    {
        public RefundResponse(TapApiRefundResponse response)
        {
            Status = response.Status.StringToEnum<TapChargeStatus>();
            StatusString = Status.ToString();
            Message = response.Response != null ? response.Response.Message : "";
            Code = response.Response != null ? response.Response.Code.ToString() : "";
            Response = response;
            RefundId = response.Id;
        }

        public TapChargeStatus Status { get; set; }
        public string StatusString { get; set; }
        public string ChargeId { get; set; }
        public string RefundId { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public TapApiRefundResponse Response { get; set; }
    }


    public partial class TapApiRefundsListResponse
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

        [JsonProperty("refunds")]
        public List<TapApiRefundResponse> Refunds { get; set; }
    }


    public partial class TapApiRefundResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("api_version")]
        public string ApiVersion { get; set; }

        [JsonProperty("live_mode")]
        public bool LiveMode { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("charge_id")]
        public string ChargeId { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("reference")]
        public Reference Reference { get; set; }

        [JsonProperty("response")]
        public Response Response { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }

    public partial class Metadata
    {
        [JsonProperty("udf1")]
        public string Udf1 { get; set; }

        [JsonProperty("udf2")]
        public string Udf2 { get; set; }
    }

    public partial class Reference
    {
        [JsonProperty("merchant")]
        public string Merchant { get; set; }  
        
        [JsonProperty("gateway")]
        public string Gateway { get; set; } 
        
        [JsonProperty("payment")]
        public string Payment { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}


