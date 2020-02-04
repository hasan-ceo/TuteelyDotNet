using CaptchaMvc.HtmlHelpers;
using Nyika.Domain.Abstract.AVL;
using Nyika.Domain.Entities.AVL;
using Nyika.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IPageRepo db;
        private IFeedbackRepo fdb;
        private static readonly HttpClient client = new HttpClient();

        public HomeController(IPageRepo DB, IFeedbackRepo FDB)
        {
            this.db = DB;
            this.fdb = FDB;
        }


        private string GetDocumentContents(System.Web.HttpRequestBase Request)
        {
            string documentContents;
            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    documentContents = readStream.ReadToEnd();
                }
            }
            return documentContents;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Route("About")]
        public ActionResult About()
        {
            return View();
        }

        private List<string> Topic()
        {
            List<string> Topics = new List<string>();
            Topics.Add("General enquiry");
            Topics.Add("HR & Payroll");
            Topics.Add("Accounts");
            Topics.Add("Invoice");
            Topics.Add("Microfinance");
            Topics.Add("School Management");
            Topics.Add("E-Commerce");
            Topics.Add("Asset Register");
            Topics.Add("New Module Development");
            Topics.Add("Report Customization");
            return Topics;
        }

        // GET: BasicSetup/Feedbacks/Create
        [Route("Contact")]
        public ActionResult Contact()
        {
            ViewBag.TopicName = new SelectList(Topic());
            return View();
        }

        // POST: BasicSetup/Feedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Contact")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactSave([Bind(Include = "FeedbackID,TopicName,Detailsoferror,WebsiteLike,WebsiteNotLike,Suggestions,Comments,purposes,VisitFrequency,Email,UserType")] Feedback Feedback)
        {
            if (ModelState.IsValid)
            {
                if (this.IsCaptchaValid("Captcha is not valid"))
                {

                    Feedback.Detailsoferror = string.IsNullOrEmpty(Feedback.Detailsoferror) ? "" : Feedback.Detailsoferror;
                    Feedback.WebsiteLike = string.IsNullOrEmpty(Feedback.WebsiteLike) ? "" : Feedback.WebsiteLike;
                    Feedback.WebsiteNotLike = string.IsNullOrEmpty(Feedback.WebsiteNotLike) ? "" : Feedback.WebsiteNotLike;
                    Feedback.Suggestions = string.IsNullOrEmpty(Feedback.Suggestions) ? "" : Feedback.Suggestions;
                    Feedback.Comments = string.IsNullOrEmpty(Feedback.Comments) ? "" : Feedback.Comments;
                    Feedback.EntryDate = DateTime.Now;
                    fdb.SaveFeedback(Feedback);
                    return RedirectToAction("ContactConfirmation");
                }
                else {
                    ViewBag.ErrMessage = "Error: captcha is not valid.";
                    
                }
            }
            ViewBag.TopicName = new SelectList(Topic(), Feedback.TopicName);
            return View("Contact", Feedback);
        }

        [Route("ContactConfirmation")]
        public ActionResult ContactConfirmation()
        {
            return View();
        }

        // GET: AboutUs/Details/5
        [Route("en/{slug}")]
        public ActionResult Details(string slug)
        {

            Page page = db.Page.Where(p => p.Slug == slug).FirstOrDefault();
            if (page == null)
            {
                return RedirectToAction("Index");
            }
            return View(page);
        }

        [Route("Cancel")]
        public ActionResult Cancel()
        {
            return View();
        }

        [Route("Success")]
        public ActionResult Success()
        {

            // CUSTOMIZE THIS: This is the seller's Payment Data Transfer authorization token.
            // Replace this with the PDT token in "Website Payment Preferences" under your account.
            string authToken = "xWqXx_Xq-sY771JzBtmzNP-BbWvBiLDN-lKK8t8FXbmks5Ui9XnoIMYuAhq"; //original
            //string authToken = "Quw2UVVk05VMJ4icy8RbU34KsX171qMh1DwOuQnHE8d-UrhY8A_Brwh-hou"; //developer
            string txToken = Request.QueryString["tx"];
            string query = "cmd=_notify-synch&tx=" + txToken + "&at=" + authToken;

            //Post back to either sandbox or live
            //string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            string strLive = "https://www.paypal.com/cgi-bin/webscr";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strLive);

            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //Send the request to PayPal and get the response
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(query);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();

            Dictionary<string, string> results = new Dictionary<string, string>();

            //Subscription sb = new Subscription();

            if (strResponse != "")
            {
                StringReader reader = new StringReader(strResponse);
                string line = reader.ReadLine();

                if (line == "SUCCESS")
                {

                    while ((line = reader.ReadLine()) != null)
                    {
                        results.Add(line.Split('=')[0], line.Split('=')[1]);

                    }

                 
                    if (results["payment_status"] == "Completed")
                    {

                        //sb.UserEmail = results["custom"];
                        //sb.PayerEmail = results["custom"];
                        //sb.PayerName = results["first_name"] + " " + results["last_name"];
                        //sb.SubscriptionDate = DateTime.Now.Date;
                        //sb.SubscriptionType = "";
                        //sb.PaypalTransactionID = results["txn_id"];
                        //sb.ExpairDate = DateTime.Now.Date;
                        //sb.Amount = Convert.ToDouble(results["mc_gross"]);

                        //sdb.SaveSubscription(sb);
                        SubscriptionVM subscriptionVM = new SubscriptionVM();
                        subscriptionVM.PayerName = results["first_name"] + " " + results["last_name"];
                        subscriptionVM.SubscriptionType = results["item_name"];
                        subscriptionVM.Amount = results["mc_gross"];
                        return View(subscriptionVM);
                    }
                    else
                    {
                        ViewBag.msg = "Paypal transaction not complete";
                        return View("PaymentConfirmation");
                        //
                    }

                }
                else if (line == "FAIL")
                {
                    // Log for manual investigation
                    ViewBag.msg = "Unable to retrive transaction detail";
                    return View("PaymentConfirmation");
                }
            }
            else
            {
                //unknown error
                ViewBag.msg = "ERROR";
                return View("PaymentConfirmation");
            }

            ViewBag.msg = "Paypal transaction not complete";
            return View("PaymentConfirmation");
        }

        [Route("UserExpire")]
        public ActionResult UserExpire([Bind(Include = "id,email")] UserExpireVM userExpireVM)
        {

            return View(userExpireVM);
        }

        [Route("Checkout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public EmptyResult Checkout([Bind(Include = "id,email,bit")] UserExpireVM userExpireVM)
        {
            string PostUrl = "";
            string os0 = "";
            string hosted_button_id = "";
            PostUrl = "https://www.paypal.com/cgi-bin/webscr";
            //PostUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr"; // sandbox

            //hosted_button_id = "6XPGFK3VUXELL"; 
            // hosted_button_id = "4X9S4M4VSSSM6"; // sandbox

            if (userExpireVM.bit == 1)
            {
                os0 = "Mwezi";
                hosted_button_id = "4X9S4M4VSSSM6"; // sandbox
            }
            if (userExpireVM.bit == 2)
            {
                os0 = "Mwaka";
                hosted_button_id = "TRD8PVVKC7SYN"; // sandbox
            }
            if (userExpireVM.bit == 3)
            {
                os0 = "mawili";
                hosted_button_id = "5Q6F4WC97MXNG"; // sandbox
            }
            if (userExpireVM.bit == 4)
            {
                os0 = "Milele";
                hosted_button_id = "KFYUULRPTPCFG"; // sandbox
            }

            RemotePost myremotepost = new RemotePost();
            myremotepost.Url = PostUrl;
            myremotepost.Add("cmd", "_s-xclick");
            myremotepost.Add("hosted_button_id", hosted_button_id);
            myremotepost.Add("custom", userExpireVM.email);
            myremotepost.Post();

            return new EmptyResult();
        }

        public EmptyResult PaypalCall(int SubId, string email)
        {
            string PostUrl = "";
            string hosted_button_id = "";
            PostUrl = "https://www.paypal.com/cgi-bin/webscr";
            //PostUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr"; // sandbox

            //hosted_button_id = "6XPGFK3VUXELL"; 
            // hosted_button_id = "4X9S4M4VSSSM6"; // sandbox

            if (SubId == 1)
            {
                hosted_button_id = "6XPGFK3VUXELL"; // sandbox
            }
            if (SubId == 2)
            {
                hosted_button_id = "758LF7YVT9QNW"; // sandbox
            }
            
            RemotePost myremotepost = new RemotePost();
            myremotepost.Url = PostUrl;
            myremotepost.Add("cmd", "_s-xclick");
            myremotepost.Add("hosted_button_id", hosted_button_id);
            myremotepost.Add("custom", email);
            myremotepost.Post();

            return new EmptyResult();
        }

    }

    public class RemotePost
    {
        private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();
        public string Url = "";
        public string Method = "post";
        public string FormName = "paypalform";
        public void Add(string name, string value)
        {
            Inputs.Add(name, value);
        }
        public void Post()
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write("<html><head><title>Nyika - HR + Payroll + Accounts</title><link rel=\"icon\" href=\"~/Images/icon.png?" + DateTime.Now.ToFileTime()+ "\" sizes=\"any\" type=\"image/svg+xml\">");
            System.Web.HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
            System.Web.HttpContext.Current.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
            for (int i = 0; i < Inputs.Keys.Count; i++)
            {
                System.Web.HttpContext.Current.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
            }
            System.Web.HttpContext.Current.Response.Write("</form>");
            System.Web.HttpContext.Current.Response.Write("<img src ='https://nyika.co/Images/logo-black.png' alt = 'Nyika logo'>");
            System.Web.HttpContext.Current.Response.Write("<p style='text-align: center'> <h2>Now you are connectin to the PayPal</h2></p>");
            System.Web.HttpContext.Current.Response.Write("</body></html>");
            System.Web.HttpContext.Current.Response.End();
        }
    }
}