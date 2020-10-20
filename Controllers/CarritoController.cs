using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using Proyecto1.Models;

using PayPal.Api;

using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;

namespace Proyecto1.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carritoint id,string img,decimal precio,int cantidad,int tipo
        Carrito Alcarrito = new Carrito();
        Pedidos newpedido = new Pedidos();
        public ActionResult Index()
        {            
            Carrito carro = new Carrito();            
            carro.idproducto = int.Parse(Request.Form["idproducto"].ToString());
            carro.nombre = Request.Form["nombre"].ToString();           
            carro.precio = decimal.Parse(Request.Form["precio"].ToString().Replace('.', ','));
            carro.descripcion = Request.Form["descripcion"].ToString();
            carro.img = Request.Form["img"].ToString();
            carro.tipoPedido = int.Parse(Request.Form["tipo"].ToString()); 
            return View(carro);
        }
        [HttpPost]
        public ActionResult AgregarCarrito()
        {
            var dbo = (USER)Session["User"];
            if (Session["carrito"]==null)
            {
                List<Carrito> carrito = new List<Carrito>();
                Alcarrito.idproducto = int.Parse(Request.Form["idproducto"].ToString());
                Alcarrito.nombre = Request.Form["nombre"].ToString();
                Alcarrito.precio = decimal.Parse(Request.Form["precio"].ToString().Replace('.', ','));
                Alcarrito.descripcion = Request.Form["descripcion"].ToString();
                Alcarrito.img =null;
                Alcarrito.cantidad = int.Parse(Request.Form["cantidad"].ToString());
                Alcarrito.tipoPedido = int.Parse(Request.Form["tipo"].ToString());
                Alcarrito.idRol = dbo.idRol;
                carrito.Add(Alcarrito);
                Session["carrito"] = carrito;
            }
            else
            {
                List<Carrito> carrito = (List<Carrito>)Session["carrito"];
                Alcarrito.idproducto = int.Parse(Request.Form["idproducto"].ToString());
                Alcarrito.nombre = Request.Form["nombre"].ToString();
                Alcarrito.precio = decimal.Parse(Request.Form["precio"].ToString().Replace('.',','));
                Alcarrito.descripcion = Request.Form["descripcion"].ToString();
                Alcarrito.img = null;
                Alcarrito.cantidad = int.Parse(Request.Form["cantidad"].ToString());
                Alcarrito.tipoPedido = int.Parse(Request.Form["tipo"].ToString());
                Alcarrito.idRol = dbo.idRol;
                carrito.Add(Alcarrito);
                Session["carrito"] = carrito;
             
            }
            return RedirectToAction("Catalogo","Productos");
        }       
        public ActionResult Dettallebol()
        {
            //obtenemos ubicacion de memoria
            MemoryStream memo = new MemoryStream();
            PdfWriter pp = new PdfWriter(memo);
            PdfDocument doc = new PdfDocument(pp);
            //el tipo de hoja
            Document documento = new Document(doc, PageSize.LETTER);

            documento.SetMargins(75, 35, 70, 35);

            // para un tipo de letra personalizado
            string font = Server.MapPath("/fonts/Megan tatatatat ruta del tipo de letra");
            PdfFont fond = PdfFontFactory.CreateFont(FontConstants.HELVETICA);
            //creamos stilos personalizados
            Style estilo = new Style().SetFontSize(24).SetFont(fond).SetFontColor(ColorConstants.BLACK)
                                       .SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.CENTER);

            documento.Add(new Paragraph(" FERANVI - FOOT").AddStyle(estilo));
            /*documento.Add(new Paragraph(" Hellow Wordl")
                .SetFontColor(ColorConstants.BLUE)
                .SetFont(fond).SetFontSize(24));*/

            //damos estilo a la celda

            Style stylecell = new Style()
            .SetBackgroundColor(ColorConstants.BLACK)
            .SetFontColor(ColorConstants.WHITE)
            .SetTextAlignment(TextAlignment.CENTER);

            //retornamos ventas
            List<Carrito> carrito = (List<Carrito>)Session["carrito"];

            //para generar una tabla

            Table _tabla = new Table(7).UseAllAvailableWidth();
            Cell _cell = new Cell(2, 1).Add(new Paragraph("ID"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell(1, 6).Add(new Paragraph("DETALLE BOLETA"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("Producto"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("Precio"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("Descripcion"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("Cant"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("TipoPedido"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("Total"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));

            decimal total = 0;
            foreach (var item in carrito)
            {
                _cell = new Cell().Add(new Paragraph(item.idproducto.ToString()));
                _tabla.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.nombre));
                _tabla.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.precio.ToString()));
                _tabla.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.descripcion));
                _tabla.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.cantidad.ToString()));
                _tabla.AddCell(_cell);           

                if (item.tipoPedido == 1)
                {
                    _cell = new Cell().Add(new Paragraph("NORMAL"));
                    _tabla.AddCell(_cell.SetBackgroundColor(ColorConstants.BLUE).SetFontColor(ColorConstants.WHITE).SetTextAlignment(TextAlignment.CENTER));
                }
                else
                {
                    _cell = new Cell().Add(new Paragraph("PROMOCION"));
                    _tabla.AddCell(_cell.SetBackgroundColor(ColorConstants.RED).SetFontColor(ColorConstants.WHITE).SetTextAlignment(TextAlignment.CENTER));
                }
                total += (item.cantidad * item.precio);
                _cell = new Cell().Add(new Paragraph((item.cantidad*item.precio).ToString() +" bs"));
                _tabla.AddCell(_cell);
            }
            documento.Add(_tabla);
            documento.Add(new Paragraph("Total Bs: " + total));
            documento.Add(new Paragraph("Fecha de Detalle Boleta : " + DateTime.Now));
            documento.Close();
            byte[] bytestream = memo.ToArray();
            memo = new MemoryStream();
            memo.Write(bytestream, 0, bytestream.Length);
            memo.Position = 0;
            return new FileStreamResult(memo, "application/pdf");
        }

        [HttpPost]
        public JsonResult GetCarrito()
        {
            List<Carrito> carrito = (List<Carrito>)Session["carrito"];
            return Json(carrito, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MostrarCarrito()
        {
            List<Carrito> carrito = (List<Carrito>)Session["carrito"];
            return View(carrito);
        }
        [HttpPost]
        public ActionResult EliminarPedidoDCarrito()
        {
            List<Carrito> carrito = (List<Carrito>)Session["carrito"];
            int id = int.Parse( Request.Form["id"].ToString());
            string nombre = Request.Form["nombre"].ToString();
            foreach (var item in carrito)
            {
                if (item.nombre.Equals(nombre) && item.idproducto ==id) {
                    carrito.Remove(item);
                    break;
                }
            }
            Session["carrito"] = carrito;
            return RedirectToAction("MostrarCarrito");
        }
        [HttpPost]
        public void AgregarPedidosBBDD(int modo)
        {
            List<Carrito> pedidos = (List<Carrito>)Session["carrito"];
            //7 pago via paypal
            if (modo==7)
            {
                newpedido.AgregarNuevos(pedidos,7);
            }
            else
            {
                //8 pago modo personal
                newpedido.AgregarNuevos(pedidos,8);
                if (Session["User"] == null)
                {

                }
            }         
           
            Session["carrito"] = null;           
        }
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
        
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.  
            return View("SuccessView");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = "Item Name comes here",
                currency = "BO",
                price = "1",
                quantity = "1",
                sku = "sku"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "1"
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "BO",
                total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = "your generated invoice number", //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }        
    }
}
