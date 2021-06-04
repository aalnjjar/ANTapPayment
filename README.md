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