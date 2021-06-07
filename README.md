# ANTapPayment
 this is a warpaer for TAP payment api , package will handel most of the hard work 


# Installing / Getting Started

- Install the package from nuguet 

### configure app in startup.cs

- defining your api configration
```
  ApiConfiguration apiConfiguration = new ApiConfiguration()
            {
                BaseUrlLive = "", // your api live base url
                BaseUrlStaging = "", // your staging base url 
                Company = "ahmed alnjjar", // your company name
                Mode = Mode.Staging, // server mode will be used for redirection
                PostEndPoint = "/api/endpoint", // post end point charge request will be redirected to this end point
                RedirectEndPoint = "/api/end/point", // redirect url 
                TapSecretKeyLive = "", // tap secret key for live environment 
                TapSecretKeyStaging = "" // tap secret key for staging 
            };

```
- if you want to use the charge apis add the following to your startup.cs
```
 services.AddTransient<IPaymentRequest, PaymentRequest>(func => new PaymentRequest(apiConfiguration));
```
- if you want to use the refund apis add the following to your startup 
```
 services.AddTransient<IRefundRequest, RefundRequest>(func => new RefundRequest(apiConfiguration));

```

### creating your first charge request
 

```
      private readonly IPaymentRequest _paymentRequest; 

      public YourRepository(IPaymentRequest paymentRequest) 
        {
            _paymentRequest = paymentRequest;
        }

     public async Task<dynamic> YourFunction(string token)
        {
        
        /// <summary>
        /// payment source  
        /// For credit card ==> send generated token from front end
        /// For KNET ==> src_kw.knet 
        /// For Mada ==> src_sa.mada
        /// For Benifit ==> src_bh.benefit
        /// For Fawray ==> src_eg.fawry
        /// For Oman net ==> src_om.omannet
        /// </summary>


         var response = await _paymentRequest.Create(new ANTapPayment.Models.PaymentRequestModel
            {
                AmountToPay = 100.000,
                CurrencyISO = "KWD",
                CustomerEmail = clientEmail,
                CustomerFirstName = clientFName,
                CustomerLastName = clientLName,
                CustomerPhoneCountryCode = "965",
                CustomerPhoneNumber = clientPhone,
                OrderId = Guid.NewGuid().ToString(),
                PaymentMerchent = "",
                PaymentDescription = "test payment",
                PaymentSource = token, 
                TransactionId = Guid.NewGuid().ToString(),
                Language="AR"
            });

            return response;

        }

```

#### response will be 
 
```
        public bool IsSuccess { get; set; }
        public string JsonResponse { get; set; }
        public ChargeReponse SucsessResponse { get; set; } ==> 
        public TapErrorResponse FailureResponse { get; set; }

 ```