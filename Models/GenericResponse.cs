using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ANTapPayment.Models
{
    public class GenericResponse<T, U>
    {
        public GenericResponse()
        {

        }
        public GenericResponse(string jsonResponse, T sucsessResponse, U failureResponse)
        {
            IsSuccess = true;
            JsonResponse = jsonResponse;
            SucsessResponse = sucsessResponse;
            FailureResponse = failureResponse;
        }

        public bool IsSuccess { get; set; }
        public string JsonResponse { get; set; }
        public T SucsessResponse { get; set; }
        public U FailureResponse { get; set; }
    }


    public partial class TapErrorResponse
    {
        [JsonProperty("errors")]
        public Error[] Errors { get; set; }
    }

    public partial class Error
    {
        [JsonProperty("code")]
        public long Code { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
