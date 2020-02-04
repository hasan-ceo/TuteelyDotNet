using Nyika.Domain.Abstract.AVL;
using Nyika.Domain.Concrete;
using Nyika.Domain.Concrete.AVL;
using Nyika.Domain.Entities.AVL;
using Nyika.Domain.Entities.Setup;
using Nyika.WebUI.Helper;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Controllers
{
    public class IPNController : Controller
    {
        PaypalIPN ipn = new PaypalIPN();
        private string receiver_email = "surzo4368@yahoo.com";
        private string mc_currency = "USD";

        private IPaypalIPNRepo db;
        string strResponse_copy;

        public IPNController(IPaypalIPNRepo DB)
        {
            this.db = DB;

        }

        [HttpPost]
        public HttpStatusCodeResult Receive()
        {


            //Store the IPN received from PayPal
            LogRequest(Request);


            //Fire and forget verification task
            Task.Run(() => VerifyTask(Request));


            //Reply back a 200 code
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private void VerifyTask(HttpRequestBase ipnRequest)
        {
            var verificationResponse = string.Empty;

            try
            {

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var verificationRequest = (HttpWebRequest)WebRequest.Create("https://ipnpb.paypal.com/cgi-bin/webscr");
                //var verificationRequest = (HttpWebRequest)WebRequest.Create("https://ipnpb.sandbox.paypal.com/cgi-bin/webscr");

                //Set values for the verification request
                verificationRequest.Method = "POST";
                verificationRequest.ContentType = "application/x-www-form-urlencoded";
                var param = Request.BinaryRead(ipnRequest.ContentLength);
                var strRequest = Encoding.ASCII.GetString(param);
                strResponse_copy = strRequest;  //Save a copy of the initial info sent by PayPal 

                //Add cmd=_notify-validate to the payload
                strRequest = "cmd=_notify-validate&" + strRequest;
                verificationRequest.ContentLength = strRequest.Length;

                //Attach payload to the verification request
                var streamOut = new StreamWriter(verificationRequest.GetRequestStream(), Encoding.ASCII);
                streamOut.Write(strRequest);
                streamOut.Close();

                //Send the request to PayPal and get the response
                var streamIn = new StreamReader(verificationRequest.GetResponse().GetResponseStream());
                verificationResponse = streamIn.ReadToEnd();
                streamIn.Close();


            }
            catch (Exception exception)
            {
                // Get stack trace for the exception with source file information
                var st = new StackTrace(exception, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                using (EFDbContext db = new EFDbContext())
                {
                    ErrorLog errorLog = new ErrorLog();
                    errorLog.Message = line.ToString() + "email error" + ", " + exception.ToString();
                    errorLog.ErrorDateTime = DateTime.Now;
                    db.ErrorLog.Add(errorLog);
                    db.SaveChanges();
                }
            }

            ProcessVerificationResponse(verificationResponse);
        }


        private void LogRequest(HttpRequestBase request)
        {
            if (request.Form["payment_status"] == "Completed")
            {
                ipn.PaymentType = request.Form["payment_type"];
                ipn.PaymentDate = request.Form["payment_date"];
                ipn.PaymentStatus = request.Form["payment_status"];
                ipn.PendingReason = request.Form["pending_reason"];
                ipn.PayerAddressStatus = request.Form["address_status"];
                ipn.PayerStatus = request.Form["payer_status"];
                ipn.PayerFirstName = request.Form["first_name"];
                ipn.PayerLastName = request.Form["last_name"];
                ipn.PayerEmail = request.Form["payer_email"];
                ipn.PayerID = request.Form["payer_id"];
                ipn.PayerCountry = request.Form["address_country"];
                ipn.PayerCountryCode = request.Form["address_country_code"];
                ipn.PayerZipCode = request.Form["address_zip"];
                ipn.PayerState = request.Form["address_state"];
                ipn.PayerCity = request.Form["address_city"];
                ipn.PayerStreet = request.Form["address_street"];
                ipn.Business = request.Form["business"];
                ipn.ReceiverEmail = request.Form["receiver_email"];
                ipn.ReceiverID = request.Form["receiver_id"];
                ipn.ItemName = request.Form["item_name"];
                ipn.ItemNumber = request.Form["item_number"];
                ipn.Quantity = request.Form["quantity"];
                ipn.Shipping = request.Form["mc_shipping"];
                ipn.Tax = request.Form["tax"];
                ipn.Currency = request.Form["mc_currency"];
                ipn.PaymentFee = request.Form["mc_fee"];
                ipn.PaymentGross = request.Form["mc_gross"];
                ipn.TXN_Type = request.Form["txn_type"];
                ipn.TXN_ID = request.Form["txn_id"];
                ipn.NotifyVersion = request.Form["notify_version"];
                ipn.Custom = request.Form["custom"];
                ipn.Invoice = request.Form["invoice"];
                ipn.VerifySign = request.Form["verify_sign"];
                ipn.QuantityCartItems = request.Form["num_cart_items"];
                db.SavePaypalIPN(ipn);
            }
        }

        private void ProcessVerificationResponse(string verificationResponse)
        {
            if (verificationResponse.Equals("VERIFIED"))
            {
                var these_argies = HttpUtility.ParseQueryString(strResponse_copy);

                if (these_argies["payment_status"] == "Completed")
                {
                    if (these_argies["receiver_email"] == receiver_email)
                    {
                        if (these_argies["mc_currency"] == mc_currency)
                        {
                            db.CSActive(these_argies["custom"], these_argies["txn_id"], these_argies["item_name"]);

                            StringBuilder body = new StringBuilder()
                                .AppendLine("<h2>Thank you for subscribe our service</h2>")
                                .AppendLine("<hr />")
                                .AppendLine("<p><strong>Date : </strong> " + these_argies["payment_date"])
                                .AppendLine("<p><strong>Item Name : </strong> " + these_argies["item_name"])
                                .AppendLine("</p><p><strong>Transaction No :</strong> " + these_argies["txn_id"])
                                .AppendLine("</p><p><strong>Full Name:</strong> " + these_argies["first_name"] + " " + these_argies["last_name"])
                                .AppendLine("</p><p><strong>Amount Received : </strong> " + these_argies["mc_gross"])
                                .AppendLine("<br />")
                                .AppendLine("<br />")
                                .AppendLine("<br />")
                                .AppendLine("<br />")
                                .AppendLine("<p>Yours sincerely,</p>")
                                .AppendLine("<p>Nyika</p>");

                            EmailSender em = new EmailSender();
                            em.Send(these_argies["custom"], "Nyika - Subscription Payment Receive", body.ToString());
                        }
                    }
                }
            }
        }
    }
}

// check that Payment_status=Completed
// check that Txn_id has not been previously processed
// check that Receiver_email is your Primary PayPal email
// check that Payment_amount/Payment_currency are correct
// process payment